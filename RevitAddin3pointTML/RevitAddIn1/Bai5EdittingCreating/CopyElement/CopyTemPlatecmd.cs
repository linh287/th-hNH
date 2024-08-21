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
    public class CopyTemPlatecmd : ExternalCommand
    {
        public override void Execute()
        {
            DocumentUtils.Document = Document;

            var templateDoc = Application.OpenDocumentFile("C:\\vidu.rvt");
            var schedule = new FilteredElementCollector(templateDoc).OfClass(typeof(ViewSchedule)).Cast<ViewSchedule>()
                .FirstOrDefault(x => x.Name == "Structural Column Schedule");
            using (var tx =new Transaction(Document,"Move"))
            {
                tx.Start();

                //ElementTransformUtils.CopyElements(templateDoc, new List<Element>()
                //{schedule.Id},Document,Transform.Identity,new CopyPasteOptions());
                ElementTransformUtils.CopyElements(
                      templateDoc,
                      new List<ElementId> { schedule.Id },
                      Document,
                      Transform.Identity,
                      new CopyPasteOptions()
                  );

                tx.Commit();
            }    
            templateDoc.Close(false);
        }
    }
}
