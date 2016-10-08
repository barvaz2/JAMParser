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
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ConfName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ConfPath = new System.Windows.Forms.TextBox();
            this.b_importConferenceArea = new System.Windows.Forms.Button();
            this.panel_messages = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.clm_MessageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm_MessagePID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.l_UpperStatus = new System.Windows.Forms.Label();
            this.panel_recordsProcessed = new System.Windows.Forms.Panel();
            this.panel_MessageText = new System.Windows.Forms.Panel();
            this.rtb_MessageText = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel_messages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel_recordsProcessed.SuspendLayout();
            this.panel_MessageText.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_StatusMessages
            // 
            this.rtb_StatusMessages.BackColor = System.Drawing.Color.Black;
            this.rtb_StatusMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_StatusMessages.ForeColor = System.Drawing.Color.Yellow;
            this.rtb_StatusMessages.Location = new System.Drawing.Point(0, 599);
            this.rtb_StatusMessages.Name = "rtb_StatusMessages";
            this.rtb_StatusMessages.Size = new System.Drawing.Size(1221, 79);
            this.rtb_StatusMessages.TabIndex = 1;
            this.rtb_StatusMessages.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_recordsProcessed);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_ConfName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_ConfPath);
            this.panel1.Controls.Add(this.b_importConferenceArea);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 68);
            this.panel1.TabIndex = 2;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Conference path";
            // 
            // tb_ConfPath
            // 
            this.tb_ConfPath.Location = new System.Drawing.Point(381, 11);
            this.tb_ConfPath.Name = "tb_ConfPath";
            this.tb_ConfPath.Size = new System.Drawing.Size(452, 27);
            this.tb_ConfPath.TabIndex = 2;
            this.tb_ConfPath.Text = "D:\\BBS\\JAM\\C_NET";
            // 
            // b_importConferenceArea
            // 
            this.b_importConferenceArea.Location = new System.Drawing.Point(12, 8);
            this.b_importConferenceArea.Name = "b_importConferenceArea";
            this.b_importConferenceArea.Size = new System.Drawing.Size(218, 52);
            this.b_importConferenceArea.TabIndex = 1;
            this.b_importConferenceArea.Text = "Import Conference Area";
            this.b_importConferenceArea.UseVisualStyleBackColor = true;
            this.b_importConferenceArea.Click += new System.EventHandler(this.b_importConferenceArea_Click);
            // 
            // panel_messages
            // 
            this.panel_messages.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_messages.Controls.Add(this.dataGridView1);
            this.panel_messages.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_messages.Location = new System.Drawing.Point(0, 68);
            this.panel_messages.Name = "panel_messages";
            this.panel_messages.Size = new System.Drawing.Size(1221, 213);
            this.panel_messages.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clm_MessageID,
            this.clm_From,
            this.clm_To,
            this.clm_Subject,
            this.clm_DateTime,
            this.clm_MessagePID});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1221, 213);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // clm_MessageID
            // 
            this.clm_MessageID.FillWeight = 180F;
            this.clm_MessageID.HeaderText = "ID";
            this.clm_MessageID.Name = "clm_MessageID";
            this.clm_MessageID.ReadOnly = true;
            this.clm_MessageID.Width = 180;
            // 
            // clm_From
            // 
            this.clm_From.FillWeight = 120F;
            this.clm_From.HeaderText = "From";
            this.clm_From.Name = "clm_From";
            this.clm_From.ReadOnly = true;
            this.clm_From.Width = 120;
            // 
            // clm_To
            // 
            this.clm_To.FillWeight = 120F;
            this.clm_To.HeaderText = "To";
            this.clm_To.Name = "clm_To";
            this.clm_To.ReadOnly = true;
            this.clm_To.Width = 120;
            // 
            // clm_Subject
            // 
            this.clm_Subject.FillWeight = 230F;
            this.clm_Subject.HeaderText = "Subject";
            this.clm_Subject.Name = "clm_Subject";
            this.clm_Subject.ReadOnly = true;
            this.clm_Subject.Width = 230;
            // 
            // clm_DateTime
            // 
            this.clm_DateTime.FillWeight = 140F;
            this.clm_DateTime.HeaderText = "Date";
            this.clm_DateTime.Name = "clm_DateTime";
            this.clm_DateTime.ReadOnly = true;
            this.clm_DateTime.Width = 140;
            // 
            // clm_MessagePID
            // 
            this.clm_MessagePID.FillWeight = 180F;
            this.clm_MessagePID.HeaderText = "PID";
            this.clm_MessagePID.Name = "clm_MessagePID";
            this.clm_MessagePID.ReadOnly = true;
            this.clm_MessagePID.Width = 180;
            // 
            // l_UpperStatus
            // 
            this.l_UpperStatus.AutoSize = true;
            this.l_UpperStatus.Location = new System.Drawing.Point(13, 25);
            this.l_UpperStatus.Name = "l_UpperStatus";
            this.l_UpperStatus.Size = new System.Drawing.Size(0, 19);
            this.l_UpperStatus.TabIndex = 6;
            // 
            // panel_recordsProcessed
            // 
            this.panel_recordsProcessed.Controls.Add(this.l_UpperStatus);
            this.panel_recordsProcessed.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_recordsProcessed.Location = new System.Drawing.Point(869, 0);
            this.panel_recordsProcessed.Name = "panel_recordsProcessed";
            this.panel_recordsProcessed.Size = new System.Drawing.Size(352, 68);
            this.panel_recordsProcessed.TabIndex = 8;
            // 
            // panel_MessageText
            // 
            this.panel_MessageText.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel_MessageText.Controls.Add(this.rtb_MessageText);
            this.panel_MessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MessageText.Location = new System.Drawing.Point(0, 281);
            this.panel_MessageText.Name = "panel_MessageText";
            this.panel_MessageText.Padding = new System.Windows.Forms.Padding(15);
            this.panel_MessageText.Size = new System.Drawing.Size(1221, 318);
            this.panel_MessageText.TabIndex = 4;
            // 
            // rtb_MessageText
            // 
            this.rtb_MessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_MessageText.Location = new System.Drawing.Point(15, 15);
            this.rtb_MessageText.Name = "rtb_MessageText";
            this.rtb_MessageText.Size = new System.Drawing.Size(1191, 288);
            this.rtb_MessageText.TabIndex = 0;
            this.rtb_MessageText.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 678);
            this.Controls.Add(this.panel_MessageText);
            this.Controls.Add(this.panel_messages);
            this.Controls.Add(this.rtb_StatusMessages);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Name = "Form1";
            this.Text = "Convert JAP app";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_messages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel_recordsProcessed.ResumeLayout(false);
            this.panel_recordsProcessed.PerformLayout();
            this.panel_MessageText.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_StatusMessages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button b_importConferenceArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ConfName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ConfPath;
        private System.Windows.Forms.Panel panel_messages;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_MessageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_From;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_To;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_MessagePID;
        private System.Windows.Forms.Label l_UpperStatus;
        private System.Windows.Forms.Panel panel_recordsProcessed;
        private System.Windows.Forms.Panel panel_MessageText;
        private System.Windows.Forms.RichTextBox rtb_MessageText;
    }
}

