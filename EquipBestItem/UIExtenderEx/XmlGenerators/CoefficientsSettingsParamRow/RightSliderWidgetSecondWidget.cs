using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightSliderWidgetSecondWidget : WidgetNode
{
    internal RightSliderWidgetSecondWidget()
    {
        Node =
            new XElement("Widget",
                new XAttribute("Id", "Filler"),
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "345"),
                new XAttribute("SuggestedHeight", "35"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("Sprite", @"SPGeneral\SPOptions\standart_slider_fill"),
                new XAttribute("ClipContents", "true"),
                new XElement("Children",
                    new RightSliderWidgetSecondWidgetChildWidget().Node));
    }
}