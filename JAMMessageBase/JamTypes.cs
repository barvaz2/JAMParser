using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JAMMessageBase
{

    public class JAMConferenceContent
    {
        public JHRHeader Header;
        public List<JAMMessageRecord> MessageRecords;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1024, CharSet = CharSet.Ansi, Pack = 1)]
    public struct JHRHeader
    {
        public UInt32 Signature;       // <J><A><M> followed by <NUL>
        public UInt32 DateCreated;     // Creation date
        public UInt32 ModCounter;      // Update counter
        public UInt32 ActiveMsgs;      // Number of active (not deleted) msgs
        public UInt32 PasswordCrc;     // CRC-32 of password to access
        public UInt32 BaseMsgNum;      // Lowest message number in index file
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
        public byte[] RESERVED;  // Reserved space
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class JHRMessageRecordHeader
    {
        public UInt32 Signature;    // <J><A><M> followed by <NUL>
        public UInt16 Revision;     // Revision level of header          (1)
        public UInt16 ReservedWord; // Reserved for future use
        public UInt32 SubfieldLen;  // Length of subfields               (2)
        public UInt32 TimesRead;    // Number of times message read
        public UInt32 MSGIDcrc;     // CRC-32 of MSGID line              (3)
        public UInt32 REPLYcrc;     // CRC-32 of REPLY line              (3)
        public UInt32 ReplyTo;      // This msg is a reply to..
        public UInt32 Reply1st;     // First reply to this msg
        public UInt32 Replynext;    // Next msg in reply chain
        public UInt32 DateWritten;  // When msg was written
        public UInt32 DateReceived; // When msg was read by recipient
        public UInt32 DateProcessed;// When msg was processed by tosser/
        // scanner
        public UInt32 MessageNumber;// Message number (1-based)
        public UInt32 Attribute;    // Msg attribute, see "Msg Attributes"
        public UInt32 Attribute2;   // Reserved for future use
        public UInt32 Offset;       // Offset of text in ????????.JDT file
        public UInt32 TxtLen;       // Length of message text
        public UInt32 PasswordCRC;  // CRC-32 of password to access message
        public UInt32 Cost;         // Cost of message
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack=1)]
    public class JAMMessageRecord
    {
        public JHRMessageRecordHeader Header;
        public Dictionary<LoIDCodes, List<JHRMessageHeaderSubField>> SubFields;
        public byte[] MessageText;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class JHRMessageHeaderSubField
    {
        public UInt16 LoID;            // Field ID, 0-65535
        public UInt16 HiID;            // Reserved for future use
        public UInt32 datlen;          // Length of buffer that follows
        public byte[] Buffer;  // DATLEN bytes of data
    }

    public enum LoIDCodes
    {
        ADDRESS0 = 0,
        ADDRESS1 = 1,
        SENDERNAME = 2,
        RECEIVERNAME = 3,
        MSGID = 4,
        REPLYID = 5,
        SUBJECT = 6,
        PID = 7,
        TRACE = 8,
        ENCLOSEDFILE = 9,
        ENCLOSEDFILEWALIAS = 10,
        ENCLOSEDFREQ = 11,
        ENCLOSEDFILEWCARD = 12,
        ENCLOSEDINDIRECTFILE = 13,
        EMBINDAT = 1000,
        FTSKLUDGE = 2000,
        SEENBY2D = 2001,
        PATH2D = 2002,
        FLAGS = 2003,
        TZUTCINFO = 2004
    }

    public enum MessageAttributes : uint
    {
        MSG_LOCAL = 0x00000001,   // Msg created locally
        MSG_INTRANSIT = 0x00000002,   // Msg is in-transit
        MSG_PRIVATE = 0x00000004,   // Private
        MSG_READ = 0x00000008,   // Read by addressee
        MSG_SENT = 0x00000010,   // Sent to remote
        MSG_KILLSENT = 0x00000020,   // Kill when sent
        MSG_ARCHIVESENT = 0x00000040,   // Archive when sent
        MSG_HOLD = 0x00000080,   // Hold for pick-up
        MSG_CRASH = 0x00000100,   // Crash
        MSG_IMMEDIATE = 0x00000200,   // Send Msg now, ignore restrictions
        MSG_DIRECT = 0x00000400,   // Send directly to destination
        MSG_GATE = 0x00000800,   // Send via gateway
        MSG_FILEREQUEST = 0x00001000,   // File request
        MSG_FILEATTACH = 0x00002000,   // Files) attached to Msg
        MSG_TRUNCFILE = 0x00004000,   // Truncate files) when sent
        MSG_KILLFILE = 0x00008000,   // Delete files) when sent
        MSG_RECEIPTREQ = 0x00010000,   // Return receipt requested
        MSG_CONFIRMREQ = 0x00020000,   // Confirmation receipt requested
        MSG_ORPHAN = 0x00040000,   // Unknown destination
        MSG_ENCRYPT = 0x00080000,   // Msg text is encrypted          1)
        MSG_COMPRESS = 0x00100000,   // Msg text is compressed         1)
        MSG_ESCAPED = 0x00200000,   // Msg text is seven bit ASCII    1)
        MSG_FPU = 0x00400000,   // Force pickup
        MSG_TYPELOCAL = 0x00800000,   // Msg is for local use only
        MSG_TYPEECHO = 0x01000000,   // Msg is for conference distribution
        MSG_TYPENET = 0x02000000,   // Msg is direct network mail
        MSG_NODISP = 0x20000000,   // Msg may not be displayed to user
        MSG_LOCKED = 0x40000000,   // Msg is locked, no editing possible
        MSG_DELETED = 0x80000000,   // Msg is deleted
    }
}
