using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using MemoryReads64;

namespace Flying47
{
    static class GameConfigLoader
    {
        /// <summary>
        /// Reads pointers and addresses together with properties from Config file.
        /// </summary>
        /// <param name="ProcessName">Outputed name of a process.</param>
        /// <param name="positionSet">Ouutputed set of pointers for 3D position.</param>
        /// <param name="sinAlpha">Outputed pointer to angle SinAlpha (rotation around Z axis).</param>
        /// <param name="sinInverted">Outputed bool value saying whatever the sinAlpha needs to be inverted.</param>
        /// <param name="cosAlpha">Outputed pointer to angle CosAlpha (rotation around Z axis).</param>
        /// <param name="cosInverted">Outputed bool value saying whatever the cosAlpha needs to be inverted.</param>
        /// <param name="MoveXYAmount">Outputed default move aount on X,Y axis. </param>
        /// <param name="MoveZAmount">Outputed default move amount on Z (height) axis.</param>
        /// <returns>Bool depending on whatever the read was sucessfull or not.</returns>
        public static bool LoadFullConfig(out string ProcessName, out PositionSet_Pointer positionSet, out Pointer sinAlpha, out bool sinInverted, out Pointer cosAlpha, out bool cosInverted, out float MoveXYAmount, out float MoveZAmount)
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
                    positionSet = new PositionSet_Pointer();
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

            System.Globalization.NumberFormatInfo frt = new System.Globalization.NumberFormatInfo()
            {
                NumberDecimalSeparator = "."
            };

            MoveXYAmount = float.Parse(rootNode["MoveXYAmount"].InnerText, frt);
            MoveZAmount = float.Parse(rootNode["MoveZAmount"].InnerText, frt);
            return true;
        }

        /// <summary>
        /// Parses position set from XML node.
        /// </summary>
        /// <param name="node">Node from which to parse Position Set.</param>
        /// <returns>PositionSet (set of 3 pointers).</returns>
        private static PositionSet_Pointer ToPositionSet(this XmlNode node)
        {
            Pointer ptr = node["Pointer"].ToPointer();
            int bytegap = 4;
            if (node["ByteGap"] != null)
                bytegap = int.Parse(node["ByteGap"].InnerText);
            bool isXZy = bool.Parse(node["XZY"].InnerText);
            return new PositionSet_Pointer(ptr, bytegap, isXZy);
        }

        /// <summary>
        /// Parses a pointer from XML node.
        /// </summary>
        /// <param name="node">Node from which to parse the pointer from.</param>
        /// <returns>Pointer.</returns>
        private static Pointer ToPointer(this XmlNode node)
        {
            string content = node.InnerText;
            string moduleName = node.Attributes["Module"] != null ? node.Attributes["Module"].InnerText : "";

            if (content.Contains(','))
            {
                string[] split = content.Split(',');
                int[] Offsets = new int[split.Length - 1];
                Int64 baseOffset = HexDecParse(split[0].Trim());
                for(int i=0; i<Offsets.Length;i++)
                {
                    Offsets[i] = HexDecParse(split[i+1].Trim());
                }
                return new Pointer(moduleName, baseOffset, Offsets);
            }
            else
            {
                return new Pointer(moduleName, HexDecParse(content), new int[0]);
            }
        }

        /// <summary>
        /// Checks whatever the string is hexadecimal or not and parses int from it.
        /// </summary>
        /// <param name="text">Text which to parse</param>
        /// <returns>Parsed value</returns>
        private static int HexDecParse(string text)
        {
            if (text == "")
                return 0;
            else if(!text.StartsWith("0x"))
            {
                return int.Parse(text);
            }
            else
            {
                text = text.Substring(2, text.Length - 2);
                return Convert.ToInt32(text, 16);
            }
        }
    }
}