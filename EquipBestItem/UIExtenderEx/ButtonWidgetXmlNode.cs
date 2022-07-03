using System;
using System.Xml.Linq;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.UIExtenderEx;

public class ButtonWidgetXmlNode
{
    public XElement Node { get; }

    public ButtonWidgetXmlNode(string slotName,
        string commandClick = "ExecuteEquipBestItem", 
        string commandAlternateClick = "ExecuteShowFilterSettings", 
        string horizontalAlignment = "Left",
        string widthSizePolicy = "Fixed",
        string heightSizePolicy = "Fixed",
        int suggestedWidth = 35,
        int suggestedHeight = 35,
        string brush = "Inventory.EquipBetterItem.PlusButton")
    {
        Node = new XElement("Children", 
            new XElement("ButtonWidget", 
                new XAttribute("Id", $"EquipBestItem{slotName}Button"),
                new XAttribute("IsEnabled", $"@Is{slotName}ButtonEnabled"),
                new XAttribute("Command.Click", $"{commandClick}"),
                new XAttribute("CommandParameter.Click", $"{slotName}"),
                new XAttribute("Command.AlternateClick", $"{commandAlternateClick}"),
                new XAttribute("CommandParameter.AlternateClick", $"{slotName}"),
                new XAttribute("WidthSizePolicy", $"{widthSizePolicy}"),
                new XAttribute("HeightSizePolicy", $"{heightSizePolicy}"),
                new XAttribute("SuggestedWidth", $"{suggestedWidth.ToString()}"),
                new XAttribute("SuggestedHeight", $"{suggestedHeight.ToString()}"),
                new XAttribute("HorizontalAlignment", $"{horizontalAlignment}"),
                new XAttribute("Brush", $"{brush}")));
    }
}