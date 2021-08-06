namespace FrontierDump.structs
{
    public class Quest
    {
        public string Title { get; set; }
        public string MainObj { get; set; }
        public string SubObjA { get; set; }
        public string SubObjB { get; set; }
        public string Completion { get; set; }
        public string Failure { get; set; }
        public string Client { get; set; }
        public string Summary { get; set; }

        public byte unk1 { get; set; }
        public byte unk2 { get; set; }
        public byte unk3 { get; set; }
        public byte unk4 { get; set; }
        public string Stars { get; set; }
        public byte unk5 { get; set; }
        public string CourseType { get; set; }
        public byte unk7 { get; set; }
        public byte HR { get; set; }
        public byte unk9 { get; set; }
        public byte unk10 { get; set; }
        public string PlayerCap { get; set; }
        public int Fee { get; set; }
        public int ZennyMain { get; set; }
        public int ZennyKo { get; set; }
        public int ZennySubA { get; set; }
        public int ZennySubB { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
        public byte unk13 { get; set; }
        public byte unk14 { get; set; }
        public byte unk15 { get; set; }
        public byte unk16 { get; set; }
        public byte unk17 { get; set; }
        public byte unk18 { get; set; }
        public int FileName { get; set; }
        public string MainGoalType { get; set; }
        public string MainGoalTarget { get; set; }
        public int MainGoalCount { get; set; }
        public string SubAGoalType { get; set; }
        public string SubAGoalTarget { get; set; }
        public int SubAGoalCount { get; set; }
        public string SubBGoalType { get; set; }
        public string SubBGoalTarget { get; set; }
        public int SubBGoalCount { get; set; }
        public int GRP_Main { get; set; }
        public int GRP_SubA { get; set; }
        public int GRP_SubB { get; set; }
        public int HR_Host { get; set; }
        public int HR_Join { get; set; }
    }
}