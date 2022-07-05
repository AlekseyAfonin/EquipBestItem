using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightButtonWidgetImageWidget : WidgetNode
{
    public RightButtonWidgetImageWidget()
    {
        Node =
            new XElement("ImageWidget",
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", "StretchToParent"),
                new XAttribute("Brush", "SPOptions.Checkbox.Full.Button"));
    }
}