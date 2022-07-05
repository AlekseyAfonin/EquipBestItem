using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightSliderWidget : WidgetNode
{
    public RightSliderWidget(string paramName)
    {
        Node =
            new XElement("SliderWidget",
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", 138),
                new XAttribute("SuggestedHeight", 42),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("DoNotUpdateHandleSize", true),
                new XAttribute("Filler", "Filler"),
                new XAttribute("Handle", "SliderHandle"),
                new XAttribute("IsDiscrete", "true"),
                new XAttribute("MaxValueFloat", 100.0),
                new XAttribute("MinValueFloat", 0.0),
                new XAttribute("ValueFloat", $"@{paramName}ArmorValue"),
                new XElement("Children",
                    new RightSliderWidgetFirstWidget(),
                    new RightSliderWidgetSecondWidget(),
                    new RightSliderWidgetThirdWidget(),
                    new RightSliderWidgetImageWidget()));
    }
}