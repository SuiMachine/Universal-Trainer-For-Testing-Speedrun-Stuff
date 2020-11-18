using Flying47.Structs;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Flying47
{
	public static class ProgramConfig
	{
		//Probably would be easier with serialization...
		private const string CONFIG_FILE_NAME = "universal_trainer_for_testing_conf.xml";

		public static bool LoadFullConfig(out KeySet set)
		{
			if (File.Exists(CONFIG_FILE_NAME))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(CONFIG_FILE_NAME);
				XmlNode rootNode = doc["Config"];
				set = new KeySet();

				if (rootNode["Keys"] != null)
				{
					XmlNode keyNode = rootNode["Keys"];

					if (keyNode["Forward"] == null || keyNode["Up"] == null || keyNode["Down"] == null || keyNode["Store"] == null || keyNode["Load"] == null)
						return false;

					if (!Enum.TryParse<Keys>(keyNode["Forward"].InnerText, out set.Forward))
						return false;

					if (!Enum.TryParse<Keys>(keyNode["Up"].InnerText, out set.Up))
						return false;

					if (!Enum.TryParse<Keys>(keyNode["Down"].InnerText, out set.Down))
						return false;

					if (!Enum.TryParse<Keys>(keyNode["Store"].InnerText, out set.StorePosition))
						return false;

					if (!Enum.TryParse<Keys>(keyNode["Load"].InnerText, out set.LoadPosition))
						return false;
				}

				if (rootNode["Other"] != null)
				{
					XmlNode otherNode = rootNode["Other"];

					if (otherNode["TopMost"] == null)
						return true;

					if (!bool.TryParse(otherNode["TopMost"].InnerText, out set.IsTopMost))
						return false;
				}

				return true;
			}
			else
			{
				set = null;
				return false;
			}
		}

		internal static bool SaveConfig(KeySet keysSet)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				XmlNode rootNode = doc.CreateElement("Config");
				doc.AppendChild(rootNode);

				XmlNode keys = doc.CreateElement("Keys");
				rootNode.AppendChild(keys);

				XmlNode other = doc.CreateElement("Other");
				rootNode.AppendChild(other);

				XmlNode kForwardNode = doc.CreateElement("Forward");
				kForwardNode.InnerText = keysSet.Forward.ToString();
				keys.AppendChild(kForwardNode);

				XmlNode kUpNode = doc.CreateElement("Up");
				kUpNode.InnerText = keysSet.Up.ToString();
				keys.AppendChild(kUpNode);

				XmlNode kDownNode = doc.CreateElement("Down");
				kDownNode.InnerText = keysSet.Down.ToString();
				keys.AppendChild(kDownNode);

				XmlNode kStoreNode = doc.CreateElement("Store");
				kStoreNode.InnerText = keysSet.StorePosition.ToString();
				keys.AppendChild(kStoreNode);

				XmlNode kLoadNode = doc.CreateElement("Load");
				kLoadNode.InnerText = keysSet.LoadPosition.ToString();
				keys.AppendChild(kLoadNode);

				XmlNode oTopMost = doc.CreateElement("TopMost");
				oTopMost.InnerText = keysSet.IsTopMost.ToString();
				other.AppendChild(oTopMost);

				doc.Save(CONFIG_FILE_NAME);

				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

		}
	}
}
