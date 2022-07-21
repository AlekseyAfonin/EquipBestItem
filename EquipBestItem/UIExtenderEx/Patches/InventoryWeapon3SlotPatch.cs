using System.Xml;
using System.Xml.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.Patches;

/// <summary>
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/PrefabExtensionInsertPatch.html
/// </summary>
[PrefabExtension(movie:"Inventory", "descendant::InventoryEquippedItemSlot[@Parameter.DropTag='Equipment_3']")]
public class InventoryWeapon3SlotPatch : PrefabExtensionInsertPatch
{
    private readonly XmlDocument _xmlDocument;
    
    public InventoryWeapon3SlotPatch()
    {
        var button = new InventoryEquipButtonWidget(CustomEquipmentIndex.Weapon3);
        var child = new XDocument(button.Node);
        _xmlDocument = new XmlDocument();
        _xmlDocument.LoadXml(child.ToString());
    }

    [PrefabExtensionXmlNode]
    public XmlNode GetPrefabExtension() => _xmlDocument;
    public override InsertType Type => InsertType.Child;
}