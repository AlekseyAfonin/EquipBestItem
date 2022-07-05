using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightListPanel : WidgetNode
{
    public RightListPanel(string paramName)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("Id", $"RightListPanel{paramName}"),
                new XAttribute("DoNotAcceptEvents", "true"),
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("HorizontalAlignment", "Right"),
                new XAttribute("VerticalAlignment", "Top"),
                new XAttribute("MarginLeft", 0),
                new XAttribute("LayoutImp.LayoutMethod", "HorizontalLeftToRight"),
                new XAttribute("Sprite", "BlankWhiteSquare_9"),
                new XAttribute("Color", "#3B5C2200"),
                new XElement("Children",
                    new RightWidget(paramName)));
    }
}