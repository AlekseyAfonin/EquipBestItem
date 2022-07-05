using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightRichTextWidget : WidgetNode
{
    public RightRichTextWidget(string paramName)
    {
        Node = 
            new XElement("RichTextWidget",
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "50"),
                new XAttribute("HeightSizePolicy", "CoverChildren"),
                new XAttribute("MarginLeft", "!EBI.Slider.Value.MarginLeft"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("Brush", "SPOptions.OptionName.Text"),
                new XAttribute("IsEnabled", "false"),
                new XAttribute("Text", $"@{paramName}ValueText"));
    }
}