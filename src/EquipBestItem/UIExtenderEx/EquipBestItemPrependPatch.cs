using System.Xml;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using TaleWorlds.ModuleManager;

namespace EquipBestItem.UIExtenderEx;

[PrefabExtension(movie:"Inventory", "descendant::Widget[@Id='LeftMenu']/Children/InventoryEquippedItemSlot[2]")]
public sealed class EquipBestItemPrefabExtension1 : PrefabExtensionInsertPatch
{
    public override InsertType Type => InsertType.Append;

    private readonly XmlDocument _xmlDocument = new();

    public EquipBestItemPrefabExtension1()
    {
        var modulePath = ModuleHelper.GetModuleFullPath("EquipBestItem");
        _xmlDocument.Load($"{modulePath}GUI/PrefabExtensions/InventoryPatch.xml");
    }

    [PrefabExtensionXmlNode()]
    public XmlNode GetPrefabExtension() => _xmlDocument;
}