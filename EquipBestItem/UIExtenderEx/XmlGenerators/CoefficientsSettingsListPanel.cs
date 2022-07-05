using System.Xml.Linq;
using EquipBestItem.UIExtenderEx.XmlGenerators;
using EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

namespace EquipBestItem.UIExtenderEx;

public class CoefficientsSettingsListPanel : WidgetNode
{
    public CoefficientsSettingsListPanel(string paramName)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("Id", $"{paramName}Row"),
                new XAttribute("IsEnabled", $"@IsEnabled{paramName}"),
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("HorizontalAlignment", "Center"),
                new XAttribute("VerticalAlignment", "Top"),
                new XAttribute("LayoutImp.LayoutMethod", "HorizontalLeftToRight"),
                new XAttribute("MarginTop", $"!EBI.MainWindow.MarginTop"),
                new XAttribute("Sprite", $"BlankWhiteSquare_9"),
                new XAttribute("Color", $"#8B5C2200"),
                new XElement("Children", new LeftListPanel(paramName), new RightListPanel(paramName)));
    }
}