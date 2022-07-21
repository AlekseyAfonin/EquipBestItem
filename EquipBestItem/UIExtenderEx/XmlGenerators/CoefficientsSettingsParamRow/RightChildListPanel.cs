using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightChildListPanel : WidgetNode
{
    internal RightChildListPanel(ItemParams itemParam)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("Id", "NumericOption"),
                new XAttribute("WidthSizePolicy", "CoverChildren"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("HorizontalAlignment", "Left"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("IsVisible", true), //TODO Check
                new XElement("Children",
                    new RightSliderWidget(itemParam).Node,
                    new RightRichTextWidget(itemParam).Node,
                    new RightButtonWidget(itemParam).Node));
    }
}