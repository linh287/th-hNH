using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.SelectionFiter
{
    public class ColumnSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.Category?.Name == "Structural Columns";
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
