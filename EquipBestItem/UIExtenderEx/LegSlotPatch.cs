using System.Xml;
using System.Xml.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx;

/// <summary>
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/PrefabExtensionInsertPatch.html
/// </summary>
[PrefabExtension(movie:"Inventory", "descendant::InventoryEquippedItemSlot[@Parameter.BackgroundBrush='InventoryBootSlot']")]
public class LegSlotPatch : PrefabExtensionInsertPatch
{
    public override InsertType Type => InsertType.Child;
    private readonly XmlDocument _xmlDocument;
    
    public LegSlotPatch()
    {
        var button = new ButtonWidgetXmlNode(CustomEquipmentIndex.Leg.ToString());
        var child = new XDocument(button.Node);
        _xmlDocument = new XmlDocument();
        _xmlDocument.LoadXml(child.ToString());
    }

    [PrefabExtensionXmlNode]
    public XmlNode GetPrefabExtension() => _xmlDocument;
}