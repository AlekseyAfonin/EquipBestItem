using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightChildListPanel : WidgetNode
{
    public RightChildListPanel(string paramName)
    {
        Node =
            new XElement("ListPanel",
                new XAttribute("WidthSizePolicy", "CoverChildren"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("HorizontalAlignment", "Left"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("IsVisible", true), //TODO Check
                new XElement("Children",
                    new RightSliderWidget(paramName),
                    new RightRichTextWidget(paramName),
                    new RightButtonWidget(paramName)));
    }
}