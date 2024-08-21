using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai5EdittingCreating.CreateGrid
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateGridCmd : ExternalCommand
    {

        public override void Execute()
        {
            DocumentUtils.Document = Document;

            var p1 = UiDocument.Selection.PickPoint("p1");
            var p2 = UiDocument.Selection.PickPoint("p2");
            var line = Line.CreateBound(p1, p2);




            using (var tx = new Transaction(Document, "Create Beam"))
            {
                tx.Start();
                Grid.Create(Document, line);

                tx.Commit();
            }

        }
    }

 }
