using System.Xml.Linq;
using EquipBestItem.Models.Enums;
using EquipBestItem.UIExtenderEx.XmlGenerators;
using EquipBestItem.Widgets;
using TaleWorlds.Core;

namespace EquipBestItem.UIExtenderEx;

internal class InventoryEquipButtonWidget : WidgetNode
{
    internal InventoryEquipButtonWidget(CustomEquipmentIndex equipmentIndex)
    {
        var slotName = equipmentIndex.ToString();
        
        Node = 
            new XElement("Children",
                new XElement("EquipBestItemTooltipWidget",
                    new XAttribute("DataSource", $"{{{slotName}BestItem}}"),
                    new XAttribute("IsDisabled", $"@Is{slotName}ButtonDisabled"),
                    new XAttribute("DoNotAcceptEvents", true),
                    new XAttribute("MarginTop", 5),
                    new XAttribute("MarginLeft", 5),
                    new XAttribute("SuggestedWidth", 14),
                    new XAttribute("SuggestedHeight", 14),
                    new XAttribute("WidthSizePolicy", $"Fixed"),
                    new XAttribute("HeightSizePolicy", $"Fixed"),
                    new XAttribute("HorizontalAlignment", $"Left"),
                    new XElement("Children",
                        new XElement("EquipButtonWidget",
                            new XAttribute("Id", $"EquipBestItem{slotName}Button"),
                            new XAttribute("DoNotAcceptEvents", false),
                            new XAttribute("DataSource", "{..}"),
                            new XAttribute("IsDisabled", $"@Is{slotName}ButtonDisabled"),
                            new XAttribute("Command.Click", $"ExecuteEquipBestItem"),
                            new XAttribute("CommandParameter.Click", $"{slotName}"),
                            new XAttribute("Command.AlternateClick", $"ExecuteShowFilterSettings"),
                            new XAttribute("CommandParameter.AlternateClick", $"{slotName}"),
                            new XAttribute("WidthSizePolicy", $"Fixed"),
                            new XAttribute("HeightSizePolicy", $"Fixed"),
                            new XAttribute("MarginTop", 0),
                            new XAttribute("MarginLeft", 0),
                            new XAttribute("SuggestedWidth", 14),
                            new XAttribute("SuggestedHeight", 14),
                            new XAttribute("HorizontalAlignment", $"Left"),
                            new XAttribute("Brush", $"EquipBestItem.SlotButton"),
                            new XElement("Children",
                                new XElement("HintWidget",
                                    new XAttribute("DoNotAcceptEvents", true),
                                    new XAttribute("DataSource", "{ButtonEquipHint}"),
                                    new XAttribute("WidthSizePolicy", "StretchToParent"),
                                    new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                    new XAttribute("Command.HoverBegin", $"ExecuteBeginHint"),
                                    new XAttribute("Command.HoverEnd", $"ExecuteEndHint")))))));
    }
}