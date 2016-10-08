using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JAMMessageBase
{
    public class JAMToModernConverter
    {
        private static Encoding _latinEncoding = Encoding.GetEncoding("Windows-1255");
        private static Encoding _hebrewEncoding = Encoding.GetEncoding(862);

        public static string ConvertJAMToModern(JAMConferenceContent JAMContent, out ModernConferenceContent ModernContent, string ConfName, Action<int> RecordsProcessedDel = null)
        {
            ModernContent = new ModernConferenceContent();
            ModernContent.ConferenceName = ConfName;
            ModernContent.MessageRecords = new List<ModernMessageRecord>();
            int recCount = 0;
            if (null != RecordsProcessedDel)
            {
                RecordsProcessedDel(recCount);
            }
            if (null != JAMContent.MessageRecords)
            {
                foreach (JAMMessageRecord record in JAMContent.MessageRecords)
                {
                    ModernContent.MessageRecords.Add(ConvertJAMMessageRecord(record));
                    recCount++;
                    if (null != RecordsProcessedDel)
                    {
                        RecordsProcessedDel(recCount);
                    }
                }
            }

            return string.Empty;
        }

        private static ModernMessageRecord ConvertJAMMessageRecord(JAMMessageRecord jamRecord)
        {
            ModernMessageRecord modRecord = new ModernMessageRecord();

            ConvertSubfieldStringList(jamRecord, LoIDCodes.ADDRESS0, ref modRecord.FromAddress);
            ConvertSubfieldStringList(jamRecord, LoIDCodes.ADDRESS1, ref modRecord.ToAddress);
            ConvertSubfieldString(jamRecord, LoIDCodes.SENDERNAME, ref modRecord.From);
            ConvertSubfieldString(jamRecord, LoIDCodes.RECEIVERNAME, ref modRecord.To);
            ConvertSubfieldString(jamRecord, LoIDCodes.MSGID, ref modRecord.MessageID);
            ConvertSubfieldString(jamRecord, LoIDCodes.PID, ref modRecord.MessagePID);
            ConvertSubfieldString(jamRecord, LoIDCodes.SUBJECT, ref modRecord.Subject);        
            ConvertSubfieldStringList(jamRecord, LoIDCodes.PATH2D, ref modRecord.Path2D);
            modRecord.TextHeb = ConvertBufferToHebrewString(jamRecord.MessageText);
            modRecord.TextOrig = _hebrewEncoding.GetString(jamRecord.MessageText);
            modRecord.MessageDate = new DateTime(1970, 1, 1).AddSeconds(jamRecord.Header.DateWritten);
            return modRecord;
        }

        private static void ConvertSubfieldString(JAMMessageRecord jamRecord, LoIDCodes loID, ref string str)
        {
            if (jamRecord.SubFields.ContainsKey(loID) && jamRecord.SubFields[loID].Count > 0)
            {
                str = ConvertJAMSubfieldToString(jamRecord.SubFields[loID][0]);
            }
        }

        private static void ConvertSubfieldStringList(JAMMessageRecord jamRecord, LoIDCodes loID, ref List<string> list)
        {
            if (jamRecord.SubFields.ContainsKey(loID))
            {
                list = new List<string>();
                foreach (JHRMessageHeaderSubField subField in jamRecord.SubFields[loID])
                {
                    list.Add(ConvertJAMSubfieldToString(subField));
                }
            }
        }

        private static string ConvertJAMSubfieldToString(JHRMessageHeaderSubField subfield)
        {
            string retval = null;

            if (null != subfield && subfield.datlen > 0)
            {
                retval = ConvertBufferToHebrewString(subfield.Buffer);
            }

            return retval;
        }

        private static string ConvertBufferToHebrewString(byte[] buffer)
        {
            if (null != buffer && buffer.Length > 0)
            {
                string hebStr= _hebrewEncoding.GetString(buffer);
                hebStr = ReplaceMemSofit(hebStr);
                return NBidi.NBidi.LogicalToVisual(hebStr);
              //  return JAMToModernConverter.ReverseHebrewInString(_hebrewEncoding.GetString(buffer));
            }

            return null;
        }

        private static string ReplaceMemSofit(string hebStr)
        {
            bool prevCharIsHeb = false;
            StringBuilder sb = new StringBuilder();
            for (int i = hebStr.Length-1; i >=0 ; i--)
            {
                char c = hebStr[i];
                if (hebStr[i] == 'O')
                {

                }
                if (prevCharIsHeb && hebStr[i] == 'O')
                {
                    c = 'ם';
                    prevCharIsHeb = true;
                }
                else if (isHeb(hebStr[i]))
                    prevCharIsHeb = true;
                else 
                {
                     prevCharIsHeb = false;
                }
                sb.Append(c);
            }

            return ReverseString(sb.ToString());
        }

        public static string ReverseString(string text)
        {
            if (text == null) return null;

            // this was posted by petebob as well 
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

        private static bool isHeb(char c)
        {
            int i = (int)c;
            return (i >= (int)'א' && i <= (int)'ת');
        }

        private static char replaceChar(char c)
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

        public static string ReverseHebrewInString(string hebStr)
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
            else
            {

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

        public static string SerializeModernConferenceContent(ModernConferenceContent content)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CheckCharacters = false;
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ModernConferenceContent));
            
            StringWriter sww = new StringWriter();

            string xml = null;
            using (XmlWriter writer = XmlWriter.Create(sww, settings))
            {
                xsSubmit.Serialize(writer, content);
                xml = sww.ToString(); // Your XML
            }

            return xml;
        }
    }
}
