using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Bai4Parameter.RenameSheet.View;
using RevitAddIn1.Bai4Parameter.RenameSheet.ViewModel;
using RevitAddIn1.Bai5EdittingCreating.CreateSheet.View;
using RevitAddIn1.Bai5EdittingCreating.CreateSheet.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai5EdittingCreating.CreateSheet
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateSheetcmd : ExternalCommand
    {
        public override void Execute()
        {
           var vm =new CreateSheetViewModel(Document);
            var view =new CreateSheetView() { DataContext = vm};
            vm.CreateSheetView = view;
            view.ShowDialog();
        }
    }
}
