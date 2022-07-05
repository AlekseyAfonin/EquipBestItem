using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators;

public abstract class WidgetNode
{
    public XElement Node { get; protected init; }
}