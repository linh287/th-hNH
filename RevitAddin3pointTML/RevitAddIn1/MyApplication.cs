﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitAddIn1
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class MyApplication : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab("Revit Api 2025");

            var panelMe = application.CreateRibbonPanel("Revit Api 2025", "Me");

            application.CreateRibbonPanel(Tab.AddIns, "Test");

            var path = Assembly.GetExecutingAssembly().Location;

            var pushButtonDataRenameSheet = new PushButtonData("RenameSheet", "Rename Sheet", path, "RevitAddIn1.Parameter.RenameSheet.RenameSheetCmd");

            pushButtonDataRenameSheet.Image = new BitmapImage(new Uri(@"C:\\Users\\Windows\\Documents\\GitHub\\Revit2024\\RevitAddin3pointTML\\RevitAddIn1\\Resources\\Icons\\RibbonIcon16.png"));

            pushButtonDataRenameSheet.LargeImage = new BitmapImage(new Uri(@"C:\\Users\\Windows\\Documents\\GitHub\\Revit2024\\RevitAddin3pointTML\\RevitAddIn1\\Resources\\Icons\\RibbonIcon32.png"));

            var pushButtonDataCreateSheet = new PushButtonData("CreateSheet", "Create Sheet", path, "RevitAddIn1.EditingCreating.CreateSheet1.CreateSheetCmd");

            pushButtonDataCreateSheet.Image = new BitmapImage(new Uri(@"C:\\Users\\Windows\\Documents\\GitHub\\Revit2024\\RevitAddin3pointTML\\RevitAddIn1\\Resources\\Icons\\RibbonIcon16.png"));

            pushButtonDataCreateSheet.LargeImage = new BitmapImage(new Uri(@"C:\\Users\\Windows\\Documents\\GitHub\\Revit2024\\RevitAddin3pointTML\\RevitAddIn1\\Resources\\Icons\\rename.png"));

            pushButtonDataCreateSheet.ToolTip = "Create sheets by import data from Excel";

            var sbd = new PulldownButtonData("View-Sheet", "View-Sheet");
            var sb = panelMe.AddItem(sbd) as PulldownButton;

            sb.AddPushButton(pushButtonDataCreateSheet);
            sb.AddPushButton(pushButtonDataRenameSheet);

            //panelMe.AddItem(pushButtonDataRenameSheet);
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Cancelled;
        }
    }
}
