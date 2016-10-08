using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMMessageBase
{
    [Serializable]
    public class ModernConferenceContent
    {
        public string ConferenceName;
        public List<ModernMessageRecord> MessageRecords;
    }

    public class ModernMessageRecord
    {
        public string MessageID;
        public string MessagePID;
        public DateTime MessageDate;
        public string From;
        public List<string> FromAddress;
        public string To;
        public List<string> ToAddress;
        public string Subject;
        public string TextHeb;
        public string TextOrig;

        public List<string> Path2D;
    }
}
