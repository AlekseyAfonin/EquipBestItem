using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class LeftRichTextWidget : WidgetNode
{
    internal LeftRichTextWidget(ItemParams itemParam)
    {
        Node =
            new XElement("RichTextWidget",
                new XAttribute("WidthSizePolicy", "StretchToParent"),
                new XAttribute("HeightSizePolicy", $"CoverChildren"),
                new XAttribute("MarginRight", 0),
                new XAttribute("VerticalAlignment", $"Center"),
                new XAttribute("Brush", "SPOptions.OptionName.Text"),
                new XAttribute("IsEnabled", false),
                new XAttribute("Text", $"@{itemParam}Text"));
    }
}