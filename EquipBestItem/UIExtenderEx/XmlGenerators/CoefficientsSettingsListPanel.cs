using System.Xml.Linq;
using EquipBestItem.Models.Enums;
using EquipBestItem.UIExtenderEx.XmlGenerators;
using EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

namespace EquipBestItem.UIExtenderEx;

internal class CoefficientsSettingsListPanel : WidgetNode
{
    internal CoefficientsSettingsListPanel(TestParams itemParam)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("Id", $"{itemParam}Row"),
                new XAttribute("IsHidden", $"@{itemParam}IsHidden"),
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("HorizontalAlignment", "Center"),
                new XAttribute("VerticalAlignment", "Top"),
                new XAttribute("LayoutImp.LayoutMethod", "HorizontalLeftToRight"),
                new XAttribute("MarginTop", $"!EBI.MainWindow.MarginTop"),
                new XAttribute("Sprite", $"BlankWhiteSquare_9"),
                new XAttribute("Color", $"#8B5C2200"),
                new XElement("Children", new LeftListPanel(itemParam).Node, new RightListPanel(itemParam).Node));
    }
}