using JAMMessageBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertJAMApp
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> MessagesText;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "d";
        }

        static void DirSearch(string sDir, string searchPattern, ref List<string> retVal)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir, searchPattern))
                {
                    retVal.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                   
                    DirSearch(d, searchPattern,ref retVal);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }       

        private void b_importFile_Click(object sender, EventArgs e)
        {

            Encoding latinEncoding = Encoding.GetEncoding("Windows-1255");
            Encoding hebrewEncoding = Encoding.GetEncoding(862);
            List<string> filesList = new List<string>();
            DirSearch(@"D:\From old\KEEP\", "*.JDT", ref filesList);
            int counter = 1;
            Task.Factory.StartNew(new Action(() =>
                {
                    foreach (string fname in filesList)
                    {
                        byte[] latinBytes = File.ReadAllBytes(fname);
                        string hebrewString = hebrewEncoding.GetString(latinBytes);
                        string[] sa = hebrewString.Split('\r');
                        StringBuilder sb = new StringBuilder();
                        foreach (string s in sa)
                        {
                            sb.Append(JAMToModernConverter.ReverseHebrewInString(s) + Environment.NewLine);
                        }
                        File.WriteAllText(Path.GetFileName(fname) + ".heb", sb.ToString());
                        AppendText(string.Format("{0}/{1} {2}\n", counter, filesList.Count, fname));
                        counter++;
                    }
                }));
        //    richTextBox1.Text = sb.ToString();
        }

        private void UpdateRecordCountProgress(int count)
        {
            ThreadSafeFacade.UpdateLabelText(l_UpperStatus, string.Format("Processing JAM, {0} records processed",count.ToString()));
        }
        private void UpdateModernRecordCountProgress(int count)
        {
            ThreadSafeFacade.UpdateLabelText(l_UpperStatus, string.Format("Converting JAM to modern, {0} records processed", count.ToString()));
        }


        private void ImportClicked()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                string folder = tb_ConfPath.Text;
                string confName = tb_ConfName.Text;

                JAMConferenceContent jamConfContent;
                AppendText(string.Format("Processing JAM Message base {0}\\{1}...\n", folder, confName));
                string error = JAMParser.ReadJAMConference(folder, confName, out jamConfContent, UpdateRecordCountProgress);

                if (String.IsNullOrEmpty(error))
                {
                    AppendText(string.Format("Conference {0}\\{1} was sucessfully read.\n", folder, confName));
                    MessagesText = new Dictionary<string, string>();
                    if (jamConfContent.MessageRecords != null)
                    {
                        AppendText(string.Format("{0} Messages parsed\n", jamConfContent.MessageRecords.Count));
                    }

                    ModernConferenceContent modernConfContent;

                    AppendText("Converting to modern format...\n");
                    JAMToModernConverter.ConvertJAMToModern(jamConfContent, out modernConfContent, confName, UpdateModernRecordCountProgress);
                    int recCount = 0;

                    if (modernConfContent.MessageRecords != null && modernConfContent.MessageRecords.Count > 0)
                    {
                        ThreadSafeFacade.ClearDataGridView(dataGridView1);
                        foreach (ModernMessageRecord record in modernConfContent.MessageRecords)
                        {
                            if (null != record.MessageID)
                            {
                                object[] sa = new object[6];
                                sa[0] = recCount.ToString();
                                sa[1] = record.From;
                                sa[2] = record.To;
                                sa[3] = record.Subject;
                                sa[4] = record.MessageDate;
                                sa[5] = record.MessagePID;
                                MessagesText[recCount.ToString()] = record.TextOrig;
                                ThreadSafeFacade.AddRecordToDataGridView(dataGridView1, sa);
                                recCount++;
                                ThreadSafeFacade.UpdateLabelText(l_UpperStatus, string.Format("Converting to modern format, {0} records processed", recCount.ToString()));
                            }
                        }

                        AppendText("Serializing object to XML...\n");
                        string xml = JAMToModernConverter.SerializeModernConferenceContent(modernConfContent);
                        File.WriteAllText(confName + ".XML", xml);
                        AppendText("Finished!\n");
                        
                    }
                }
                else
                {
                    AppendText(error + "\n");
                }

               
            }));
        }

        private void AppendText(string s)
        {
            ThreadSafeFacade.AppendTextToRichTextbox(rtb_StatusMessages, s);
        }

        private void b_importConferenceArea_Click(object sender, EventArgs e)
        {
            ImportClicked();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                return;

            string messageText = "N/A";
            if (null != dataGridView1.SelectedRows[0].Cells[0].Value)
            {
                string msgID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
             
                if (MessagesText.ContainsKey(msgID))
                {
                    messageText = MessagesText[msgID];
                }
            }
            ThreadSafeFacade.UpdateBrowserDocumentText(webBrowser1, GetHTML(messageText));
        }

        private string GetHTML(string messageText)
        {
            return string.Format("<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><title></title></head><body><pre><bdo dir=\"ltr\" style=\"font-family: monospace;\">{0}</bdo></pre></body></html>", messageText);
        }
    }
}
