using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightSliderWidget : WidgetNode
{
    internal RightSliderWidget(TestParams itemParam)
    {
        Node =
            new XElement("CoefficientsSliderWidget",
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "138"),
                new XAttribute("SuggestedHeight", "42"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("DoNotUpdateHandleSize", "true"),
                //new XAttribute("UpdateValueOnRelease", true),
                new XAttribute("Filler", "Filler"),
                new XAttribute("Handle", "SliderHandle"),
                new XAttribute("IsDiscrete", "true"),
                new XAttribute("MaxValueFloat", "100.0"),
                new XAttribute("MinValueFloat", "0.0"),
                new XAttribute("ValueFloatReleased", $"@{itemParam}"),
                new XAttribute("ValueFloat", $"@{itemParam}Value"),
                new XElement("Children",
                    new RightSliderWidgetFirstWidget().Node,
                    new RightSliderWidgetSecondWidget().Node,
                    new RightSliderWidgetThirdWidget().Node,
                    new RightSliderWidgetImageWidget().Node));
    }
}