using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai4Parameter
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class SetMatkcmd : ExternalCommand
    {
        public override void Execute()
        {
            var columnBeamSlabsFiter = new ElementMulticategoryFilter(new List<BuiltInCategory>
            { BuiltInCategory.OST_StructuralColumns,
            BuiltInCategory.OST_Floors,
            BuiltInCategory.OST_StructuralFraming,
            });
            var columnBeamSlabs = new FilteredElementCollector(Document, ActiveView.Id)
                .WherePasses(columnBeamSlabsFiter)
                .WhereElementIsNotElementType()
                .ToElements();
            MessageBox.Show(columnBeamSlabs.Count.ToString());
            var totalVolume=columnBeamSlabs.Sum(x => x.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble()*0.02831685);
            MessageBox.Show($"Total volume is{totalVolume} m3");
        }
    }
}
