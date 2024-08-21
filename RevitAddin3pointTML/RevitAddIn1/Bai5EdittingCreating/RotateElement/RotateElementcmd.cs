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

namespace RevitAddIn1.Bai5EdittingCreating.RotateElement
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class MirrorElementElementcmd : ExternalCommand
    {
        public override void Execute()
        {

            DocumentUtils.Document = Document;
            var column = UiDocument.Selection.PickObject(ObjectType.Element,
                new ColumnSelectionFilter(),
                "Select column to move").ToElement();
            using (var tx = new Transaction(Document, "Move"))
            {
                tx.Start();

                var lc = column.Location as LocationPoint;
                XYZ point1 = new XYZ(lc.Point.X, lc.Point.Y, 0);
                XYZ point2 = new XYZ(lc.Point.X, lc.Point.Y, 30);
                Line axis =Line.CreateBound(point1, point2);
                ElementTransformUtils.RotateElement(Document, column.Id, axis, Math.PI / 3.0);

                tx.Commit();
            }
        }
    }
}
