using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using EquipBestItem.Models.Enums;

namespace EquipBestItem.UIExtenderEx.Patches;

/// <summary>
///     https://butr.github.io/Bannerlord.UIExtenderEx/articles/v2/PrefabExtensionInsertPatch.html
/// </summary>
[PrefabExtension("CoefficientsSettings", "descendant::ListPanel[@Id='MainList']/Children")]
public class CoefficientsSettingsWindowPatch : PrefabExtensionInsertPatch
{
    private readonly List<XmlNode> _nodes = new();

    public CoefficientsSettingsWindowPatch()
    {
        foreach (var paramName in (ItemParams[]) Enum.GetValues(typeof(ItemParams)))
        {
            var row = new CoefficientsSettingsListPanel(paramName);
            var child = new XDocument(row.Node);
            var doc = new XmlDocument();
            doc.LoadXml(child.ToString());
            _nodes?.Add(doc);
        }
    }

    [PrefabExtensionXmlNodes] public IEnumerable<XmlNode> Nodes => _nodes;

    public override InsertType Type => InsertType.Child;
    public override int Index => 3;
}