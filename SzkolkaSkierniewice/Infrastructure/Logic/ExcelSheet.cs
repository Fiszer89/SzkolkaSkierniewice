using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SzkolkaSkierniewice.Infrastructure.Logic
{
    public class ExcelSheet
    {
        public DataTable dataTable { get; set; }
        public string heading { get; set; }
        public bool showSrNo { get; set; }
        public string[] columnsToTake { get; set; }

        public ExcelSheet(DataTable dataTable, string heading, bool showSrNo, string[] columnsToTake)
        {
            this.dataTable = dataTable;
            this.heading = heading;
            this.showSrNo = showSrNo;
            this.columnsToTake = columnsToTake;
        }

        public ExcelSheet()
        {
        }
    }
}