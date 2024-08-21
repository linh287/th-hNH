using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai5EdittingCreating.RealProject.DimensionGrid.Model;

public class GridModel: ObservableObject
{
    public bool IsHorizontalGrid { get; set; } = true;
    public Grid Grid { get; set; }
    public XYZ SP { get; set; }
    public XYZ EP { get; set; }

    public GridModel(Grid grid)
    {
        Grid = grid;

        // Kiểm tra nếu grid.Curve là null
        if (grid.Curve == null)
        {
            throw new ArgumentNullException(nameof(grid.Curve), "Curve của lưới không thể null.");
        }

        var gridCurve = grid.Curve;

        // Lấy điểm bắt đầu và điểm kết thúc
        SP = gridCurve.GetEndPoint(0);
        EP = gridCurve.GetEndPoint(1);

        var direction = EP - SP;

        // Sử dụng DotProduct để kiểm tra sự vuông góc
        var dotProduct = direction.DotProduct(XYZ.BasisX);

        // Kiểm tra nếu dotProduct gần bằng 0
        if (Math.Abs(dotProduct) < 0.0001)
        {
            IsHorizontalGrid = true;
        }
        else
        {
            IsHorizontalGrid = false;
        }
    }
}
