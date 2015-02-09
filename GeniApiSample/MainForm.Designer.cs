namespace GeniApiSample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.messagesBox = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.brandingPbx = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.userLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bigtreeLbl = new System.Windows.Forms.Label();
            this.accounttypeLbl = new System.Windows.Forms.Label();
            this.emailLbl = new System.Windows.Forms.Label();
            this.joinedLbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.residenceLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.brandingPbx)).BeginInit();
            this.SuspendLayout();
            // 
            // messagesBox
            // 
            this.messagesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesBox.BackColor = System.Drawing.SystemColors.Window;
            this.messagesBox.Location = new System.Drawing.Point(15, 156);
            this.messagesBox.Multiline = true;
            this.messagesBox.Name = "messagesBox";
            this.messagesBox.ReadOnly = true;
            this.messagesBox.Size = new System.Drawing.Size(595, 184);
            this.messagesBox.TabIndex = 0;
            // 
            // connectBtn
            // 
            this.connectBtn.AutoSize = true;
            this.connectBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectBtn.Image = ((System.Drawing.Image)(resources.GetObject("connectBtn.Image")));
            this.connectBtn.Location = new System.Drawing.Point(12, 12);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(149, 32);
            this.connectBtn.TabIndex = 1;
            this.toolTip1.SetToolTip(this.connectBtn, "Click to connect to geni.com");
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.Location = new System.Drawing.Point(167, 17);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(75, 23);
            this.disconnectBtn.TabIndex = 1;
            this.disconnectBtn.Text = "Disconnect";
            this.toolTip1.SetToolTip(this.disconnectBtn, "Click to disconnect");
            this.disconnectBtn.UseVisualStyleBackColor = true;
            this.disconnectBtn.Click += new System.EventHandler(this.disconnectBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(248, 17);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "Exit";
            this.toolTip1.SetToolTip(this.exitBtn, "Terminat program");
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // brandingPbx
            // 
            this.brandingPbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brandingPbx.Image = ((System.Drawing.Image)(resources.GetObject("brandingPbx.Image")));
            this.brandingPbx.Location = new System.Drawing.Point(337, 19);
            this.brandingPbx.Name = "brandingPbx";
            this.brandingPbx.Size = new System.Drawing.Size(270, 115);
            this.brandingPbx.TabIndex = 2;
            this.brandingPbx.TabStop = false;
            this.toolTip1.SetToolTip(this.brandingPbx, "Click for details.\r\nThis application uses the Geni API but is not endorsed, opera" +
        "ted, or sponsored by Geni.com.");
            this.brandingPbx.Click += new System.EventHandler(this.brandingPbx_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User:";
            // 
            // userLbl
            // 
            this.userLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userLbl.AutoEllipsis = true;
            this.userLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userLbl.Location = new System.Drawing.Point(112, 51);
            this.userLbl.Name = "userLbl";
            this.userLbl.Size = new System.Drawing.Size(211, 15);
            this.userLbl.TabIndex = 4;
            this.userLbl.Text = "                  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Big Tree:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Account Type:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Email:";
            // 
            // bigtreeLbl
            // 
            this.bigtreeLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bigtreeLbl.AutoEllipsis = true;
            this.bigtreeLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bigtreeLbl.Location = new System.Drawing.Point(112, 68);
            this.bigtreeLbl.Name = "bigtreeLbl";
            this.bigtreeLbl.Size = new System.Drawing.Size(211, 15);
            this.bigtreeLbl.TabIndex = 4;
            this.bigtreeLbl.Text = "                  ";
            // 
            // accounttypeLbl
            // 
            this.accounttypeLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accounttypeLbl.AutoEllipsis = true;
            this.accounttypeLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.accounttypeLbl.Location = new System.Drawing.Point(112, 85);
            this.accounttypeLbl.Name = "accounttypeLbl";
            this.accounttypeLbl.Size = new System.Drawing.Size(211, 15);
            this.accounttypeLbl.TabIndex = 4;
            this.accounttypeLbl.Text = "                  ";
            // 
            // emailLbl
            // 
            this.emailLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailLbl.AutoEllipsis = true;
            this.emailLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.emailLbl.Location = new System.Drawing.Point(112, 103);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(211, 15);
            this.emailLbl.TabIndex = 4;
            this.emailLbl.Text = "                  ";
            // 
            // joinedLbl
            // 
            this.joinedLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.joinedLbl.AutoEllipsis = true;
            this.joinedLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.joinedLbl.Location = new System.Drawing.Point(112, 120);
            this.joinedLbl.Name = "joinedLbl";
            this.joinedLbl.Size = new System.Drawing.Size(211, 15);
            this.joinedLbl.TabIndex = 4;
            this.joinedLbl.Text = "                  ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Profile Created:";
            // 
            // residenceLbl
            // 
            this.residenceLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.residenceLbl.AutoEllipsis = true;
            this.residenceLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.residenceLbl.Location = new System.Drawing.Point(112, 138);
            this.residenceLbl.Name = "residenceLbl";
            this.residenceLbl.Size = new System.Drawing.Size(212, 15);
            this.residenceLbl.TabIndex = 4;
            this.residenceLbl.Text = "                  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Residence:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 352);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.residenceLbl);
            this.Controls.Add(this.joinedLbl);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.accounttypeLbl);
            this.Controls.Add(this.bigtreeLbl);
            this.Controls.Add(this.userLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.brandingPbx);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.disconnectBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.messagesBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(635, 343);
            this.Name = "MainForm";
            this.Text = "API sample app for Geni.com";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.brandingPbx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messagesBox;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button disconnectBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox brandingPbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label userLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label bigtreeLbl;
        private System.Windows.Forms.Label accounttypeLbl;
        private System.Windows.Forms.Label emailLbl;
        private System.Windows.Forms.Label joinedLbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label residenceLbl;
        private System.Windows.Forms.Label label5;
    }
}

