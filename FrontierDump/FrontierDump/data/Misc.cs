using System.Collections.Generic;

namespace FrontierDump.data
{
    public static partial class Data
    {
        public static Dictionary<int, string> PlayerCap = new Dictionary<int, string>()
        {
            {0, "4 Player"},
            {1, "1 Player"},
            {2, "2 Player"},
            {3, "3 Player"},
        };

        public static Dictionary<int, string> Objectives = new Dictionary<int, string>()
        {
            {0x00000000, "None"},
            {0x00000001, "Hunt"},
            {0x00000101, "Capture"},
            {0x00000201, "Slay"},
            {0x00008004, "Damage"},
            {0x00018004, "Slay or Damage"},
            {0x00040000, "Slay All"},
            {0x00020000, "Slay Total"},
            {0x00000002, "Deliver"},
            {0x00004004, "Break Part"},
            {0x00001002, "Deliver Flag"},
            {0x00000010, "Esoteric Action"}
        };
    }
}