using System.Xml.Serialization;

namespace CutieBot.Source.XML;

[XmlRoot("Config")]
public class ConfigModel
{
    [XmlElement("Token")]
    public string Token { get; set; }
}
