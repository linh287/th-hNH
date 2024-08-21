﻿using System.Data;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using CommunityToolkit.Mvvm.Messaging;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.ViewModels;
using RevitAddIn1.Views;
using OperationCanceledException = Autodesk.Revit.Exceptions.OperationCanceledException;

namespace RevitAddIn1.Commands
{
    /// <summary>
    ///     External command entry point invoked from the Revit interface
    /// </summary>
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class PickWallsCmd : ExternalCommand
    {
        public override void Execute()
        {
            try
            {
                var referent = UiDocument.Selection.PickObject(ObjectType.Element, new WallSelectionFilter() ,"Chon doi tuong Wall");
                var ele = Document.GetElement(referent);
                MessageBox.Show(ele.Name);
            }
            catch (OperationCanceledException )
            {
                MessageBox.Show("Ban da huy chon", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
  
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