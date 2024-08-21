using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.Model;
using RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.View;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.ViewModel
{

    public class DimensionGridViewModel : ObservableObject
    {
        public List<DimensionType> DimensionTypes { get; set; }
        public DimensionType SelectedDimensionType { get; set; }
        public double Distance { get; set; } = 5.0;
        public RelayCommand OkeCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        private Document doc;
        private UIDocument uidoc;
        private XYZ vector;

        public DimensionGridView DimensionGridView { get; set; }

        public DimensionGridViewModel(Document doc, UIDocument uidoc)
        {
            this.uidoc= uidoc;
            this.doc = doc;
            vector = XYZ.BasisX; // Ví dụ: Khởi tạo vector theo trục X, thay đổi theo nhu cầu
            OkeCommand = new RelayCommand(Run);
            CloseCommand = new RelayCommand(Close);
            GetData();
        }

        void GetData()
        {
            DimensionTypes = new FilteredElementCollector(doc)
                .OfClass(typeof(DimensionType))
                .Cast<DimensionType>()
                .Where(x => x.StyleType == DimensionStyleType.Linear || x.StyleType == DimensionStyleType.LinearFixed)
                .OrderBy(x => x.Name)
                .ToList();

            SelectedDimensionType = DimensionTypes.FirstOrDefault();
        }

        void Run()
        {
            DimensionGridView.Close();

            var allGrids = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfClass(typeof(Grid))
                .Cast<Grid>()
                .ToList();

            var allGridModels = allGrids.Select(x => new GridModel(x)).ToList();

            using (var tx = new Transaction(doc, "Create dim"))
            {
                tx.Start();
                while (true)
                {
                    Reference rf;
                    try
                    {
                        rf = uidoc.Selection.PickObject(ObjectType.Element, new GridSelectionFilter(), "Select grid to dim ...");
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    var point = rf.GlobalPoint;
                    var grid = rf.ToElement() as Grid;
                    if (grid == null) continue; // Bỏ qua nếu grid là null

                    var selectedGridModel = new GridModel(grid);
                    var gridToDim = allGridModels.Where(x => x.IsHorizontalGrid == selectedGridModel.IsHorizontalGrid).ToList();
                    var ra = new ReferenceArray();
                    foreach (var gridModel in gridToDim)
                    {
                        ra.Append(new Reference(gridModel.Grid));
                    }

                    var orderedGrids = allGridModels.OrderBy(x => x.EP.DotProduct(vector)).ToList();
                    var gridModel1 = orderedGrids.FirstOrDefault();
                    var gridModel2 = orderedGrids.Skip(1).FirstOrDefault(); // Lấy lưới thứ hai

                    if (gridModel1 == null || gridModel2 == null) continue; // Bỏ qua nếu không đủ lưới

                    var line = Line.CreateBound(point, point.Add(vector));

                    doc.Create.NewDimension(doc.ActiveView, line, ra, SelectedDimensionType);

                    var p = point.Add(XYZ.BasisX * Distance.FeetToMeet() * doc.ActiveView.Scale);
                    var line2 = Line.CreateUnbound(p, p.Add(vector));

                    var ra2 = new ReferenceArray();
                    ra2.Append(new Reference(gridModel1.Grid));
                    ra2.Append(new Reference(gridModel2.Grid));

                    doc.Create.NewDimension(doc.ActiveView, line2, ra2, SelectedDimensionType);

                    tx.Commit();
                }
            }
        }

        void Close()
        {
            // Implement close logic if needed
        }
    }
}