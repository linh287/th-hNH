using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Bai15ThucChien.ColumnRebar.ViewModel;
using RevitAddIn1.Utils;
using RevitAddIn1.Bai15ThucChien.ColumnRebar.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai15ThucChien.ColumnRebar
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class ColumnRebarCmd : ExternalCommand
    {
        public override void Execute()
        {
            DocumentUtils.Document = Document;

            var vm = new ColumnRebarViewModel(Document, UiDocument);
            var view = new ColumnRebarView() { DataContext = vm };

            vm.ColumnRebarView = view;
            view.ShowDialog();
        }
    }
}
