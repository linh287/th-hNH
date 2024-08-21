using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAddIn1.Bai5EdittingCreating.CreateFloor
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateFloorCmd : ExternalCommand
    {

        public override void Execute()
        {
            DocumentUtils.Document = Document;
            Autodesk.Revit.DB.Document doc = Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FloorType floorType = collector
                .OfClass(typeof(FloorType))
                .OfCategory(BuiltInCategory.OST_Floors)
                .FirstElement() as FloorType;

            XYZ pt1 = new XYZ(0, 0, 0);
            XYZ pt2 = new XYZ(20, 0, 0);
            XYZ pt3 = new XYZ(20, 20, 0);
            XYZ pt4 = new XYZ(0, 20, 0);

            var curveLoop = new CurveLoop();
            Curve ab = Line.CreateBound(pt1, pt2);
            Curve bc = Line.CreateBound(pt2, pt3);
            Curve cd = Line.CreateBound(pt3, pt4);
            Curve da = Line.CreateBound(pt4, pt1);

            curveLoop.Append(ab);
            curveLoop.Append(bc);
            curveLoop.Append(cd);
            curveLoop.Append(da);

            Level level = collector
               .OfClass(typeof(Level))
               .FirstElement() as Level;

            using (var tx = new Transaction(Document, "Create Floor"))
            {
                tx.Start();
                NewMethod(doc, floorType, curveLoop);

                tx.Commit();
            }

        }

        public void NewMethod(Document doc, FloorType floorType, CurveLoop curveLoop)
        {
            //Floor.Create(doc, new List<CurveLoop> { curveLoop }, floorType.Id, ActiveView.GenLevel.Id);
         

        }
    }
}
