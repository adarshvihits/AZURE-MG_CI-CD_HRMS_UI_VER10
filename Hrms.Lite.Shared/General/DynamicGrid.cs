using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.General
{
  public  class DynamicGrid
    {
        public List<string> Head { get; set; }
        public List<RowData> Rows { get; set; }
        public List<string> Footer { get; set; }
        public int TotalRows { get; set; }
        public DataSet DataSet { get; set; }
    }
    public class RowData
    {
        public List<string> Columns { get; set; }
    }
}
