namespace ConvertJAMApp
{
    partial class Form1
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
            this.rtb_StatusMessages = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.b_jhr = new System.Windows.Forms.Button();
            this.tb_ConfPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ConfName = new System.Windows.Forms.TextBox();
            this.panel_messages = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_StatusMessages
            // 
            this.rtb_StatusMessages.BackColor = System.Drawing.Color.Black;
            this.rtb_StatusMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_StatusMessages.ForeColor = System.Drawing.Color.Yellow;
            this.rtb_StatusMessages.Location = new System.Drawing.Point(0, 281);
            this.rtb_StatusMessages.Name = "rtb_StatusMessages";
            this.rtb_StatusMessages.Size = new System.Drawing.Size(1221, 108);
            this.rtb_StatusMessages.TabIndex = 1;
            this.rtb_StatusMessages.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_ConfName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_ConfPath);
            this.panel1.Controls.Add(this.b_jhr);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 68);
            this.panel1.TabIndex = 2;
            // 
            // b_jhr
            // 
            this.b_jhr.Location = new System.Drawing.Point(12, 8);
            this.b_jhr.Name = "b_jhr";
            this.b_jhr.Size = new System.Drawing.Size(218, 58);
            this.b_jhr.TabIndex = 1;
            this.b_jhr.Text = "Import Conference Area";
            this.b_jhr.UseVisualStyleBackColor = true;
            this.b_jhr.Click += new System.EventHandler(this.b_jhr_Click);
            // 
            // tb_ConfPath
            // 
            this.tb_ConfPath.Location = new System.Drawing.Point(381, 11);
            this.tb_ConfPath.Name = "tb_ConfPath";
            this.tb_ConfPath.Size = new System.Drawing.Size(733, 27);
            this.tb_ConfPath.TabIndex = 2;
            this.tb_ConfPath.Text = "D:\\BBS\\JAM\\C_NET";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Conference path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Conference name";
            // 
            // tb_ConfName
            // 
            this.tb_ConfName.Location = new System.Drawing.Point(381, 38);
            this.tb_ConfName.Name = "tb_ConfName";
            this.tb_ConfName.Size = new System.Drawing.Size(177, 27);
            this.tb_ConfName.TabIndex = 4;
            this.tb_ConfName.Text = "BIRTH";
            // 
            // panel_messages
            // 
            this.panel_messages.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_messages.Location = new System.Drawing.Point(0, 68);
            this.panel_messages.Name = "panel_messages";
            this.panel_messages.Size = new System.Drawing.Size(1221, 213);
            this.panel_messages.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 389);
            this.Controls.Add(this.panel_messages);
            this.Controls.Add(this.rtb_StatusMessages);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_StatusMessages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button b_jhr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ConfName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ConfPath;
        private System.Windows.Forms.Panel panel_messages;
    }
}

