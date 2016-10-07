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
        public Form1()
        {
            InitializeComponent();
            richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        }

        private bool isHeb(char c)
        {
            int i = (int)c;
            return (i >= (int)'א' && i <= (int)'ת'); 
        }

        private char replaceChar(char c)
        {
            switch (c)
            {
                case '(':
                    return ')';
                case ')':
                    return '(';
            }
            return c;
        }

        private string reverseHebrewInString(string hebStr)
        {
            string correctStr = string.Empty;
            Stack<char> hebChars = new Stack<char>();
            bool lineHeb = false;
            foreach (char c in hebStr)
            {
                if (isHeb(c))
                {
                    
                    lineHeb = true;
                }
            }
            if (lineHeb)
            {
                foreach (char c in hebStr)
                {
                    hebChars.Push(c);
                }
            }
            for (int i = 0; i < hebStr.Length; i++)
            {
                char c = hebStr[i];
                if (lineHeb)
                 {
                     correctStr += replaceChar(hebChars.Pop());
                 }
                 else
                 {
                     correctStr += c;
                 }
            }
            return correctStr;
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

        private void appendText(string s)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new Action(()=>
                    {
                        appendText(s);
                    }));
                return;
            }
            richTextBox1.AppendText(s);
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
                            sb.Append(reverseHebrewInString(s) + Environment.NewLine);
                        }
                        File.WriteAllText(Path.GetFileName(fname) + ".heb", sb.ToString());
                        appendText(string.Format("{0}/{1} {2}\n", counter,filesList.Count,fname));
                        counter++;
                    }
                }));
        //    richTextBox1.Text = sb.ToString();
        }

        private void b_jhr_Click(object sender, EventArgs e)
        {
            string folder = @"D:\BBS\JAM\C_NET";
            string areaName = "BIRTH";
            string fname = Path.Combine(folder, areaName + ".JHR");

            JHRFileContent headerContent;
            string error = JAMParser.ReadJHRFile(fname, out headerContent);

            if (String.IsNullOrEmpty(error))
            {
                appendText(string.Format("File {0} was sucessfully read\n",fname));
            }
            else
            {
                appendText(error + "\n");
            }
        }
    }
}
