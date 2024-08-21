using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;

namespace RevitAddIn1.Utils
{
    public static class DocumentUtils
    {
        private static Autodesk.Revit.UI.ExternalEvent externalEvent;
        private static ExternalEventHandler externalEventHandler;
        public static Autodesk.Revit.UI.ExternalEvent ExternalEvent
        {
            get
            {
                if (externalEvent == null)

                    externalEvent = Autodesk.Revit.UI.ExternalEvent.Create(ExternalEventHandler);
                return externalEvent;
            }
            set => externalEvent = value;
        }

        public static IExternalEventHandler ExternalEventHandler
        {
            set;
            get;
        }

        public static Document Document;
        public static Element ToElement(this Reference rf) => Document.GetElement(rf);

        public static Element ToElement(this ElementId id) => Document.GetElement(id);
    }
}
