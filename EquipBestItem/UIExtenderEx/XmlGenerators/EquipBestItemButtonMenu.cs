using System.Xml.Linq;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.UIExtenderEx.XmlGenerators;

internal class EquipBestItemButtonMenu : WidgetNode
{
    internal EquipBestItemButtonMenu(HorizontalAlignment horizontalAlignment)
    {
        if (horizontalAlignment == HorizontalAlignment.Left)
            Node =
                new XElement("ButtonWidget",
                    new XAttribute("PositionXOffset", "!SidePanel.NegativeWidth"),
                    new XAttribute("VisualDefinition", $"{horizontalAlignment}Menu"),
                    new XAttribute("WidthSizePolicy", "Fixed"),
                    new XAttribute("HeightSizePolicy", "Fixed"),
                    new XAttribute("HorizontalAlignment", $"{horizontalAlignment}"),
                    new XAttribute("SuggestedWidth", "20"),
                    new XAttribute("SuggestedHeight", "35"),
                    new XAttribute($"Margin{horizontalAlignment}", "5"),
                    new XAttribute("MarginTop", "100"),
                    new XAttribute("Brush", "Inventory.Tuple.BuyButton"),
                    new XAttribute("Command.Click", $"ExecuteSwitch{horizontalAlignment}Menu"),
                    new XElement("Children",
                        new XElement("HintWidget",
                            new XAttribute("DoNotAcceptEvents", true),
                            new XAttribute("DataSource", "{ButtonMenuShowHint}"),
                            new XAttribute("WidthSizePolicy", "StretchToParent"),
                            new XAttribute("HeightSizePolicy", "StretchToParent"),
                            new XAttribute("Command.HoverBegin", "ExecuteBeginHint"),
                            new XAttribute("Command.HoverEnd", "ExecuteEndHint"))));
        else
            Node =
                new XElement("ButtonWidget",
                    new XAttribute("PositionXOffset", "!SidePanel.Width"),
                    new XAttribute("VisualDefinition", $"{horizontalAlignment}Menu"),
                    new XAttribute("WidthSizePolicy", "Fixed"),
                    new XAttribute("HeightSizePolicy", "Fixed"),
                    new XAttribute("HorizontalAlignment", $"{horizontalAlignment}"),
                    new XAttribute("SuggestedWidth", "20"),
                    new XAttribute("SuggestedHeight", "35"),
                    new XAttribute($"Margin{horizontalAlignment}", "5"),
                    new XAttribute("MarginTop", "100"),
                    new XAttribute("Brush", "Inventory.Tuple.SellButton"),
                    new XAttribute("Command.Click", $"ExecuteSwitch{horizontalAlignment}Menu"),
                    new XElement("Children",
                        new XElement("HintWidget",
                            new XAttribute("DoNotAcceptEvents", true),
                            new XAttribute("DataSource", "{ButtonMenuShowHint}"),
                            new XAttribute("WidthSizePolicy", "StretchToParent"),
                            new XAttribute("HeightSizePolicy", "StretchToParent"),
                            new XAttribute("Command.HoverBegin", "ExecuteBeginHint"),
                            new XAttribute("Command.HoverEnd", "ExecuteEndHint"))));
    }
}