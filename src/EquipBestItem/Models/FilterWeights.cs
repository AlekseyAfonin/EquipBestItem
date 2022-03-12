using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EquipBestItem.Models;

public class FilterWeights
{
    // Armor
    public double HeadArmor { get; set; }
    public double BodyArmor { get; set; }
    public double ArmArmor { get; set; }
    public double Weight { get; set; }
}