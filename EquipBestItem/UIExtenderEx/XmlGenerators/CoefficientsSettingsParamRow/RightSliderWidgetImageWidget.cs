using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightSliderWidgetImageWidget : WidgetNode
{
    internal RightSliderWidgetImageWidget()
    {
        Node =
            new XElement("ImageWidget",
                new XAttribute("Id", "SliderHandle"),
                new XAttribute("DoNotAcceptEvents", "true"),
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "14"),
                new XAttribute("SuggestedHeight", "38"),
                new XAttribute("HorizontalAlignment", "Left"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("Brush", "SPOptions.Slider.Handle"));
    }
}