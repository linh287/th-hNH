using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.SelectionFiter;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai5EdittingCreating.CreatBeam
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateBeamcmd : ExternalCommand
    {
        public override void Execute()
        {

            DocumentUtils.Document = Document;
            var p1 = UiDocument.Selection.PickPoint("P1");
            var p2 = UiDocument.Selection.PickPoint("P2");
            var curve = Line.CreateBound(p1, p2);

            var BeamType = new FilteredElementCollector(Document).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_StructuralFraming)
                .FirstOrDefault() as FamilySymbol;

            var level1 = ActiveView.GenLevel;

            using (var tx = new Transaction(Document, "Create"))
            {
                tx.Start();

              FamilyInstance instance = Document.Create.NewFamilyInstance(curve,BeamType, level1,StructuralType.Beam);

                tx.Commit();
            }
        }
    }
}