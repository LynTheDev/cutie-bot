using System.Reflection;
using System.Xml.Serialization;

namespace CutieBot.Source.XML;

public static class ReadXML
{
    public static Dictionary<string, object> GetConfigDict()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/Config.xml");

        XmlSerializer serialiser = new XmlSerializer(typeof(ConfigModel));
        
        using Stream stream = new FileStream(path, FileMode.Open);
        ConfigModel item = (ConfigModel)serialiser.Deserialize(stream);

        return new Dictionary<string, object>()
        {
            {"Token", item.Token},
            {"Name", item.Name},

            {"NID", item.NID},
            {"CID", item.CID},

            {"Compliments", item.Compliments}
        };
    }

    public static void NewCompliment(string compliment)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/Config.xml");

        XmlSerializer serialiser = new XmlSerializer(typeof(ConfigModel));
        ConfigModel modelChange;

        using (Stream stream = new FileStream(path, FileMode.Open))
        {
            modelChange = (ConfigModel)serialiser.Deserialize(stream);
        }

        modelChange.Compliments.Add(compliment);

        using StreamWriter writer = new StreamWriter(path);
        serialiser.Serialize(writer, modelChange);
    }
}
