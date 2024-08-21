using System.Data;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using CommunityToolkit.Mvvm.Messaging;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.ViewModels;
using RevitAddIn1.Views;
using OperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;

namespace RevitAddIn1.Bai6GetGeometry.Getgeometry
{
    /// <summary>
    ///     External command entry point invoked from the Revit interface
    /// </summary>
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class GetWallsSoildCmd : ExternalCommand
    {
        public override void Execute()
        {
            try
            {
                var referent = UiDocument.Selection.PickObject(ObjectType.Element, new WallSelectionFilter(), "Chon doi tuong Wall");
                var ele = Document.GetElement(referent);


                var opt = new Options();
                var geometryElement =ele.get_Geometry(opt);
                foreach(GeometryObject geometryObject in geometryElement) 
                {
                    if (geometryObject is Solid solid)
                    {
                        MessageBox.Show($"Volumn is {solid.Volume} ft3");
                    }
                }
                    
                
           
                        
                        
                        }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Ban da huy chon", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private List<Solid> GetAllSoilds (Element ele)
        {
            var soilds = new List<Solid>();
            var geometryElement = ele.get_Geometry(new Options());

            foreach (var geoOject in geometryElement) 
            {
                if(geoOject is Solid solid)
                    if(solid.Volume > 0)
                    {
                        soilds.Add(solid);
                    }
                if (geoOject is GeometryInstance geometryInstance)
                {
                    var geoElement = geometryInstance.GetInstanceGeometry();
                    var soild2 = geoElement.ToList().Where(x => x is Solid).Cast<Solid>().Where(x => x.Volume > 0).ToList();
                    soilds.AddRange(soild2);
                }

            }   
            return soilds;
                
        }
    }

    public class WallSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category.Name == "Walls")
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }


}