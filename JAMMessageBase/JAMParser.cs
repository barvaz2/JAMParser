using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JAMMessageBase
{
    public class JAMParser
    {
        ///<summary>
        ///ReadJHRFile returns description of error when it occurs. Otherwise it returns string.Empty
        ///</summary>
        public static string ReadJAMConference(string folderPath, string conferenceFileName, out JAMConferenceContent jhrContent)
        {
            string jhrFname = Path.Combine(folderPath, conferenceFileName + ".JHR");
            string jdtFname = Path.Combine(folderPath, conferenceFileName + ".JDT");

            jhrContent = new JAMConferenceContent();
            jhrContent.Header = new JHRHeader();
            jhrContent.MessageRecords = new List<JAMMessageRecord>();

            if (!File.Exists(jhrFname))
                return "JHR File not found";
            if (!File.Exists(jdtFname))
                return "JDT File not found";

            byte[] jhrBuffer = File.ReadAllBytes(jhrFname);
            int jhrOffset = 0;

            byte[] jdtBuffer = File.ReadAllBytes(jdtFname);

            CopyVar<JHRHeader>(jhrBuffer, ref jhrOffset, ref jhrContent.Header);
          
            while (jhrOffset < jhrBuffer.Length)
            {
                JAMMessageRecord record;
                ReadJHRMessageRecord(jhrBuffer, ref jhrOffset, out record);
                PopulateMessageRecordText(jdtBuffer, ref record);
                jhrContent.MessageRecords.Add(record);
            }

            return string.Empty;
        }

        private static void PopulateMessageRecordText(byte[] jdtBuffer, ref JAMMessageRecord record)
        {
            if (null == record || null==record.Header)
                return;

            if (record.Header.TxtLen == 0 // Empty message
                || (record.Header.Offset + record.Header.TxtLen) > jdtBuffer.Length // Mismatch between JHR and JDT file
                || (record.Header.Attribute & (uint)MessageAttributes.MSG_DELETED) != 0) // Message was deleted and should be ignored
                return;

            record.MessageText = new byte[record.Header.TxtLen];
            Buffer.BlockCopy(jdtBuffer, (int)record.Header.Offset, record.MessageText, 0, (int)record.Header.TxtLen);
        }

        public static string ReadJHRMessageRecord(byte[] buffer, ref int offset, out JAMMessageRecord record)
        {
            record = new JAMMessageRecord();
            record.Header = new JHRMessageRecordHeader();

            CopyVar<JHRMessageRecordHeader>(buffer, ref offset, ref record.Header);

            if (record.Header.SubfieldLen > 0)
            {
                int subfieldsOffsetEnd = offset + (int)record.Header.SubfieldLen;
                record.SubFields = new Dictionary<int, List<JHRMessageHeaderSubField>>();
                while (offset < subfieldsOffsetEnd)
                {
                    JHRMessageHeaderSubField subField = new JHRMessageHeaderSubField();

                    CopyVar<UInt16>(buffer, ref offset, ref subField.LoID);
                    CopyVar<UInt16>(buffer, ref offset, ref subField.HiID);
                    CopyVar<UInt32>(buffer, ref offset, ref subField.datlen);
                    if (subField.datlen > 0)
                    {
                        subField.Buffer = new byte[subField.datlen];
                        Buffer.BlockCopy(buffer, offset, subField.Buffer, 0, (int)subField.datlen);
                        offset += (int) subField.datlen;
                    }
                    if (!record.SubFields.ContainsKey(subField.LoID))
                    {
                        record.SubFields[subField.LoID] = new List<JHRMessageHeaderSubField>();
                    }                  
                    record.SubFields[subField.LoID].Add(subField);
                }
            }

            return string.Empty;
        }

        public static void CopyVar<T>(byte[] buffer, ref int offset, ref T var)
        {
            IntPtr ptrObj = IntPtr.Zero;
            int objSize = Marshal.SizeOf(var);
            ptrObj = Marshal.AllocHGlobal(objSize);
            Marshal.Copy(buffer, offset, ptrObj, objSize);
            var = (T)Marshal.PtrToStructure(ptrObj, var.GetType());
            offset += objSize;
        }

        public static void CopyBuffer(byte[] buffer, ref int offset, int len, out byte[] outputBuffer)
        {
            outputBuffer = new byte[len];
            for (int i=offset;i<offset+len;i++)
            {
                outputBuffer[i - offset] = buffer[i];
            }
        }
    }
}
