using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class LeftChildListPanel : WidgetNode
{
    public LeftChildListPanel(string paramName)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("WidthSizePolicy", "CoverChildren"),
                new XAttribute("HeightSizePolicy", $"CoverChildren"),
                new XAttribute("HorizontalAlignment", "Left"),
                new XAttribute("VerticalAlignment", $"Center"),
                new XAttribute("IsVisible", true), //Todo Check
                new XElement("Children", new LeftRichTextWidget(paramName)));
    }
}