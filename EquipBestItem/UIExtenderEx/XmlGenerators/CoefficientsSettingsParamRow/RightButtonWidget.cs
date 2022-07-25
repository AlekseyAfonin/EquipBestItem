using System.Xml.Linq;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;

internal class RightButtonWidget : WidgetNode
{
    internal RightButtonWidget(ItemParams itemParam)
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
                new XAttribute("Command.Click", "ExecuteCheckboxSetDefault"),
                new XAttribute("CommandParameter.Click", $"{itemParam}"),
                new XAttribute("IsSelected", $"@{itemParam}IsDefault"),
                new XAttribute("ToggleIndicator", "ToggleIndicator"),
                new XAttribute("UpdateChildrenStates", "true"),
                new XElement("Children",
                    new XElement("HintWidget",
                        new XAttribute("DoNotAcceptEvents", true),
                        new XAttribute("DataSource", "{CheckboxHint}"),
                        new XAttribute("WidthSizePolicy", "StretchToParent"),
                        new XAttribute("HeightSizePolicy", "StretchToParent"),
                        new XAttribute("Command.HoverBegin", "ExecuteBeginHint"),
                        new XAttribute("Command.HoverEnd", "ExecuteEndHint")),
                    new RightButtonWidgetImageWidget().Node));
    }
}