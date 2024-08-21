using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using OfficeOpenXml;
using RevitAddIn1.Bai5EdittingCreating.CreateSheet.View;
using RevitAddIn1.Bai5EdittingCreating.CreateSheet.ViewModel;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai8ExternalEvent
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreateSheetCmd2 : ExternalCommand
    {
        public override void Execute()
        {
            var externalEvent = DocumentUtils.ExternalEvent;
            // if you have a commercial license
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var vm = new CreateSheetViewModel(Document);
            var view = new CreateSheetView() { DataContext = vm };

            vm.CreateSheetView = view;
            view.ShowDialog();

        }
    }
}
