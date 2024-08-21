using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai7Updater
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class WallUpdaterCmd : ExternalCommand
    {
        public override void Execute()
        {
            // Register wall updater with Revit
            WallUpdater updater = new WallUpdater(UiApplication.ActiveAddInId);
            UpdaterRegistry.RegisterUpdater(updater);

            // Change Scope = any Wall element
            ElementClassFilter wallFilter = new ElementClassFilter(typeof(Wall));

            // Change type = element addition
            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), wallFilter,
                Element.GetChangeTypeElementAddition());
            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), wallFilter,
                Element.GetChangeTypeGeometry());

        }
    }
}