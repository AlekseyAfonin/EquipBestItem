using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightSliderWidgetThirdWidget : WidgetNode
{
    public RightSliderWidgetThirdWidget()
    {
        Node = 
            new XElement("Widget",
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "!EBI.Slider.Width"),
                new XAttribute("SuggestedHeight", 65),
                new XAttribute("HorizontalAlignment", "Center"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("Sprite", @"SPGeneral\SPOptions\standart_slider_frame"),
                new XAttribute("IsEnabled", false));
    }
}