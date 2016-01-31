using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc2
{
    public class ResultModel
    {
        public ResultModel(int index, double result)
        {
            Index = index;
            Result = result;
        }
        public int Index { get; set; }
        public double Result { get; set; }
    }
}
