using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator01
{
    public class Calculator
    {
        private StringBuilder currentNumber;
        private string currentFunction;
        private bool toBreak = false;
        
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
                if(char.IsLetter(expression[i]))
                {
                    CalculateFunction(expression, outputRecord,ref i);
                    CalculateTrigonometricFunction(expression, outputRecord, ref i);
                }
             }
            EmptyCurrentNumber(outputRecord);
            EmpryTheOperatorStack(operators, outputRecord);
            return CalcReversedPolishRecord(outputRecord, index);
        }

        private void CalculateTrigonometricFunction(String expression, List<string> outputRecord, ref int i)
        {
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
            else if (char.ToLower(expression[i]) == functions.cotg.ToString()[0] && char.ToLower(expression[i + 1]) == functions.cotg.ToString()[1]
                && char.ToLower(expression[i + 2]) == functions.cotg.ToString()[2] && char.ToLower(expression[i + 2]) == functions.cotg.ToString()[3])
            {
                var result = CalcFuncWithOneParameter(i, expression, functions.cotg.ToString());
                outputRecord.Add(result.Result.ToString());
                i = result.Index;
            }
        }

        private void CalculateFunction(String expression, List<string> outputRecord,ref int i)
        {
            
            if (char.ToLower(expression[i]) == functions.pow.ToString()[0] && char.ToLower(expression[i + 1]) == functions.pow.ToString()[1]
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
            i = (int)i;
        }

        public void EmpryTheOperatorStack(Stack<string> operators, List<string> outputRecord)
        {
            if (operators.Count != 0)
            {
                var count = operators.Count;
                for (int i = 0; i < count; i++)
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
            int newOperatorPriority = Operations.GetOperatorPriority(theOperator);
            int topOfStackOperatorPriority = 0;
            if (operators.Count != 0)
            {
                topOfStackOperatorPriority = Operations.GetOperatorPriority(operators.Peek()[0]);
            }

            if (newOperatorPriority <= topOfStackOperatorPriority && topOfStackOperatorPriority != 0 && newOperatorPriority != 0
                 && topOfStackOperatorPriority != 1 && newOperatorPriority != 1)
            {
                return false;
            }
            return true;
        }


        private ResultModel CalcFuncWithOneParameter(int currentIndex, string expression, string operation)
        {
            ResultModel res = null;
            List<string> record = new List<string>();
            Stack<string> operators = new Stack<string>();
            currentFunction = operation;
            res = ReadExpression(expression, currentIndex + operation.Length, record, operators);
            return new ResultModel(res.Index, Operations.DoOperationWithAOperand(operation, res.Result));
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
                if (expression[i] == functions.log.ToString()[0] &&  expression[i + 1] == functions.log.ToString()[1]
                    && expression[i + 2] == functions.log.ToString()[2])
                {
                    var res = CalcFunctionWithTwoParameters(i, expression, functions.log.ToString());
                    tempExpression.Append(res.Result);
                    i = res.Index;
                }
                else if (expression[i] == functions.pow.ToString()[0] && expression[i + 1] == functions.pow.ToString()[1]
                    && expression[i + 2] == functions.pow.ToString()[2])
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
            return new ResultModel(currentIndexPow, Operations.DoOperationWithTwoOperands(operation, resX.Result, resY.Result));
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
                            result = Operations.DoOperationWithTwoOperands(record[i], number1, 0);
                        }
                        else
                        {
                            double number2 = numbers.Pop();
                            result = Operations.DoOperationWithTwoOperands(record[i], number2, number1);
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
    }
}

