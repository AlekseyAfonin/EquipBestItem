using System.Drawing.Printing;
using System.Xml.Linq;
using EquipBestItem.Models.Enums;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.UIExtenderEx.XmlGenerators;

internal class EquipBestItemMenuWidgetXml : WidgetNode
{
    internal EquipBestItemMenuWidgetXml(HorizontalAlignment horizontalAligment)
    {
        
        if (horizontalAligment == HorizontalAlignment.Left)
            Node = 
                new XElement("EquipBestItemMenuWidget",
                        new XAttribute("Id", $"EquipBestItem{horizontalAligment}Menu"),
                        new XAttribute("WidthSizePolicy", $"Fixed"),
                        new XAttribute("IsVisible", $"@Is{horizontalAligment}MenuVisible"),
                        new XAttribute("HeightSizePolicy", $"Fixed"),
                        new XAttribute("MarginTop", 82),
                        new XAttribute($"Margin{horizontalAligment}", -10),
                        new XAttribute("SuggestedWidth", 146),
                        new XAttribute("SuggestedHeight", 69),
                        new XAttribute("HorizontalAlignment", $"{horizontalAligment}"),
                        new XAttribute("Brush", $"Inventory.EquipedItemControls.Background.HorizontalFlip"),
                        new XElement("Children", 
                            new XElement("ListPanel", 
                                new XAttribute("Id", "Panel"),
                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                new XElement("Children",
                                    new XElement("Widget",
                                        new XAttribute("WidthSizePolicy", $"Fixed"),
                                        new XAttribute("HeightSizePolicy", $"Fixed"),
                                        new XAttribute("SuggestedWidth", 35),
                                        new XAttribute("SuggestedHeight", 35),
                                        new XAttribute("MarginLeft", 19),
                                        new XAttribute("VerticalAlignment", $"Center"),
                                        new XAttribute("Sprite", $"Inventory\\toolbox_icon_bed"),
                                        new XElement("Children",
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"3"),
                                                new XAttribute("MarginRight", $"3"),
                                                new XAttribute("MarginTop", $"3"),
                                                new XAttribute("MarginBottom", $"3"),
                                                new XAttribute("Command.Click", $"ExecuteSwitchLeftMenu"),
                                                new XAttribute("Brush", $"ButtonRightArrowBrush1"),
                                                new XElement("Children", 
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents","true"),
                                                        new XAttribute("DataSource","{ButtonMenuHideHint}"),
                                                        new XAttribute("Command.HoverBegin","ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd","ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy","StretchToParent"),
                                                        new XAttribute("HeightSizePolicy","StretchToParent"))
                                                    )
                                                )
                                            )
                                        ),
                                    new XElement("Widget",
                                        new XAttribute("WidthSizePolicy", $"Fixed"),
                                        new XAttribute("HeightSizePolicy", $"Fixed"),
                                        new XAttribute("SuggestedWidth", 35),
                                        new XAttribute("SuggestedHeight", 35),
                                        new XAttribute("MarginLeft", 2),
                                        new XAttribute("VerticalAlignment", $"Center"),
                                        new XAttribute("Sprite", $"Inventory\\toolbox_icon_bed"),
                                        new XElement("Children",
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"6"),
                                                new XAttribute("MarginRight", $"6"),
                                                new XAttribute("MarginTop", $"6"),
                                                new XAttribute("MarginBottom", $"6"),
                                                new XAttribute("Command.Click", $"Execute{horizontalAligment}PanelLock"),
                                                new XAttribute("IsHidden", $"@{horizontalAligment}OpenedLockIsHidden"),
                                                new XAttribute("Brush", $"EquipBestItem.LockPanelButton.Closed"),
                                                new XElement("Children", 
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents","true"),
                                                        new XAttribute("DataSource","{ButtonLeftPanelUnlockHint}"),
                                                        new XAttribute("Command.HoverBegin","ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd","ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy","StretchToParent"),
                                                        new XAttribute("HeightSizePolicy","StretchToParent"))
                                                    )
                                                ),
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"6"),
                                                new XAttribute("MarginRight", $"6"),
                                                new XAttribute("MarginTop", $"6"),
                                                new XAttribute("MarginBottom", $"6"),
                                                new XAttribute("Command.Click", $"Execute{horizontalAligment}PanelLock"),
                                                new XAttribute("IsHidden", $"@{horizontalAligment}ClosedLockIsHidden"),
                                                new XAttribute("Brush", $"EquipBestItem.LockPanelButton.Opened"),
                                                new XElement("Children",
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents", "true"),
                                                        new XAttribute("DataSource", "{ButtonLeftPanelLockHint}"),
                                                        new XAttribute("Command.HoverBegin", "ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd", "ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy", "StretchToParent"),
                                                        new XAttribute("HeightSizePolicy", "StretchToParent"))
                                                    )
                                                )
                                            )
                                        ),
                                    new XElement("Widget",
                                        new XAttribute("WidthSizePolicy", $"Fixed"),
                                        new XAttribute("HeightSizePolicy", $"Fixed"),
                                        new XAttribute("SuggestedWidth", 35),
                                        new XAttribute("SuggestedHeight", 35),
                                        new XAttribute("MarginLeft", 2),
                                        new XAttribute("MarginRight", 10),
                                        new XAttribute("VerticalAlignment", $"Center"),
                                        new XAttribute("Sprite", $"Inventory\\toolbox_icon_bed"),
                                        new XElement("Children",
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"3"),
                                                new XAttribute("MarginRight", $"3"),
                                                new XAttribute("MarginTop", $"3"),
                                                new XAttribute("MarginBottom", $"3"),
                                                new XAttribute("Command.Click", $"ExecuteEquipAllCharacters"),
                                                new XAttribute("Brush", $"EquipBestItem.EquipAllCharactersButton"),
                                                new XElement("Children", 
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents","true"),
                                                        new XAttribute("DataSource","{ButtonEquipAllHint}"),
                                                        new XAttribute("Command.HoverBegin","ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd","ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy","StretchToParent"),
                                                        new XAttribute("HeightSizePolicy","StretchToParent"))
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        );
        else
        {
            Node = 
                new XElement("EquipBestItemMenuWidget",
                        new XAttribute("Id", $"EquipBestItem{horizontalAligment}Menu"),
                        new XAttribute("WidthSizePolicy", $"Fixed"),
                        new XAttribute("IsVisible", $"@Is{horizontalAligment}MenuVisible"),
                        new XAttribute("HeightSizePolicy", $"Fixed"),
                        new XAttribute("MarginTop", 82),
                        new XAttribute($"Margin{horizontalAligment}", -10),
                        new XAttribute("SuggestedWidth", 146),
                        new XAttribute("SuggestedHeight", 69),
                        new XAttribute("HorizontalAlignment", $"{horizontalAligment}"),
                        new XAttribute("Brush", $"Inventory.EquipedItemControls.Background"),
                        new XElement("Children", 
                            new XElement("ListPanel", 
                                new XAttribute("Id", "Panel"),
                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                new XElement("Children",
                                    new XElement("Widget",
                                        new XAttribute("WidthSizePolicy", $"Fixed"),
                                        new XAttribute("HeightSizePolicy", $"Fixed"),
                                        new XAttribute("SuggestedWidth", 35),
                                        new XAttribute("SuggestedHeight", 35),
                                        new XAttribute("MarginLeft", 19),
                                        new XAttribute("VerticalAlignment", $"Center"),
                                        new XAttribute("Sprite", $"Inventory\\toolbox_icon_bed"),
                                        new XElement("Children",
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"3"),
                                                new XAttribute("MarginRight", $"3"),
                                                new XAttribute("MarginTop", $"3"),
                                                new XAttribute("MarginBottom", $"3"),
                                                new XAttribute("Command.Click", $"ExecuteEquipCurrentCharacter"),
                                                new XAttribute("Brush", $"InventoryEquipButton"),
                                                new XElement("Children", 
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents","true"),
                                                        new XAttribute("DataSource","{ButtonEquipCurrentHint}"),
                                                        new XAttribute("Command.HoverBegin","ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd","ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy","StretchToParent"),
                                                        new XAttribute("HeightSizePolicy","StretchToParent"))
                                                    )
                                                )
                                            )
                                        ),
                                    new XElement("Widget",
                                        new XAttribute("WidthSizePolicy", $"Fixed"),
                                        new XAttribute("HeightSizePolicy", $"Fixed"),
                                        new XAttribute("SuggestedWidth", 35),
                                        new XAttribute("SuggestedHeight", 35),
                                        new XAttribute("MarginLeft", 2),
                                        new XAttribute("VerticalAlignment", $"Center"),
                                        new XAttribute("Sprite", $"Inventory\\toolbox_icon_bed"),
                                        new XElement("Children",
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"6"),
                                                new XAttribute("MarginRight", $"6"),
                                                new XAttribute("MarginTop", $"6"),
                                                new XAttribute("MarginBottom", $"6"),
                                                new XAttribute("Command.Click", $"Execute{horizontalAligment}PanelLock"),
                                                new XAttribute("IsHidden", $"@{horizontalAligment}OpenedLockIsHidden"),
                                                new XAttribute("Brush", $"EquipBestItem.LockPanelButton.Closed"),
                                                new XElement("Children",
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents", "true"),
                                                        new XAttribute("DataSource", "{ButtonRightPanelUnlockHint}"),
                                                        new XAttribute("Command.HoverBegin", "ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd", "ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy", "StretchToParent"),
                                                        new XAttribute("HeightSizePolicy", "StretchToParent"))
                                                    )
                                                ),
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"6"),
                                                new XAttribute("MarginRight", $"6"),
                                                new XAttribute("MarginTop", $"6"),
                                                new XAttribute("MarginBottom", $"6"),
                                                new XAttribute("Command.Click", $"Execute{horizontalAligment}PanelLock"),
                                                new XAttribute("IsHidden", $"@{horizontalAligment}ClosedLockIsHidden"),
                                                new XAttribute("Brush", $"EquipBestItem.LockPanelButton.Opened"),
                                                new XElement("Children",
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents", "true"),
                                                        new XAttribute("DataSource", "{ButtonRightPanelLockHint}"),
                                                        new XAttribute("Command.HoverBegin", "ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd", "ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy", "StretchToParent"),
                                                        new XAttribute("HeightSizePolicy", "StretchToParent"))
                                                    )
                                                )
                                            )
                                        ),
                                    new XElement("Widget",
                                        new XAttribute("WidthSizePolicy", $"Fixed"),
                                        new XAttribute("HeightSizePolicy", $"Fixed"),
                                        new XAttribute("SuggestedWidth", 35),
                                        new XAttribute("SuggestedHeight", 35),
                                        new XAttribute("MarginLeft", 2),
                                        new XAttribute("MarginRight", 10),
                                        new XAttribute("VerticalAlignment", $"Center"),
                                        new XAttribute("Sprite", $"Inventory\\toolbox_icon_bed"),
                                        new XElement("Children",
                                            new XElement("ButtonWidget",
                                                new XAttribute("WidthSizePolicy", $"StretchToParent"),
                                                new XAttribute("HeightSizePolicy", $"StretchToParent"),
                                                new XAttribute("MarginLeft", $"3"),
                                                new XAttribute("MarginRight", $"3"),
                                                new XAttribute("MarginTop", $"3"),
                                                new XAttribute("MarginBottom", $"3"),
                                                new XAttribute("Command.Click", $"ExecuteSwitchRightMenu"),
                                                new XAttribute("Brush", $"ButtonLeftArrowBrush1"),
                                                new XElement("Children", 
                                                    new XElement("HintWidget",
                                                        new XAttribute("DoNotAcceptEvents","true"),
                                                        new XAttribute("DataSource","{ButtonMenuHideHint}"),
                                                        new XAttribute("Command.HoverBegin","ExecuteBeginHint"),
                                                        new XAttribute("Command.HoverEnd","ExecuteEndHint"),
                                                        new XAttribute("WidthSizePolicy","StretchToParent"),
                                                        new XAttribute("HeightSizePolicy","StretchToParent"))
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        );
        }
        
            
    }
}