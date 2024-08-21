using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad.View;
using RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class CreatePilesFormCadcmd : ExternalCommand
    {
        public override void Execute()
        {
            var vn = new CreatePileFromCadViewModel(Document,UiDocument);
            var view = new CreatePilesFormCadView() { DataContext = vn };
            view.ShowDialog();
        }
    }
}
