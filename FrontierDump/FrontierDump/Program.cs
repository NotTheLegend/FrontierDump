using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FrontierDump.data;
using FrontierDump.structs;

namespace FrontierDump
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                DumpData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Done");
            Console.Read();
        }
        
        private static void DumpData()
        {
            using (var stream = new MemoryStream(File.ReadAllBytes("mhfinf.bin")))
            using (var reader = new BinaryReader(stream))
            using (var writer = new StreamWriter("InfQuests.csv", false, Encoding.GetEncoding("shift-jis")))
            using(var csv = new CsvWriter(writer))
            {
                var header = reader.ReadInt32();
                if (header != 0x1A666E69)
                {
                    Console.WriteLine("mhfinf.bin still packed!");
                    return;
                }
                Data.initiate();//Load ItemIds

                stream.Seek(16, SeekOrigin.Begin);
                var _pointer = reader.ReadInt32();
                var _loc = reader.ReadInt32();
                stream.Seek(_pointer, SeekOrigin.Begin);
                var _count = reader.ReadInt16();//No idea if this is actually right, but the data lines up
                stream.Seek(_loc, SeekOrigin.Begin);

                var pointers = new List<KeyValuePair<int, int>>();
                for (var _i = 0; _i < _count; _i++)
                {
                    var unknown = reader.ReadInt16();//sync's with fileName, probably to create a dict lookup table?
                    var num = reader.ReadInt16();
                    var loc = reader.ReadInt32();
                    pointers.Add(new KeyValuePair<int,int>(loc, num));
                }

                var locations = new List<int>();
                foreach (var (key, value) in pointers)
                {
                    stream.Seek(key, SeekOrigin.Begin);
                    for (var o = 0; o < value; o++)
                    {
                        var loc = reader.ReadInt32();
                        if (loc != 0) locations.Add(loc);
                    }
                }
                //Each quest is 352 bytes
                var len = locations.Count;
                var quests = new List<Quest>();
                for (var i = 0; i < len; i++)
                {
                    stream.Seek(locations[i], SeekOrigin.Begin);
                    var data = new Quest();
                    data.unk1 = reader.ReadByte();
                    data.unk2 = reader.ReadByte();
                    data.unk3 = reader.ReadByte();
                    data.unk4 = reader.ReadByte();
                    data.Stars = reader.ReadByte() + " Stars";
                    data.unk5 = reader.ReadByte();
                    data.CourseType = GetName(Data.Courses, reader.ReadByte(), "x2");
                    data.unk7 = reader.ReadByte();
                    data.HR = reader.ReadByte();//HR, GR is stored else where
                    data.unk9 = reader.ReadByte();// High HR quest?, Travel Quest?, Superior Quest?
                    data.unk10 = reader.ReadByte();// 0:1:2:3 = ???, 7 = GR?, 8 = unused?, 9 = Travel quests?
                    data.PlayerCap = GetName(Data.PlayerCap, reader.ReadByte(), "x2");
                    data.Fee = reader.ReadInt32();
                    data.ZennyMain = reader.ReadInt32();
                    data.ZennyKo = reader.ReadInt32();
                    data.ZennySubA = reader.ReadInt32();
                    data.ZennySubB = reader.ReadInt32();
                    data.Time = reader.ReadInt32();//In frames(30 fps), ei: 90000 frames = 3000 seconds = 50 minutes
                    data.Location = GetName(Data.Locations, reader.ReadInt32(), "x8");
                    data.unk13 = reader.ReadByte();
                    data.unk14 = reader.ReadByte();
                    data.unk15 = reader.ReadByte();
                    data.unk16 = reader.ReadByte();
                    data.unk17 = reader.ReadByte();
                    data.unk18 = reader.ReadByte();
                    data.FileName = reader.ReadInt16();
                    data.MainGoalType = GetName(Data.Objectives, reader.ReadInt32(), "x8");
                    data.MainGoalTarget = GetGoals(data.MainGoalType, reader);
                    data.MainGoalCount = reader.ReadInt16();
                    data.SubAGoalType = GetName(Data.Objectives, reader.ReadInt32(), "x8");
                    data.SubAGoalTarget = GetGoals(data.SubAGoalType, reader);
                    data.SubAGoalCount = reader.ReadInt16();
                    data.SubBGoalType = GetName(Data.Objectives, reader.ReadInt32(), "x8");
                    data.SubBGoalTarget = GetGoals(data.SubBGoalType, reader);
                    data.SubBGoalCount = reader.ReadInt16();
                    stream.Seek(2, SeekOrigin.Current);
                    data.HR_Join = reader.ReadByte();//HR to join???
                    stream.Seek(3, SeekOrigin.Current);
                    data.HR_Host = reader.ReadByte();//HR to host???, these 2 have an effect on that value but there's no correlation unless im missing something 
                    stream.Seek(92-7, SeekOrigin.Current);
                    data.GRP_Main = reader.ReadInt32();
                    data.GRP_SubA = reader.ReadInt32();
                    data.GRP_SubB = reader.ReadInt32();
                    stream.Seek(20, SeekOrigin.Current);//Data in here
                    stream.Seek(124, SeekOrigin.Current);//All empty bytes
                    data.Title = StringFromPointer(reader);
                    data.MainObj = StringFromPointer(reader);
                    data.SubObjA = StringFromPointer(reader);
                    data.SubObjB = StringFromPointer(reader);
                    data.Completion = StringFromPointer(reader);
                    data.Failure = StringFromPointer(reader);
                    data.Client = StringFromPointer(reader);
                    data.Summary = StringFromPointer(reader);
                    quests.Add(data);

                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"{i}/{len+1} : {i*100/len+1:f2}%     ");
                }
                Console.SetCursorPosition(0, 1);
                
                csv.Configuration.Delimiter = "\t";
                csv.WriteRecords(quests);
            }
        }

        private static string GetName(Dictionary<int, string> list, int id, string format)
        {
            list.TryGetValue(id, out string name);
            return name ?? id.ToString(format);
        }

        private static string GetGoals(string type, BinaryReader reader)
        {
            switch (type)
            {
                case "Hunt":
                case "Slay":
                case "Slay or Damage":
                case "Damage":
                case "Capture":
                case "Break Part":
                    return GetName(Data.Monsters, reader.ReadInt16(), "x4");
                case "Deliver":
                case "Deliver Flag":
                    return GetName(Data.ItemIDs, reader.ReadInt16(), "x4");
                default:
                    return reader.ReadInt16().ToString("x4");
            }
        }

        private static string StringFromPointer(BinaryReader reader)
        {
            var pointer = reader.ReadInt32();
            var position = reader.BaseStream.Position;
            if (pointer > reader.BaseStream.Length || pointer < 0)
                return "";
            reader.BaseStream.Seek(pointer, SeekOrigin.Begin);
            var text = ReadNullterminatedString(reader, Encoding.GetEncoding("shift-jis"));
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
            return text.Replace("\n", "<NL>");
        }

        private static string ReadNullterminatedString(BinaryReader reader, Encoding encoding)
        {
            var byteList = new List<byte>();
            var b = reader.ReadByte();
            while (b != 0x00 && reader.BaseStream.Position != reader.BaseStream.Length)
            {
                byteList.Add(b);
                b = reader.ReadByte();
            }
            return encoding.GetString(byteList.ToArray());
        }
    }
}
