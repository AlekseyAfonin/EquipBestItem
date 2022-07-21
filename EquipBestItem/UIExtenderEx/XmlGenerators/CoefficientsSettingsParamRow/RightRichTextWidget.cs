using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightRichTextWidget : WidgetNode
{
    internal RightRichTextWidget(ItemParams itemParam)
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
                new XAttribute("Text", $"@{itemParam}PercentText"));
    }
}