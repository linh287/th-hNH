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

namespace RevitAddIn1.Bai5EdittingCreating.CreateWall
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateWallCmd : ExternalCommand
    {

        public override void Execute()
        {
            DocumentUtils.Document = Document;

            var p1 = UiDocument.Selection.PickPoint("p1");
            var p2 = UiDocument.Selection.PickPoint("p2");

            var curve = Line.CreateBound(p1, p2);
            var wallTypes = new FilteredElementCollector(Document).OfClass(typeof(WallType)).FirstOrDefault(x => x.Name == "Curtain Wall 1"); // Ten de nham
            var level1 = ActiveView.GenLevel;


            using (var tx = new Transaction(Document, "Create Wall"))
            {
                tx.Start();
                // Wall..::..Create Method (Document, Curve, ElementId, ElementId, Double, Double, Boolean, Boolean)
                Wall.Create(Document, curve, wallTypes.Id, level1.Id, 3.MeetToFeet(), 0, true, true);

                tx.Commit();
            }

        }


    }
}
