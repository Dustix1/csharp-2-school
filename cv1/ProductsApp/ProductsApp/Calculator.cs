using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApp
{
    public delegate int Operation(int x, int y);

    public class ResultEventArgs : EventArgs
    {
        public int Result {  get; set; }
    }

    public class Calculator
    {
        public int X { get; set; }
        public int Y { get; set; }

        public event EventHandler onSetXY;
        public event EventHandler<ResultEventArgs> onCompute;

        public void SetXY(int x, int y)
        {
            X = x;
            Y = y;

            onSetXY(this, new EventArgs());
        }

        public void Execute(Operation op)
        {
            int result = op.Invoke(X, Y);
            Console.WriteLine(result);

            onCompute(this, new ResultEventArgs() { Result = result });
        }
    }
}
