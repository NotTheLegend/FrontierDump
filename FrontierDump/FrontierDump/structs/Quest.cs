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
        public string stars { get; set; }
        public byte unk5 { get; set; }
        public byte courseType { get; set; } // 8 = Premium, 18 = Free?, 19 = HLC?, 20 = Extra
        public byte unk7 { get; set; }
        public byte unk8 { get; set; }
        public byte unk9 { get; set; }
        public byte unk10 { get; set; }
        public string pCap { get; set; }//Player Cap
        public int fee { get; set; }
        public int zennyMain { get; set; }
        public int zennyKo { get; set; }
        public int zennySubA { get; set; }
        public int zennySubB { get; set; }
        public int time { get; set; }
        public string map { get; set; }
        public byte unk13 { get; set; }
        public byte unk14 { get; set; }
        public byte unk15 { get; set; }
        public byte unk16 { get; set; }
        public byte unk17 { get; set; }
        public byte unk18 { get; set; }
        public int fileName { get; set; }
        public string mainGoalType { get; set; }
        public string mainGoalTarget { get; set; }
        public int mainGoalCount { get; set; }
        public string subAGoalType { get; set; }
        public string subAGoalTarget { get; set; }
        public int subAGoalCount { get; set; }
        public string subBGoalType { get; set; }
        public string subBGoalTarget { get; set; }
        public int subBGoalCount { get; set; }
        public int mainGRP { get; set; }
        public int subAGRP { get; set; }
        public int subBGRP { get; set; }
        //public int hrh { get; set; }
        //public int hrj { get; set; }
    }
}