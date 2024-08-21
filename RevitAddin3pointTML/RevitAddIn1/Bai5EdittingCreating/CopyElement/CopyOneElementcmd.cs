using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.SelectionFiter;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai5EdittingCreating.CopyElement
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CopyOneElementcmd : ExternalCommand
    {
        public override void Execute()
        {
            DocumentUtils.Document = Document;
            var column =UiDocument.Selection.PickObject(ObjectType.Element,
                new ColumnSelectionFilter(),
                "Select column to copy").ToElement();
            using (var tx =new Transaction(Document,"Move"))
            {
                tx.Start();

                ElementTransformUtils.CopyElement(Document,column.Id,new XYZ(10.FeetToMeet(),0,0));

                tx.Commit();
            }    
        }
    }
}
