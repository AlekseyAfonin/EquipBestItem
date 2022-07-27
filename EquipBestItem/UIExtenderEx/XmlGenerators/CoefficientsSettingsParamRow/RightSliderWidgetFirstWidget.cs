using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightSliderWidgetFirstWidget : WidgetNode
{
    internal RightSliderWidgetFirstWidget()
    {
        Node =
            new XElement("Widget",
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", 138),
                new XAttribute("SuggestedHeight", 38),
                new XAttribute("HorizontalAlignment", "Center"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("Sprite", @"SPGeneral\SPOptions\standart_slider_canvas"),
                new XAttribute("IsEnabled", false));
    }
}