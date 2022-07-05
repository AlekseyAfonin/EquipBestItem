using System.Xml.Linq;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

public class RightButtonWidget : WidgetNode
{
    public RightButtonWidget(string paramName)
    {
        Node = 
            new XElement("ButtonWidget",
                new XAttribute("DoNotPassEventsToChildren", "true"),
                new XAttribute("WidthSizePolicy", "Fixed"),
                new XAttribute("HeightSizePolicy", "Fixed"),
                new XAttribute("SuggestedWidth", "40"),
                new XAttribute("SuggestedHeight", "40"),
                new XAttribute("MarginLeft", "!EBI.CheckBox.MarginLeft"),
                new XAttribute("Brush", "SPOptions.Checkbox.Empty.Button"),
                new XAttribute("VerticalAlignment", "Center"),
                new XAttribute("HorizontalAlignment", "Right"),
                new XAttribute("ButtonType", "Toggle"),
                new XAttribute("Command.Click", "ExecuteValueDefault"),
                new XAttribute("CommandParameter.Click", $"{paramName}"),
                new XAttribute("IsSelected", $"@Is{paramName}ValueIsDefault"),
                new XAttribute("ToggleIndicator", "ToggleIndicator"),
                new XAttribute("UpdateChildrenStates", "true"),
                new XElement("Children", new RightButtonWidgetImageWidget()));
    }
}