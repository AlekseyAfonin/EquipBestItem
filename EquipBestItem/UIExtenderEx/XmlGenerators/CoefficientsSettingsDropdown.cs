// using System.Xml.Linq;
// using EquipBestItem.UIExtenderEx.XmlGenerators.CoefficientsSettingsParamRow;
//
// namespace EquipBestItem.UIExtenderEx.XmlGenerators;
//
// public class CoefficientsSettingsDropdown : WidgetNode
// {
//     public CoefficientsSettingsDropdown()
//     {
//         Node =
//             new XElement("ListPanel",
//                 new XAttribute("Id", $"HorizontalControlParent"),
//                 new XAttribute("DataSource", $"SelectorDataSource"),
//                 new XAttribute("WidthSizePolicy", "CoverChildren"),
//                 new XAttribute("HeightSizePolicy", "CoverChildren"),
//                 new XElement("Children", 
//                     new XElement("Widget",
//                         XAttribute("WidthSizePolicy","Fixed"),
//                         XAttribute("HeightSizePolicy","Fixed"),
//                         XAttribute("SuggestedWidth","40"),
//                         XAttribute("SuggestedHeight","43"),
//                         XAttribute("VerticalAlignment","Center"),
//                         XAttribute("DoNotAcceptEvents","true"),
//                         XAttribute("IsVisible","*ShowNextAndPrevious"),
//                         new XElement("Children",
//                             new XElement("ButtonWidget",
//                                 XAttribute("WidthSizePolicy","Fixed"),
//                                 XAttribute("HeightSizePolicy","Fixed"),
//                                 XAttribute("SuggestedWidth","60"),
//                                 XAttribute("SuggestedHeight","70"),
//                                 XAttribute("VerticalAlignment","Center"),
//                                 XAttribute("HorizontalAlignment","Center"),
//                                 XAttribute("Command.Click","ExecuteSelectPreviousItem"),
//                                 XAttribute("Brush","!PreviousButtonBrush"),
//                                 XAttribute("IsEnabled","*IsEnabled")))),
//                     new XElement("OptionDropdownWidget",
//                         new XAttribute("Id","DropdownParent"),
//                         new XAttribute("WidthSizePolicy","Fixed"),
//                         new XAttribute("HeightSizePolicy","CoverChildren"),
//                         new XAttribute("SuggestedWidth","!DropdownCenter.Width"),
//                         new XAttribute("HorizontalAlignment","Center"),
//                         new XAttribute("VerticalAlignment","Center"),
//                         new XAttribute("DropdownContainerWidget","DropdownClipWidget\\DropdownContainerWidget"),
//                         new XAttribute("ListPanel","DropdownClipWidget\\DropdownContainerWidget\\ScrollablePanel\\ClipRect\\PrimaryUsageSelectorList"),
//                         new XAttribute("Button","DropdownButtonContainer\\DropdownButton"),
//                         new XAttribute("CurrentSelectedIndex","@SelectedIndex"),
//                         new XAttribute("TextWidget","DropdownParent"),
//                         new XAttribute("Id","DropdownParent"),
//                         new XAttribute("Id","DropdownParent"),
//                         )));
//     }
// }