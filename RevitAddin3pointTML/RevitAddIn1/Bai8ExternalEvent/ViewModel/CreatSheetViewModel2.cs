﻿using Microsoft.Win32;
using OfficeOpenXml;
using RevitAddIn1.Bai8ExternalEvent.Model;
using RevitAddIn1.Bai8ExternalEvent.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Bai8ExternalEvent.ViewModel
{
    
   
        public class CreateSheetViewModel2 : ObservableObject
        {
            public CreateSheetView2 CreateSheetView { get; set; }

            public List<CreateSheetModel2> CreateSheetModels
            {
                get => _createSheetModels;
                set
                {
                    if (Equals(value, _createSheetModels)) return;
                    _createSheetModels = value;
                    OnPropertyChanged();
                }
            }

            private List<ViewSheet> existingSheets = new List<ViewSheet>();
            public RelayCommand ImportCommand { get; set; }
            public RelayCommand OkCommand { get; set; }
            public RelayCommand CloseCommand { get; set; }

            private Document doc;
            private List<CreateSheetModel2> _createSheetModels = new List<CreateSheetModel2>();
            public CreateSheetViewModel2(Document doc)
            {
                this.doc = doc;
                OkCommand = new RelayCommand(Run);
                CloseCommand = new RelayCommand(Close);
                ImportCommand = new RelayCommand(Import);
                GetData();
            }

            void GetData()
            {
                existingSheets = new FilteredElementCollector(doc)
                 .OfClass(typeof(ViewSheet))
                 .Cast<ViewSheet>()
                 .ToList();


            }

            void Run()
            {
                if (CreateSheetView.SheetDataGrid.SelectedItems.Count < 1)
                {
                    MessageBox.Show("please select one sheet", "Warning", MessageBoxButton.OK);
                    return;
                }

                CreateSheetView.Close();

                var sheetsCreated = new List<CreateSheetModel2>();
                using (var tx = new TransactionGroup(doc, "Create sheet"))
                {
                    tx.Start();
                    foreach (var createSheetModel in CreateSheetView.SheetDataGrid.SelectedItems.Cast<CreateSheetModel2>())
                    {
                        using (var t = new Transaction(doc, "Create Sheet"))
                        {

                            t.Start();
                            try
                            {
                                var vs = ViewSheet.Create(doc, ElementId.InvalidElementId);
                                vs.SheetNumber = createSheetModel.SheetNumber;
                                vs.Name = createSheetModel.SheetName;
                                t.Commit();
                            }
                            catch (Exception ex)
                            {
                                t.RollBack();
                                sheetsCreated.Add(createSheetModel);
                            }

                        }
                    }

                    tx.Commit();
                }

                if (sheetsCreated.Any())
                {
                    MessageBox.Show("Can not create sheets for the following sheet numbers: " + Environment.NewLine + string.Join(",", sheetsCreated.Select(x => x.SheetNumber)), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            void Import()
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Files|*xlsx;*.xls",
                    FilterIndex = 1,
                    Multiselect = false,
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                var sheetNumber = worksheet.Cells[$"A{row}"].Value?.ToString();
                                var sheetName = worksheet.Cells[$"B{row}"].Value?.ToString();
                                var drawnBy = worksheet.Cells[$"C{row}"].Value?.ToString();
                                var checkedBy = worksheet.Cells[$"D{row}"].Value?.ToString();

                                if (!string.IsNullOrWhiteSpace(sheetNumber) && !string.IsNullOrWhiteSpace(sheetName))
                                {
                                    var createSheetModel2 = new CreateSheetModel2()
                                    {
                                        SheetName = sheetName,
                                        SheetNumber = sheetNumber,
                                        DrawnBy = drawnBy,
                                        CheckedBy = checkedBy,
                                    };
                                    CreateSheetModels.Add(createSheetModel2);

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while importing the Excel file: " + ex.Message);
                    }
                    CreateSheetModels = CreateSheetModels.OrderBy(x => x.SheetNumber).ToList();
                }
            }

            void Close()
            {

            }
        }
    }
