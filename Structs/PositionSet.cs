using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Flying47.Structs
{
	[Serializable]
	public class PositionSet_Coordinates
	{
		[XmlElement]
		public string Name { get; set; }

		[XmlElement]
		public float X { get; set; }
		[XmlElement]

		public float Y { get; set; }
		[XmlElement]
		public float Z { get; set; }

		public PositionSet_Coordinates()
		{
			Name = "";
			X = 0;
			Y = 0;
			Z = 0;
		}

		public PositionSet_Coordinates(string Name, float X, float Y, float Z)
		{
			this.Name = Name;
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
	}

	[Serializable]
	public class PositionSets
	{
		[XmlArrayItem]
		public List<PositionSet_Coordinates> PositionsList { get; set; }

		public static PositionSets Load(string FilePath)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(PositionSets));
			FileStream fs = new FileStream(FilePath, FileMode.Open);
			PositionSets obj = (PositionSets)serializer.Deserialize(fs);
			fs.Close();
			return obj;
		}

		public void Save(string FilePath)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(PositionSets));
			StreamWriter fw = new StreamWriter(FilePath);
			serializer.Serialize(fw, this);
			fw.Close();
		}

		public PositionSets()
		{
			this.PositionsList = new List<PositionSet_Coordinates>();
		}
	}
}
