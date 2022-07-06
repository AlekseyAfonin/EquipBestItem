using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightButtonWidgetImageWidget : WidgetNode
{
    internal RightButtonWidgetImageWidget()
    {
        Node =
            new XElement("ImageWidget",
                new XAttribute("Id", "ToggleIndicator"),
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", "StretchToParent"),
                new XAttribute("Brush", "SPOptions.Checkbox.Full.Button"));
    }
}