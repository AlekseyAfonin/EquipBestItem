using System.Xml;
using System.Xml.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.Patches;

/// <summary>
///     https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/PrefabExtensionInsertPatch.html
/// </summary>
[PrefabExtension("Inventory", "descendant::InventoryEquippedItemSlot[@Parameter.BackgroundBrush='InventoryCloakSlot']")]
public class InventoryCapeSlotPatch : PrefabExtensionInsertPatch
{
    private readonly XmlDocument _xmlDocument;

    public InventoryCapeSlotPatch()
    {
        var button = new InventoryEquipButtonWidget(CustomEquipmentIndex.Cape);
        var child = new XDocument(button.Node);
        _xmlDocument = new XmlDocument();
        _xmlDocument.LoadXml(child.ToString());
    }

    public override InsertType Type => InsertType.Child;

    [PrefabExtensionXmlNode]
    public XmlNode GetPrefabExtension()
    {
        return _xmlDocument;
    }
}