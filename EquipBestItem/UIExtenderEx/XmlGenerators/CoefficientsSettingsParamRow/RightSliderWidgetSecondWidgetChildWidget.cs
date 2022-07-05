using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightSliderWidgetSecondWidgetChildWidget : WidgetNode
{
    public RightSliderWidgetSecondWidgetChildWidget()
    {
        Node = 
            new XElement("Widget",
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "345"),
                new XAttribute("SuggestedHeight", "35"),
                new XAttribute("HorizontalAlignment", "Left"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("Sprite", @"SPGeneral\SPOptions\standart_slider_fill"));
    }
}