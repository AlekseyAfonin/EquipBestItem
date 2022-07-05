using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class LeftRichTextWidget : WidgetNode
{
    public LeftRichTextWidget(string paramName)
    {
        Node =
            new XElement("RichTextWidget",
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", $"CoverChildren"),
                new XAttribute("MarginRight", 0),
                new XAttribute("VerticalAlignment", $"Center"),
                new XAttribute("Brush", "SPOptions.OptionName.Text"),
                new XAttribute("IsEnabled", false),
                new XAttribute("Text", $"@{paramName}Text"));
    }
}