using System.Xml.Serialization;

namespace CutieBot.Source.XML;

public static class ReadXML
{
    public static string GetToken()
    {
        string token = string.Empty;
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/Config.xml");

        XmlSerializer serialiser = new XmlSerializer(typeof(ConfigModel));
        using (Stream stream = new FileStream(path, FileMode.Open))
        {
            ConfigModel item = (ConfigModel)serialiser.Deserialize(stream);
            token = item.Token;
        }

        return token;
    }
}
