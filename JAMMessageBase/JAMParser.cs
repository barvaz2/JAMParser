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
        public static string ReadJHRFile(string fname, out JHRFileContent jhrContent)
        {
            jhrContent = new JHRFileContent();
            jhrContent.Header = new JHRHeader();
            jhrContent.MessageHeaders = new List<JHRMessageRecord>();
            if (!File.Exists(fname))
                return "File not found";

            byte[] buffer = File.ReadAllBytes(fname);
            int offset = 0;
            CopyVar<JHRHeader>(buffer, ref offset, ref jhrContent.Header);
          
            while (offset < buffer.Length)
            {
                JHRMessageRecord record;
                ReadJHRMessageRecord(buffer, ref offset, out record);
                jhrContent.MessageHeaders.Add(record);
            }

            return string.Empty;
        }

        public static string ReadJHRMessageRecord(byte[] buffer, ref int offset, out JHRMessageRecord record)
        {
            record = new JHRMessageRecord();
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
