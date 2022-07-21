using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class LeftWidget : WidgetNode
{
    internal LeftWidget(ItemParams itemParam)
    {
        Node =
            new XElement("Widget",
                new XAttribute("DoNotAcceptEvents", true),
                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedHeight", $"!EBI.Slider.Height"),
                new XAttribute("VerticalAlignment", $"Center"),
                new XAttribute("MarginLeft", $"!EBI.Slider.MarginLeft"),
                new XElement("Children", new LeftChildListPanel(itemParam).Node));
    }
}