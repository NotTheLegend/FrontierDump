using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
                    var unkown = reader.ReadInt16();
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
                    data.level = reader.ReadByte();
                    data.unk5 = reader.ReadByte();
                    data.courseType = reader.ReadByte();
                    data.unk7 = reader.ReadByte();
                    data.unk8 = reader.ReadByte();
                    data.unk9 = reader.ReadByte();
                    data.unk10 = reader.ReadByte();
                    data.unk11 = reader.ReadByte();
                    data.fee = reader.ReadInt32();
                    data.zennyMain = reader.ReadInt32();
                    data.zennyKo = reader.ReadInt32();
                    data.zennySubA = reader.ReadInt32();
                    data.zennySubB = reader.ReadInt32();
                    data.time = reader.ReadInt32();
                    data.map = GetName(Data.Locations, reader.ReadInt32(), "x8");
                    data.unk13 = reader.ReadByte();
                    data.unk14 = reader.ReadByte();
                    data.unk15 = reader.ReadByte();
                    data.unk16 = reader.ReadByte();
                    data.unk17 = reader.ReadByte();
                    data.unk18 = reader.ReadByte();
                    data.fileName = reader.ReadInt16();
                    data.mainGoalType = GetName(Data.Objectives, reader.ReadInt32(), "x8");
                    switch (data.mainGoalType)
                    {
                        case "Hunt":
                        case "Slay":
                        case "Slay or Damage":
                        case "Damage":
                        case "Capture":
                        case "Break Part":
                            data.mainGoalTarget = GetName(Data.Monsters, reader.ReadByte(), "x2");
                            reader.ReadByte();//Dead Space due to items being 2 bytes
                            break;
                        case "Deliver":
                            data.mainGoalTarget = GetName(Data.ItemIDs, reader.ReadInt16(), "x4");
                            break;
                        default:
                            data.mainGoalTarget = reader.ReadInt16().ToString("x4");
                            break;
                    }
                    data.mainGoalCount = reader.ReadInt16();
                    data.subAGoalType = GetName(Data.Objectives, reader.ReadInt32(), "x8");
                    switch (data.subAGoalType)
                    {
                        case "Hunt":
                        case "Slay":
                        case "Slay or Damage":
                        case "Damage":
                        case "Capture":
                        case "Break Part":
                            data.subAGoalTarget = GetName(Data.Monsters, reader.ReadByte(), "x2");
                            reader.ReadByte();//Dead Space due to items being 2 bytes
                            break;
                        case "Deliver":
                            data.subAGoalTarget = GetName(Data.ItemIDs, reader.ReadInt16(), "x4");
                            break;
                        default:
                            data.subAGoalTarget = reader.ReadInt16().ToString("x4");
                            break;
                    }
                    data.subAGoalCount = reader.ReadInt16();
                    data.subBGoalType = GetName(Data.Objectives, reader.ReadInt32(), "x8");
                    switch (data.subBGoalType)
                    {
                        //Break Part
                        case "Hunt":
                        case "Slay":
                        case "Slay or Damage":
                        case "Damage":
                        case "Capture":
                        case "Break Part":
                            data.subBGoalTarget = GetName(Data.Monsters, reader.ReadByte(), "x2");
                            reader.ReadByte();//Dead Space due to items being 2 bytes
                            break;
                        case "Deliver":
                            data.subBGoalTarget = GetName(Data.ItemIDs, reader.ReadInt16(), "x4");
                            break;
                        default:
                            data.subBGoalTarget = reader.ReadInt16().ToString("x4");
                            break;
                    }
                    data.subBGoalCount = reader.ReadInt16();
                    stream.Seek(92, SeekOrigin.Current);
                    data.mainGRP = reader.ReadInt32();
                    data.subAGRP = reader.ReadInt32();
                    data.subBGRP = reader.ReadInt32();
                    stream.Seek(144, SeekOrigin.Current);
                    data.title = StringFromPointer(reader);
                    data.textMain = StringFromPointer(reader);
                    data.textSubA = StringFromPointer(reader);
                    data.textSubB = StringFromPointer(reader);
                    data.textSubC = StringFromPointer(reader);
                    data.textSubD = StringFromPointer(reader);
                    data.textSubE = StringFromPointer(reader);
                    data.textSubF = StringFromPointer(reader);
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
            if (!list.TryGetValue(id, out string name))
                name = id.ToString(format);
            return name;
        }

        private static string StringFromPointer(BinaryReader reader)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length) return "";
            var pos = reader.BaseStream.Position;
            var off = reader.ReadInt32();
            if (off > reader.BaseStream.Length || off < 0) return "";
            reader.BaseStream.Seek(off, SeekOrigin.Begin);
            var str = ReadNullterminatedString(reader, Encoding.GetEncoding("shift-jis")).Replace("\n", "<NL>");
            reader.BaseStream.Seek(pos + 4, SeekOrigin.Begin);//Advance stream by 4 after reading pointer
            return str;
        }

        private static string ReadNullterminatedString(BinaryReader reader, Encoding encoding)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length) return "";
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
