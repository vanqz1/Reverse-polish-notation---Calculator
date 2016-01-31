using System;
using System.Collections.Generic;
using System.Text;
using calc2;

namespace Calculator01
{
    public class Calculator
    {
        private StringBuilder currentNumber;
        private string currentFunction;
        private bool toBreak = false;
        private readonly double maxLimit = Math.Pow(10, -10);
        private enum functions { sin, cos, tg, cotg, log, ln, pow, root, exp, sqrt }
        public Calculator()
        {
            currentNumber = new StringBuilder();
            currentFunction = null;
        }

        public ResultModel ReadExpression(String expression, int currentIndex, List<string> outputRecord, Stack<string> operators)
        {
            int index = 0;
            for (int i = currentIndex; i < expression.Length; i++)
            {
                if (expression[i] == 'p' && expression[i + 1] == 'i')
                {
                    i += 1;
                    currentNumber.Append(Math.PI.ToString());
                    continue;
                }
                if (expression[i] == 'e')
                {
                    currentNumber.Append(Math.E.ToString());
                    continue;
                }
                if (char.IsNumber(expression[i]))
                {
                    currentNumber.Append(expression[i]);
                }
                else
                {
                    if (currentNumber.Length != 0)
                    {
                        if (expression[i].Equals('.'))
                        {
                            currentNumber.Append(',');
                        }
                        else
                        {
                            outputRecord.Add(currentNumber.ToString());
                            currentNumber.Clear();
                        }
                    }
                }

                if (expression[i].Equals('+') || expression[i] == '-' || expression[i] == '/'
                    || expression[i] == '*' || expression[i] == '(' || expression[i] == ')')
                {
                    ArrangeOperator(expression[i], outputRecord, operators);
                    if (expression[i] == ')' && toBreak)
                    {
                        index = i;
                        break;
                    }
                }
                if (char.ToLower(expression[i]) == functions.sin.ToString()[0] && char.ToLower(expression[i + 1]) == functions.sin.ToString()[1]
                    && char.ToLower(expression[i + 2]) == functions.sin.ToString()[2])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.sin.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.cos.ToString()[0] && char.ToLower(expression[i + 1]) == functions.cos.ToString()[1]
                    && char.ToLower(expression[i + 2]) == functions.cos.ToString()[2])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.cos.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.tg.ToString()[0] && char.ToLower(expression[i + 1]) == functions.tg.ToString()[1])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.tg.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.cotg.ToString()[0] && char.ToLower(expression[i + 1]) == functions.cotg.ToString()[1] &&
                    char.ToLower(expression[i + 2]) == functions.cotg.ToString()[2] && char.ToLower(expression[i + 2]) == functions.cotg.ToString()[3])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.cotg.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.pow.ToString()[0] && char.ToLower(expression[i + 1]) == functions.pow.ToString()[1]
                    && char.ToLower(expression[i + 2]) == functions.pow.ToString()[2])
                {
                    var result = CalcFunctionWithTwoParameters(i, expression, functions.pow.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.root.ToString()[0] && char.ToLower(expression[i + 1]) == functions.root.ToString()[1]
                    && char.ToLower(expression[i + 2]) == functions.root.ToString()[2] && char.ToLower(expression[i + 3]) == functions.root.ToString()[3])
                {
                    var result = CalcFunctionWithTwoParameters(i, expression, functions.root.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.log.ToString()[0] && char.ToLower(expression[i + 1]) == functions.log.ToString()[1]
                    && char.ToLower(expression[i + 2]) == functions.log.ToString()[2])
                {
                    var result = CalcFunctionWithTwoParameters(i, expression, functions.log.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.ln.ToString()[0] && char.ToLower(expression[i + 1]) == functions.ln.ToString()[1])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.ln.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.sqrt.ToString()[0] && char.ToLower(expression[i + 1]) == functions.sqrt.ToString()[1]
                && char.ToLower(expression[i + 2]) == functions.sqrt.ToString()[2] && char.ToLower(expression[i + 3]) == functions.sqrt.ToString()[3])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.sqrt.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
                else if (char.ToLower(expression[i]) == functions.exp.ToString()[0] && char.ToLower(expression[i + 1]) == functions.exp.ToString()[1]
                    && char.ToLower(expression[i + 2]) == functions.exp.ToString()[2])
                {
                    var result = CalcFuncWithOneParameter(i, expression, functions.exp.ToString());
                    outputRecord.Add(result.Result.ToString());
                    i = result.Index;
                }
            }

            EmptyCurrentNumber(outputRecord);
            EmpryTheOperatorStack(operators, outputRecord);
            return CalcReversedPolishRecord(outputRecord, index);
        }

        public void EmpryTheOperatorStack(Stack<string> operators, List<string> outputRecord)
        {
            if (operators.Count != 0)
            {
                for (int i = 0; i <= operators.Count; i++)
                {
                    outputRecord.Add(operators.Pop());
                }
            }
        }

        public void EmptyCurrentNumber(List<string> outputRecord)
        {
            if (currentNumber.Length != 0)
            {
                outputRecord.Add(currentNumber.ToString());
                currentNumber.Clear();
            }
        }
        private void ArrangeOperator(char theOperator, List<string> record, Stack<string> theOperators)
        {
            if (!DoesTopOfStackContainsOperatorWithBiggerPriority(theOperator, theOperators))
            {
                record.Add(theOperators.Pop());
            }
            if (theOperator == ')')
            {
                bool isValidExpresion = false;
                int operatorsLength = theOperators.Count;
                for (int i = 0; i < operatorsLength; i++)
                {
                    if (Char.Parse(theOperators.Peek()) != '(')
                    {
                        record.Add(theOperators.Pop());
                    }
                    else
                    {
                        isValidExpresion = true;
                        theOperators.Pop();
                        break;
                    }
                }
                if (!isValidExpresion)
                {
                    throw new Exception("Invalid expression - please check brackets");
                }

                if (currentFunction != null && theOperators.Count == 0)
                {
                    currentFunction = null;
                    toBreak = true; ;
                }
            }
            if (theOperator != ')')
            {
                theOperators.Push(theOperator.ToString());
            }
        }

        private bool DoesTopOfStackContainsOperatorWithBiggerPriority(char theOperator, Stack<string> operators)
        {
            int newOperatorPriority = GetOperatorPriority(theOperator);
            int topOfStackOperatorPriority = 0;
            if (operators.Count != 0)
            {
                topOfStackOperatorPriority = GetOperatorPriority(operators.Peek()[0]);
            }

            if (newOperatorPriority <= topOfStackOperatorPriority && topOfStackOperatorPriority != 0 && newOperatorPriority != 0
                 && topOfStackOperatorPriority != 1 && newOperatorPriority != 1)
            {
                return false;
            }
            return true;
        }

        private int GetOperatorPriority(char theOperator)
        {
            switch (theOperator)
            {
                case '(':
                    return 1;
                    break;
                case ')':
                    return 1;
                    break;
                case '-':
                    return 2;
                    break;
                case '+':
                    return 2;
                    break;
                case '/':
                    return 3;
                    break;
                case '*':
                    return 3;
                    break;
                default:
                    return 0;
            }
        }

        private ResultModel CalcFuncWithOneParameter(int currentIndex, string expression, string operation)
        {
            ResultModel res = null;
            List<string> record = new List<string>();
            Stack<string> operators = new Stack<string>();
            currentFunction = operation;
            res = ReadExpression(expression, currentIndex + operation.Length, record, operators);
            return new ResultModel(res.Index, DoOperationWithAOperand(operation, res.Result));
        }

        private ResultModel CalcFunctionWithTwoParameters(int currentIndex, string expression, string operation)
        {
            ResultModel resX = null;
            ResultModel resY = null;
            List<string> recordX = new List<string>();
            Stack<string> operatorsX = new Stack<string>();
            List<string> recordY = new List<string>();
            Stack<string> operatorsY = new Stack<string>();
            StringBuilder tempExpression = new StringBuilder();
            int numberOpenBrackets = 0;
            int numberCloseBrackets = 0;
            int currentIndexPow = 0;
            for (int i = currentIndex + operation.Length; i < expression.Length; i++)
            {
                if (expression[i] == functions.log.ToString()[0] &&
                    expression[i + 1] == functions.log.ToString()[1] && expression[i + 2] == functions.log.ToString()[2])
                {
                    var res = CalcFunctionWithTwoParameters(i, expression, functions.log.ToString());
                    tempExpression.Append(res.Result);
                    i = res.Index;
                }
                else if (expression[i] == functions.pow.ToString()[0] &&
                    expression[i + 1] == functions.pow.ToString()[1] && expression[i + 2] == functions.pow.ToString()[2])
                {
                    var res = CalcFunctionWithTwoParameters(i, expression, functions.pow.ToString());
                    tempExpression.Append(res.Result);
                    i = res.Index;
                }
                else if (expression[i] == functions.root.ToString()[0] && expression[i + 1] == functions.root.ToString()[1]
                    && expression[i + 2] == functions.root.ToString()[2] && expression[i + 3] == functions.root.ToString()[3])
                {
                    var res = CalcFunctionWithTwoParameters(i, expression, functions.root.ToString());
                    tempExpression.Append(res.Result);
                    i = res.Index;
                }
                else
                {
                    if (expression[i] == '(')
                    {
                        numberOpenBrackets++;
                        tempExpression.Append(expression[i]);
                    }
                    else if (expression[i] == ')')
                    {
                        numberCloseBrackets++;
                        tempExpression.Append(expression[i]);
                    }
                    else if (expression[i] != ',')
                    {
                        tempExpression.Append(expression[i]);
                    }
                    else if (expression[i].Equals('.'))
                    {
                        currentNumber.Append(',');
                    }
                    else
                    {
                        resX = ReadExpression(tempExpression.ToString(), 0, recordX, operatorsX);
                        tempExpression.Clear();
                    }
                    if (numberOpenBrackets == numberCloseBrackets)
                    {
                        resY = ReadExpression(tempExpression.ToString().Substring(0, tempExpression.ToString().Length - 1), 0, recordY, operatorsY);
                        currentIndexPow = i;
                        break;
                    }
                }
            }
            return new ResultModel(currentIndexPow, DoOperationWithTwoOperands(operation, resX.Result, resY.Result));
        }

        private ResultModel CalcReversedPolishRecord(List<string> record, int index)
        {
            Stack<double> numbers = new Stack<double>();
            double result = 0;

            for (int i = 0; i < record.Count; i++)
            {
                double number;
                bool isNumeric = double.TryParse(record[i], out number);
                if (isNumeric)
                {
                    numbers.Push(number);
                    result = number;
                }
                else
                {
                    if (numbers.Count >= 2)
                    {
                        double number1 = numbers.Pop();
                        if (record[i].ToLower().Equals(functions.sin.ToString()))
                        {
                            result = DoOperationWithTwoOperands(record[i], number1, 0);
                        }
                        else
                        {
                            double number2 = numbers.Pop();
                            result = DoOperationWithTwoOperands(record[i], number2, number1);
                        }
                        numbers.Push(result);
                    }
                    else if (numbers.Count == 1)
                    {
                        result = numbers.Pop();
                    }
                    else
                    {
                        throw new Exception("Wrong expression");
                    }
                }
            }
            return new ResultModel(index, Math.Round(result, 15));
        }

        private double DoOperationWithAOperand(string operation, double operand)
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
                    if()
                    {
                    
                    }
                    return Math.Log(operand);
                case "exp":
                    return Math.Pow(Math.E, operand);
                case "sqrt":
                    return Math.Sqrt(operand);
                default:
                    return 0;
            }
        }

        private double DoOperationWithTwoOperands(string operation, double operand1, double operand2)
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
                    return Math.Pow(operand1, 1.0 / operand2);
                case "log":
                    return Math.Log(operand1, operand2);
                default:
                    return 0;
            }
        }
    }
}

