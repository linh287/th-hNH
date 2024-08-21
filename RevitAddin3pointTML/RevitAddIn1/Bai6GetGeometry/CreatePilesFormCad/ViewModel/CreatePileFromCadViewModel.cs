﻿using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad.Model;
using MoreLinq;
using RevitAddIn1.Utils;

namespace RevitAddIn1.Bai6GetGeometry.CreatePilesFormCad.ViewModel
{
    public class CreatePileFromCadViewModel : ObservableObject
    {
        public List<CreatePilesFormCadModel> CadCurveModels { get; set; } = new List<CreatePilesFormCadModel>();

        public List<String> Layers
        {
            get => _layers;
            set
            {
                if (Equals(value, _layers)) return;
                _layers = value;
                OnPropertyChanged();
            }
        }

        public String SelectedLayer

        {
            get => _selectedLayer;
            set
            {
                if (value == _selectedLayer) return;
                _selectedLayer = value;
                OnPropertyChanged();
            }
        }

        public List<FamilySymbol> PileSymbols { get; set; } = new List<FamilySymbol>();
        public FamilySymbol SelectedPileSymbol { get; set; }
        public CreatePilesFormCadView CreatePileFromCadView { get; set; }

        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HocRevitApi\\PileCad.json";

        public RelayCommand OkCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public RelayCommand LoadCadCmd { get; set; }

        private Document doc;
        private UIDocument uiDoc;
        private List<string> _layers = new List<string>();
        private string _selectedLayer;


        public CreatePileFromCadViewModel(Document doc, UIDocument uiDoc)
        {
            this.uiDoc = uiDoc;
            this.doc = doc;
            LoadCadCmd = new RelayCommand(LoadCad);
            OkCommand = new RelayCommand(Run);
            CloseCommand = new RelayCommand(Close);

            GetData();
        }

        void GetData()
        {
            PileSymbols = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_StructuralFoundation).Cast<FamilySymbol>()
                .OrderBy(x => x.Name).ToList();

            SelectedPileSymbol = PileSymbols.FirstOrDefault();
            LoadData();
        }
        void Run()
        {
            CreatePileFromCadView.Close();

            var cadPileCurves = CadCurveModels.Where(x => x.Layer == SelectedLayer).ToList();
            using (var tx = new Transaction(doc, "Create pile"))
            {
                tx.Start();
                foreach (var cadPileCurve in cadPileCurves)
                {
                    var arc = cadPileCurve.Curve as Arc;
                    if (arc != null)
                    {
                        var center = arc.Center;
                        var radius = arc.Radius;

                        doc.Create.NewFamilyInstance(center, SelectedPileSymbol, doc.ActiveView.GenLevel,
                            StructuralType.Footing);
                    }
                }

                tx.Commit();
            }


        }
        void LoadCad()
        {
            CreatePileFromCadView.Hide();
            var cadLink = uiDoc.Selection.PickObject(ObjectType.Element, new CadLinkSelectionFilter(), "Select Cad Link").ToElement();
            var allArcs = new List<Arc>();
            var geometryElement = cadLink.get_Geometry(new Options());
            foreach (var geoOjb in geometryElement)
            {
                if (geoOjb is GeometryInstance geometryInstance)
                {
                    var ge = geometryInstance.GetInstanceGeometry();
                    var arcs = ge.Where(x => x is Arc).Cast<Arc>().ToList();
                    allArcs.AddRange(arcs);
                }
            }

            CadCurveModels = allArcs.Select(x => new CreatePilesFormCadModel(x)).ToList();
            Layers = CadCurveModels.Select(x => x.Layer).DistinctBy(x => x).OrderBy(x => x).ToList();

            SelectedLayer = Layers.FirstOrDefault();

            CreatePileFromCadView.ShowDialog();
        }
        void Close()
        {

        }
        void SaveData()
        {

        }
        void LoadData()
        {

        }
    }
}

