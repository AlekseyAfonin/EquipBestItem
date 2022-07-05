using System.Xml;
using System.Xml.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using EquipBestItem.Models.Enums;
using EquipBestItem.Widgets;

namespace EquipBestItem.UIExtenderEx;

/// <summary>
/// https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/PrefabExtensionInsertPatch.html
/// </summary>
[PrefabExtension(movie:"Inventory", "descendant::InventoryEquippedItemSlot[@Parameter.BackgroundBrush='InventoryHelmetSlot']")]
public class HeadSlotPatch : PrefabExtensionInsertPatch
{
    public override InsertType Type => InsertType.Child;
    private readonly XmlDocument _xmlDocument;
    
    public HeadSlotPatch()
    {
        var button = new InventoryEquipButtonWidget(CustomEquipmentIndex.Head);
        var child = new XDocument(button.Node);
        _xmlDocument = new XmlDocument();
        _xmlDocument.LoadXml(child.ToString());
    }

    [PrefabExtensionXmlNode]
    public XmlNode GetPrefabExtension() => _xmlDocument;
}