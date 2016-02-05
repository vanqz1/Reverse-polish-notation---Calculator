

namespace Calculator01
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
