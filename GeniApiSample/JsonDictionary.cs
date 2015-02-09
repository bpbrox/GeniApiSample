/****************************** Module Header ******************************\
Module Name:  JsonDictionary.cs
Project:      GeniApiSample
Copyright (c) Bjørn P. Brox <bjorn.brox@gmail.com>

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace GeniApiSample
{
    /// <summary>
    /// The base JSON object is a Dictionary where we avoid KeyNotFoundException's
    /// by having a object this[key] option which return null if not found.
    /// </summary>
    [Serializable]
    public class JsonDictionary : Dictionary<string, object>
    {
        private JsonDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }

        private JsonDictionary()
        {
        }

        public new object this[string key]
        {
            get
            {
                object @object;
                return TryGetValue(key, out @object) ? @object : null;
            }
            set { base[key] = value; }
        }

        public static JsonDictionary RequestUrl(string url, string accessToken)
        {
            return RequestUri(new Uri(url), accessToken);
        }

        public static JsonDictionary RequestUri(Uri uri, string accessToken)
        {
            using (WebClient request = new WebClient())
            {
                if (accessToken != null)
                {
                    request.Headers.Add("Authorization", String.Format("Bearer {0}", Uri.EscapeDataString(accessToken)));
                }
                return JsonParser.JsonDecode(request.DownloadString(uri));
            }
        }

        /// <summary>
        /// This class decodes JSON strings.
        /// See http://www.json.org/
        /// </summary>
        internal class JsonParser
        {
            private enum JsonToken
            {
                None = -1, // Used to denote no Lookahead available
                CurlyOpen,
                CurlyClose,
                BracketOpen,
                BracketClose,
                Colon,
                Comma,
                String,
                Number,
                True,
                False,
                Null
            }

            private readonly char[] _jsonData;
            private readonly StringBuilder _sb = new StringBuilder();
            private JsonToken _lookAheadToken = JsonToken.None;
            private int _jsonIndex;

            private JsonParser(string json)
            {
                _jsonData = json.ToCharArray();
            }

            /// <summary>
            /// Decodes a string into a JsonDictionaryclass. 
            /// </summary>
            /// <param name="jsonString">String to decode</param>
            /// <returns>Null if decoding fails, JsonDictionaryclass if success</returns>
            public static JsonDictionary JsonDecode(string jsonString)
            {
                try
                {
                    object jsonObject = new JsonParser(jsonString).ParseValue();
                    if (jsonObject == null)
                        return null;
                    if (jsonObject is JsonDictionary)
                        return jsonObject as JsonDictionary;
                    return jsonObject is Dictionary<string, object> ? new JsonDictionary((Dictionary<string, object>) jsonObject)
                               : new JsonDictionary(new Dictionary<string, object> {{"results", jsonObject}});
                }
                catch (Exception)
                {
                    return null;
                }
            }

            private JsonDictionary ParseObject()
            {
                JsonDictionary table = new JsonDictionary();
                ConsumeToken(); // Skip the {

                while (true)
                {
                    switch (LookAhead())
                    {
                        case JsonToken.Comma:
                            ConsumeToken();
                            break;
                        case JsonToken.CurlyClose:
                            ConsumeToken();
                            return table;
                        default:
                            // name
                            string name = ParseString();

                            // :
                            if (NextToken() != JsonToken.Colon)
                                throw new Exception("Expected colon at index " + _jsonIndex);

                            // value
                            table[name] = ParseValue();
                            break;
                    }
                }
            }

            private ArrayList ParseArray()
            {
                ArrayList array = new ArrayList();
                ConsumeToken(); // Skip the [

                while (true)
                {
                    switch (LookAhead())
                    {
                        case JsonToken.Comma:
                            ConsumeToken();
                            break;
                        case JsonToken.BracketClose:
                            ConsumeToken();
                            return array;
                        default:
                            array.Add(ParseValue());
                            break;
                    }
                }
            }

            private object ParseValue()
            {
                switch (LookAhead())
                {
                    case JsonToken.Number:
                        return ParseNumber();
                    case JsonToken.String:
                        return ParseString();
                    case JsonToken.CurlyOpen:
                        return ParseObject();
                    case JsonToken.BracketOpen:
                        return ParseArray();
                    case JsonToken.True:
                        ConsumeToken();
                        return true;
                    case JsonToken.False:
                        ConsumeToken();
                        return false;
                    case JsonToken.Null:
                        ConsumeToken();
                        return null;
                }

                throw new Exception("Unrecognized token at index " + _jsonIndex);
            }

            private string ParseString()
            {
                ConsumeToken(); // "

                _sb.Length = 0;
                int runIndex = -1;
                while (_jsonIndex < _jsonData.Length)
                {
                    var c = _jsonData[_jsonIndex++];

                    if (c == '"')
                    {
                        if (runIndex != -1)
                        {
                            if (_sb.Length == 0)
                                return new string(_jsonData, runIndex, _jsonIndex - runIndex - 1);

                            _sb.Append(_jsonData, runIndex, _jsonIndex - runIndex - 1);
                        }
                        return _sb.ToString();
                    }

                    if (c != '\\')
                    {
                        if (runIndex == -1)
                            runIndex = _jsonIndex - 1;

                        continue;
                    }

                    if (_jsonIndex == _jsonData.Length) break;

                    if (runIndex != -1)
                    {
                        _sb.Append(_jsonData, runIndex, _jsonIndex - runIndex - 1);
                        runIndex = -1;
                    }

                    switch (_jsonData[_jsonIndex++])
                    {
                        case '"':
                            _sb.Append('"');
                            break;
                        case '\\':
                            _sb.Append('\\');
                            break;
                        case '/':
                            _sb.Append('/');
                            break;
                        case 'b':
                            _sb.Append('\b');
                            break;
                        case 'f':
                            _sb.Append('\f');
                            break;
                        case 'n':
                            _sb.Append('\n');
                            break;
                        case 'r':
                            _sb.Append('\r');
                            break;
                        case 't':
                            _sb.Append('\t');
                            break;
                        case 'u':
                            {
                                int remainingLength = _jsonData.Length - _jsonIndex;
                                if (remainingLength < 4) break;

                                // parse the 32 bit hex into an integer codepoint
                                uint codePoint = ParseUnicode(_jsonData[_jsonIndex], _jsonData[_jsonIndex + 1],
                                                              _jsonData[_jsonIndex + 2], _jsonData[_jsonIndex + 3]);
                                _sb.Append((char) codePoint);

                                // skip 4 chars
                                _jsonIndex += 4;
                            }
                            break;
                    }
                }

                throw new Exception("Unexpectedly reached end of string");
            }

            private uint ParseSingleChar(char c1, uint multipliyer)
            {
                uint p1 = 0;
                if (c1 >= '0' && c1 <= '9')
                    p1 = (uint) (c1 - '0')*multipliyer;
                else if (c1 >= 'A' && c1 <= 'F')
                    p1 = (uint) ((c1 - 'A') + 10)*multipliyer;
                else if (c1 >= 'a' && c1 <= 'f')
                    p1 = (uint) ((c1 - 'a') + 10)*multipliyer;
                return p1;
            }

            private uint ParseUnicode(char c1, char c2, char c3, char c4)
            {
                uint p1 = ParseSingleChar(c1, 0x1000);
                uint p2 = ParseSingleChar(c2, 0x100);
                uint p3 = ParseSingleChar(c3, 0x10);
                uint p4 = ParseSingleChar(c4, 1);

                return p1 + p2 + p3 + p4;
            }

            private object ParseNumber()
            {
                ConsumeToken();

                // Need to start back one place because the first digit is also a token and would have been consumed
                var startIndex = _jsonIndex - 1;
                bool isDouble = false;
                do
                {
                    var c = _jsonData[_jsonIndex];

                    if ((c >= '0' && c <= '9') || c == '.' || c == '-' || c == '+' || c == 'e' || c == 'E')
                    {
                        if (++_jsonIndex == _jsonData.Length)
                            throw new Exception("Unexpected end of string whilst parsing number");
                        isDouble |= c == '.' || c == 'e' || c == 'E';
                        continue;
                    }
                    break;
                } while (true);

                string numberStr = new string(_jsonData, startIndex, _jsonIndex - startIndex);
                return isDouble
                           ? Double.Parse(numberStr, NumberStyles.Any, CultureInfo.InvariantCulture)
                           : Int64.Parse(numberStr, NumberStyles.Any, CultureInfo.InvariantCulture);
            }

            private JsonToken LookAhead()
            {
                if (_lookAheadToken != JsonToken.None) return _lookAheadToken;

                return _lookAheadToken = NextTokenCore();
            }

            private void ConsumeToken()
            {
                _lookAheadToken = JsonToken.None;
            }

            private JsonToken NextToken()
            {
                var result = _lookAheadToken != JsonToken.None ? _lookAheadToken : NextTokenCore();

                _lookAheadToken = JsonToken.None;
                return result;
            }

            private JsonToken NextTokenCore()
            {
                char c;

                // Skip past whitespace
                do
                {
                    c = _jsonData[_jsonIndex];

                    if (c == 0xfeff) // byte order mark (BOM) 
                        continue;
                    if (c > ' ') break;
                    if (c != ' ' && c != '\t' && c != '\n' && c != '\r') break;

                } while (++_jsonIndex < _jsonData.Length);

                if (_jsonIndex == _jsonData.Length)
                {
                    throw new Exception("Reached end of string unexpectedly");
                }

                c = _jsonData[_jsonIndex];

                _jsonIndex++;

                switch (c)
                {
                    case '{':
                        return JsonToken.CurlyOpen;
                    case '}':
                        return JsonToken.CurlyClose;
                    case '[':
                        return JsonToken.BracketOpen;
                    case ']':
                        return JsonToken.BracketClose;
                    case ',':
                        return JsonToken.Comma;
                    case '"':
                        return JsonToken.String;
                    case '0': case '1': case '2': case '3': case '4': case '5': case '6':
                    case '7': case '8': case '9': case '-': case '+': case '.':
                        return JsonToken.Number;
                    case ':':
                        return JsonToken.Colon;
                    case 'f':
                        if (_jsonData.Length - _jsonIndex >= 4 &&
                            _jsonData[_jsonIndex + 0] == 'a' &&
                            _jsonData[_jsonIndex + 1] == 'l' &&
                            _jsonData[_jsonIndex + 2] == 's' &&
                            _jsonData[_jsonIndex + 3] == 'e')
                        {
                            _jsonIndex += 4;
                            return JsonToken.False;
                        }
                        break;
                    case 't':
                        if (_jsonData.Length - _jsonIndex >= 3 &&
                            _jsonData[_jsonIndex + 0] == 'r' &&
                            _jsonData[_jsonIndex + 1] == 'u' &&
                            _jsonData[_jsonIndex + 2] == 'e')
                        {
                            _jsonIndex += 3;
                            return JsonToken.True;
                        }
                        break;
                    case 'n':
                        if (_jsonData.Length - _jsonIndex >= 3 &&
                            _jsonData[_jsonIndex + 0] == 'u' &&
                            _jsonData[_jsonIndex + 1] == 'l' &&
                            _jsonData[_jsonIndex + 2] == 'l')
                        {
                            _jsonIndex += 3;
                            return JsonToken.Null;
                        }
                        break;
                }

                throw new Exception("Could not find token at index " + --_jsonIndex);
            }
        }
    }
}
