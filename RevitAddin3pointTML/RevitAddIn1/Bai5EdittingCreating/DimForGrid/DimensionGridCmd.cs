using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Bai5EdittingCreating.DimForGrid.View;
using RevitAddIn1.Bai5EdittingCreating.DimForGrid.ViewModel;
using RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.View;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitAddIn1.Bai5EdittingCreating.DimForGrid.View;
using RevitAddIn1.Bai5EdittingCreating.DimForGrid.Model;
using RevitAddIn1.Bai5EdittingCreating.DimForGrid.ViewModel;


namespace RevitAddIn1.Bai5EdittingCreating.DimForGrid
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class DimensionGridCmd : ExternalCommand
    {
        public override void Execute()
        {
            DocumentUtils.Document = Document;
            var vm = new DimensionGridViewModel(Document, UiDocument);
            var view = new View.DimensionGridView() { DataContext = vm };

            vm.DimensionGridView = view;

            view.ShowDialog();
        }
    }
}
