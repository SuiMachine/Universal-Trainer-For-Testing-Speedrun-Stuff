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
		public static string ActiveConfig { get; private set; }

		public static bool LoadFullConfig(out KeySet set)
		{
			string LOCAL_CONFIG_FILE_NAME = "universal_trainer_for_testing_conf.xml";
			string CONFIG_FILE_NAME = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Universal_Trainer_FTST", LOCAL_CONFIG_FILE_NAME);

			ActiveConfig = File.Exists(LOCAL_CONFIG_FILE_NAME) ? LOCAL_CONFIG_FILE_NAME : CONFIG_FILE_NAME;

			if (File.Exists(ActiveConfig))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(ActiveConfig);
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

					if (otherNode["TopMost"] == null || otherNode["CheckActiveWindow"] == null)
						return true;

					if (!bool.TryParse(otherNode["TopMost"].InnerText, out set.IsTopMost))
						return false;

					if (!bool.TryParse(otherNode["CheckActiveWindow"].InnerText, out set.CheckActiveWindow))
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


				XmlNode oActiveWindow = doc.CreateElement("CheckActiveWindow");
				oActiveWindow.InnerText = keysSet.CheckActiveWindow.ToString();
				other.AppendChild(oActiveWindow);

				var dir = Directory.GetParent(ActiveConfig).FullName;
				if(!Directory.Exists(dir))
					Directory.CreateDirectory(dir);
				doc.Save(ActiveConfig);
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
