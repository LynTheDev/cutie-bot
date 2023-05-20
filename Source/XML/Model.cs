using System.Xml.Serialization;

namespace CutieBot.Source.XML;

[XmlRoot("Config")]
public class ConfigModel
{
    [XmlElement("Token")]
    public string Token { get; set; }

    [XmlElement("NID")]
    public ulong NID { get; set; }

    [XmlElement("CID")]
    public ulong CID { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlArray("Compliments")]
    [XmlArrayItem("value")]
    public List<string> Compliments { get; set; }
}
