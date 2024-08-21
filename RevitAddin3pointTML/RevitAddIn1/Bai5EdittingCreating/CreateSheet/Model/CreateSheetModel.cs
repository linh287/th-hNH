using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Bai5EdittingCreating.CreateSheet.Model
{
    public class CreateSheetModel
    {
        public string SheetNumber {  get; set; }
        public string SheetName
        {
            get; set;
        }
        public string DrawBy
        {
            get; set;
        }
        public string CheckedBy
        {
            get; set;
        }



        public ViewSheet ViewSheet { get; set; }
        public string DrawnBy { get; internal set; }

        public CreateSheetModel(ViewSheet viewSheet)
        {
           
        }

        public CreateSheetModel()
        {
        }
    }
}
