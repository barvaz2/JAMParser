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
            this.b_importFile = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.b_jhr = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_importFile
            // 
            this.b_importFile.Location = new System.Drawing.Point(12, 8);
            this.b_importFile.Name = "b_importFile";
            this.b_importFile.Size = new System.Drawing.Size(138, 61);
            this.b_importFile.TabIndex = 0;
            this.b_importFile.Text = "Import JDT File";
            this.b_importFile.UseVisualStyleBackColor = true;
            this.b_importFile.Click += new System.EventHandler(this.b_importFile_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 72);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1221, 338);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.b_jhr);
            this.panel1.Controls.Add(this.b_importFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 72);
            this.panel1.TabIndex = 2;
            // 
            // b_jhr
            // 
            this.b_jhr.Location = new System.Drawing.Point(176, 8);
            this.b_jhr.Name = "b_jhr";
            this.b_jhr.Size = new System.Drawing.Size(138, 61);
            this.b_jhr.TabIndex = 1;
            this.b_jhr.Text = "Import JHR File";
            this.b_jhr.UseVisualStyleBackColor = true;
            this.b_jhr.Click += new System.EventHandler(this.b_jhr_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 410);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_importFile;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button b_jhr;
    }
}

