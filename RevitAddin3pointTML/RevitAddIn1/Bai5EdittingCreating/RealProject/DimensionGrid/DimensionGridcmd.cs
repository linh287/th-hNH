using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.View;
using RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.ViewModel;
using RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai5EdittingCreating.DimensionGridcmd
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class DimensionGridcmd : ExternalCommand
    {
        public override void Execute()
        {
           var vn =new DimensionGridViewModel(Document,UiDocument);
            var view =new DimensionGridView() { DataContext = vn };
            vn.DimensionGridView = view;
            view.ShowDialog();
        }
    }
}
