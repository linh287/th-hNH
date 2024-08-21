using Nice3point.Revit.Extensions;
using RevitAddIn1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad.Model
{
    public class CreatePilesFormCadModel : ObservableObject
    {
        public string Layer { get; set; }
        public Curve Curve { get; set; }

        public CreatePilesFormCadModel(Arc arc)
        {
            var graphicsStyle = arc.GraphicsStyleId.ToElement() as GraphicsStyle; ;
            Layer = graphicsStyle.GraphicsStyleCategory.Name;
            Curve = arc;
        }
    }
}
