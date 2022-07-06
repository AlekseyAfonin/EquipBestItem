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
                new XElement("EquipButtonWidget",
                    new XAttribute("Id", $"EquipBestItem{slotName}Button"),
                    new XAttribute("IsEnabled", $"@Is{slotName}ButtonEnabled"),
                    new XAttribute("Command.Click", $"ExecuteEquipBestItem"),
                    new XAttribute("CommandParameter.Click", $"{slotName}"),
                    new XAttribute("Command.AlternateClick", $"ExecuteShowFilterSettings"),
                    new XAttribute("CommandParameter.AlternateClick", $"{slotName}"),
                    new XAttribute("WidthSizePolicy", $"Fixed"),
                    new XAttribute("HeightSizePolicy", $"Fixed"),
                    new XAttribute("MarginTop", 7),
                    new XAttribute("MarginLeft", 7),
                    new XAttribute("SuggestedWidth", 14),
                    new XAttribute("SuggestedHeight", 14),
                    new XAttribute("HorizontalAlignment", $"Left"),
                    new XAttribute("Brush", $"Inventory.EquipBetterItem.PlusButton")));
    }
}