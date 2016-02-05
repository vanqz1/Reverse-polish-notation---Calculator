using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator01
{
    public static class Operations
    {
        private static readonly double maxLimit = Math.Pow(10, -10);

        public static double DoOperationWithTwoOperands(string operation, double operand1, double operand2)
        {
            switch (operation)
            {
                case "-":
                    return operand1 - operand2;
                case "+":
                    return operand1 + operand2;
                case "/":
                    return operand1 / operand2;
                case "*":
                    return operand1 * operand2;
                case "pow":
                    return Math.Pow(operand1, operand2);
                case "root":
                    if (operand1 < 0)
                    {
                        throw new Exception("Invalid Root");
                    }
                    return Math.Pow(operand1, 1.0 / operand2);
                case "log":
                    if (operand1 <= 0 || operand2 <= 1)
                    {
                        throw new Exception("Invalid Log");
                    }
                    return Math.Log(operand1, operand2);
                default:
                    return 0;
            }
        }

        public static double DoOperationWithAOperand(string operation, double operand)
        {
            double res = 0;
            switch (operation)
            {

                case "sin":
                    res = Math.Sin(operand);
                    if (res < maxLimit)
                    {
                        return 0;
                    }
                    return res;
                case "cos":
                    res = Math.Cos(operand);
                    if (res < maxLimit)
                    {
                        return 0;
                    }
                    return res;
                case "tg":
                    res = Math.Tan(operand);
                    if (res < maxLimit)
                    {
                        return 0;
                    }
                    return res;
                case "cotg":
                    res = 1 / Math.Tan(operand);
                    if (res < maxLimit)
                    {
                        return 0;
                    }
                    return res;
                case "ln":
                    if (operand <= 0)
                    {
                        throw new Exception("Invalid Ln");
                    }
                    return Math.Log(operand);
                case "exp":
                    return Math.Pow(Math.E, operand);
                case "sqrt":
                    if (operand < 0)
                    {
                        throw new Exception("Invalid Sqrt");
                    }
                    return Math.Sqrt(operand);
                default:
                    return 0;
            }
        }


        public static int GetOperatorPriority(char theOperator)
        {
            switch (theOperator)
            {
                case '(':
                    return 1;
                case ')':
                    return 1;
                case '-':
                    return 2;
                case '+':
                    return 2;
                case '/':
                    return 3;
                case '*':
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
