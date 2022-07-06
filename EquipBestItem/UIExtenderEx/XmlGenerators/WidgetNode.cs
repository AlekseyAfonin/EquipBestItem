using System.Xml.Linq;
using JetBrains.Annotations;

namespace EquipBestItem.UIExtenderEx.XmlGenerators;

internal abstract class WidgetNode
{
    internal XElement Node { get; init; }
}