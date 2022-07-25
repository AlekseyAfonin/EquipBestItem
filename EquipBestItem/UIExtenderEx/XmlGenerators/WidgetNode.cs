using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators;

internal abstract class WidgetNode
{
    internal XElement Node { get; init; }
}