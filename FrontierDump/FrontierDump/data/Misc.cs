using System.Collections.Generic;

namespace FrontierDump.data
{
    public static partial class Data
    {
        public static Dictionary<int, string> Ranks = new Dictionary<int, string>()
        {
            {1, "Lower"},
            {2, "Lower"},
            {3, "Lower"},
            {4, "Lower"},
            {5, "Lower"},
            {6, "Lower"},
            {7, "Lower"},
            {8, "Lower"},
            {9, "Lower"},
            {10, "Lower"},
            {11, "Lower/Higher"},
            {12, "Higher"},
            {13, "Higher"},
            {14, "Higher"},
            {15, "Higher"},
            {16, "Higher"},
            {17, "Higher"},
            {18, "Higher"},
            {19, "Higher"},
            {20, "Higher"},
            {26, "HR5"},
            {31, "HR5"},
            {42, "HR5"},
            {53, "G Rank"},
            {54, "Musou 1"},
            {55, "Musou 2"},
            {64, "Z1"},
            {65, "Z2"},
            {66, "Z3"},
            {67, "Z4"},
            {71, "Interception"},
            {72, "Interception"},
            {73, "Interception"}
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