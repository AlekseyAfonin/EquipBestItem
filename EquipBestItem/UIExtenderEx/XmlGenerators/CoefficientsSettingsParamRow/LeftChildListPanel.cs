using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class LeftChildListPanel : WidgetNode
{
    internal LeftChildListPanel(ItemParams itemParam)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("Id", "NumericOption"),
                new XAttribute("WidthSizePolicy", "CoverChildren"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("HorizontalAlignment", "Left"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("IsVisible", true),
                new XElement("Children", new LeftRichTextWidget(itemParam).Node));
    }
}