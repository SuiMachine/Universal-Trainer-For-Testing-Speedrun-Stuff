using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
#if BUILD64
using MemoryReads64;
using UniversalInt = System.Int64;
#else
using MemoryReads;
using UniversalInt = System.Int32;
#endif

namespace Flying47
{
    static class ConfigLoader
    {
#if BUILD64
        const bool Is64BitVersion = true;
#else
        const bool Is64BitVersion = false;
#endif

        public static bool LoadFullConfig(out string ProcessName, out PositionSet positionSet, out Pointer sinAlpha, out bool sinInverted, out Pointer cosAlpha, out bool cosInverted, out float MoveXYAmount, out float MoveZAmount)
        {
            string XML_FILE_NAME = "universal_trainer_for_testing.xml";

            if (!File.Exists(XML_FILE_NAME))
            {
                OpenFileDialog fd = new OpenFileDialog
                {
                    Filter = "*.xml|*.xml",
                    Multiselect = false,
                    InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Configs")
                };
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                    XML_FILE_NAME = fd.FileName;
                else
                {
                    ProcessName = "";
                    positionSet = new PositionSet();
                    sinAlpha = null;
                    sinInverted = false;
                    cosAlpha = null;
                    cosInverted = false;
                    MoveXYAmount = 1;
                    MoveZAmount = 1;
                    return false;
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(XML_FILE_NAME);
            XmlNode rootNode = doc["Config"];
            bool Requires64Bit = bool.Parse(rootNode.Attributes["x64"].InnerText);
            if (!Is64BitVersion && Requires64Bit)
                throw new Exception("Following config requires 64bit executable!");

            ProcessName = rootNode["ProcessName"].InnerText;
            positionSet = rootNode["Position"].ToPositionSet();

            if(rootNode["SinAlpha"] != null || rootNode["CosAlpha"] != null)
            {
                sinAlpha = rootNode["SinAlpha"].ToPointer();
                sinInverted = bool.Parse(rootNode["SinAlpha"].Attributes["Inverted"].InnerText);
                cosAlpha = rootNode["CosAlpha"].ToPointer();
                cosInverted = bool.Parse(rootNode["SinAlpha"].Attributes["Inverted"].InnerText);
            }
            else
            {
                sinAlpha = new Pointer(0x0);
                sinInverted = false;
                cosAlpha = new Pointer(0x0);
                cosInverted = false;
            }

            MoveXYAmount = float.Parse(rootNode["MoveXYAmount"].InnerText);
            MoveZAmount = float.Parse(rootNode["MoveZAmount"].InnerText);
            return true;
        }

        private static PositionSet ToPositionSet(this XmlNode node)
        {
            Pointer ptr = node["Pointer"].ToPointer();
            bool isXZy = bool.Parse(node["XZY"].InnerText);
            return new PositionSet(ptr, isXZy);
        }

        private static Pointer ToPointer(this XmlNode node)
        {
            string content = node.InnerText;
            if (content.Contains(','))
            {
                string[] split = content.Split(',');
                UniversalInt[] Offsets = new UniversalInt[split.Length - 1];
                UniversalInt baseAddress = HexDecParse(split[0].Trim());
                for(int i=0; i<Offsets.Length;i++)
                {
                    Offsets[i] = HexDecParse(split[i+1].Trim());
                }
                return new Pointer(baseAddress, Offsets);
            }
            else
            {
                return new Pointer(HexDecParse(content), new UniversalInt[0]);
            }
        }

        private static UniversalInt HexDecParse(string text)
        {
            if(!text.StartsWith("0x"))
            {
                return UniversalInt.Parse(text);
            }
            else
            {
                text = text.Substring(2, text.Length - 2);
#if BUILD64
                return Convert.ToInt64(text, 16);
#else
                return Convert.ToInt32(text, 16);
#endif
            }
        }
    }
}