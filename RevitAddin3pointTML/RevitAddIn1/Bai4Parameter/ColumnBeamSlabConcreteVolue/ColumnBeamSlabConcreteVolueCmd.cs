using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai4Parameter.SetMark
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class SetMatkcmd : ExternalCommand
    {
        public override void Execute()
        {
            var Columnfr = UiDocument.Selection.PickObjects(ObjectType.Element, "Select Column");
            var columns = Columnfr.Select(x => Document.GetElement(x)).ToList();
            var markc1 = "C1";
            using (var tx= new Transaction(Document, "Set mark"))
            {
                tx.Start();
                columns.ForEach(x =>
                 {
                     x.LookupParameter("Mark").Set(markc1);


                 });
                tx.Commit();
            }
        }
    }
}
