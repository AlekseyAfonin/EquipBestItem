using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class LeftListPanel : WidgetNode
{
    internal LeftListPanel(TestParams itemParam)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("Id", $"LeftListPanel{itemParam}"),
                new XAttribute("DoNotAcceptEvents", true),
                new XAttribute("WidthSizePolicy", $"Fixed"),
                new XAttribute("SuggestedWidth", 200),
                new XAttribute("HeightSizePolicy", $"CoverChildren"),
                new XAttribute("HorizontalAlignment", $"Left"),
                new XAttribute("VerticalAlignment", $"Top"),
                new XAttribute("MarginLeft", 0),
                new XAttribute("MarginRight", 0),
                new XAttribute("LayoutImp.LayoutMethod", $"HorizontalLeftToRight"),
                new XAttribute("Sprite", $"BlankWhiteSquare_9"),
                new XAttribute("Color", $"#8B5C2200"),
                new XElement("Children", new LeftWidget(itemParam).Node));
    }
}