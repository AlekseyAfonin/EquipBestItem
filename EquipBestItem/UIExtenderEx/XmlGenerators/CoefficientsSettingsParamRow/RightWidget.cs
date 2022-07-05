using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightWidget : WidgetNode
{
    public RightWidget(string paramName)
    {
        Node =
            new XElement("Widget",
                new XAttribute("DoNotAcceptEvents", "true"),
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedHeight", "!EBI.Slider.Height"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("MarginLeft", "!EBI.Slider.MarginLeft"),
                new XElement("Children",
                    new RightChildListPanel(paramName)));
    }
}