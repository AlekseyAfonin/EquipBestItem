using SharpRepository.Repository;
using System;

namespace EquipBestItem.Models.Entities;

public class Settings : BaseEntity
{
    public object Value { get; set; }

    public const string IsLeftPanelLocked = "IsLeftPanelLocked";
    public const string IsRightPanelLocked = "IsRightPanelLocked";
}