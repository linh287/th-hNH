using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Bai4Parameter.RenameSheet.View;
using RevitAddIn1.Bai4Parameter.RenameSheet.ViewModel;
using RevitAddIn1.Bai4Parameter.RenameSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai4Parameter.RenameSheetcmd
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateSheetcmd : ExternalCommand
    {
        public override void Execute()
        {
           var vn =new RenameSheetViewModel(Document);
            var view =new RenameSheetView() { DataContext = vn };
            view.ShowDialog();
        }
    }
}
