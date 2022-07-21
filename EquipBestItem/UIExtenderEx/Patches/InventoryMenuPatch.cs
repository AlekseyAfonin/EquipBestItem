using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using EquipBestItem.Models.Enums;
using EquipBestItem.UIExtenderEx.XmlGenerators;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.UIExtenderEx.Patches;

/// <summary>
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/PrefabExtensionInsertPatch.html
/// </summary>
[PrefabExtension(movie:"Inventory", "descendant::InventoryCenterPanelWidget[@Id='CenterPanel']/Children")]
public class InventoryMenuPatch : PrefabExtensionInsertPatch
{
    private readonly List<XmlNode> _nodes = new();

    public InventoryMenuPatch()
    {
        XmlDocument firstChild = new XmlDocument();
        firstChild.LoadXml(new EquipBestItemButtonMenu(HorizontalAlignment.Left).Node.ToString());
        XmlDocument secondChild = new XmlDocument();
        secondChild.LoadXml(new EquipBestItemButtonMenu(HorizontalAlignment.Right).Node.ToString());
        XmlDocument thirdChild = new XmlDocument();
        thirdChild.LoadXml(new EquipBestItemMenuWidgetXml(HorizontalAlignment.Left).Node.ToString());
        XmlDocument fourthChild = new XmlDocument();
        fourthChild.LoadXml(new EquipBestItemMenuWidgetXml(HorizontalAlignment.Right).Node.ToString());

        _nodes = new List<XmlNode> {firstChild, secondChild, thirdChild, fourthChild};
    }

    [PrefabExtensionXmlNodes]
    public IEnumerable<XmlNode> Nodes => _nodes;
    public override InsertType Type => InsertType.Child;
    public override int Index => 1;
}