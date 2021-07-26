using System;

namespace FrontierDump.structs
{
    public class Quest
    {
        public string title { get; set; }
        public string textMain { get; set; }
        public string textSubA { get; set; }
        public string textSubB { get; set; }
        public string textSubC { get; set; }
        public string textSubD { get; set; }
        public string textSubE { get; set; }
        public string textSubF { get; set; }

        public byte unk1 { get; set; }
        public byte unk2 { get; set; }
        public byte unk3 { get; set; }
        public byte unk4 { get; set; }
        public byte level { get; set; }
        public byte unk5 { get; set; }
        public byte courseType { get; set; } // 6 = Premium, 18 = Free?, 19 = HLC?, 20 = Extra
        public byte unk7 { get; set; }
        public byte unk8 { get; set; }
        public byte unk9 { get; set; }
        public byte unk10 { get; set; }
        public byte unk11 { get; set; }
        public Int32 fee { get; set; }
        public Int32 zennyMain { get; set; }
        public Int32 zennyKo { get; set; }
        public Int32 zennySubA { get; set; }
        public Int32 zennySubB { get; set; }
        public Int32 time { get; set; }
        public string map { get; set; }
        public byte unk13 { get; set; }
        public byte unk14 { get; set; }
        public byte unk15 { get; set; }
        public byte unk16 { get; set; }
        public byte unk17 { get; set; }
        public byte unk18 { get; set; }
        public Int16 fileName { get; set; }
        public string mainGoalType { get; set; }
        public string mainGoalTarget { get; set; }
        public Int16 mainGoalCount { get; set; }
        public string subAGoalType { get; set; }
        public string subAGoalTarget { get; set; }
        public Int16 subAGoalCount { get; set; }
        public string subBGoalType { get; set; }
        public string subBGoalTarget { get; set; }
        public Int16 subBGoalCount { get; set; }

        public Int32 mainGRP { get; set; }
        public Int32 subAGRP { get; set; }
        public Int32 subBGRP { get; set; }
    }
}