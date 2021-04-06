using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_cv9
{
    public class Calculator
    {
        public string Memory;

        private string firstNumber;
        private string secondNumber;
        private double result;
        private string operand;
        string error;
        private enum State
        {
            FirstNumber,
            Operation,
            SecondNumber,
            Result
        };
        private State _state;
        public Calculator()
        {
            Memory = "";
            firstNumber = "";
            secondNumber = "";
            operand = "";
            error = "";
            result = 0;
            _state = State.FirstNumber;
        }
        public string Display
        {
            get
            {
                switch (_state)
                {
                    case State.FirstNumber:
                        return firstNumber;
                    case State.Operation:
                        return firstNumber + operand;
                    case State.SecondNumber:
                        return firstNumber + operand + secondNumber;
                    case State.Result:
                        if (error.Length > 0) return error;
                        return result.ToString();
                    default:
                        return "";
                }
            }
        }
        public void Button(string buttonContent)
        {
            switch (buttonContent)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    processNumberPress(buttonContent);
                    break;
                case "MC":
                case "MR":
                case "MS":
                case "M+":
                case "M-":
                    processMemoryOperation(buttonContent);
                    break;
                case "<-":
                case "CE":
                case "C":
                    processClearOperation(buttonContent);
                    break;
                case "+/-":
                case "+":
                case "-":
                case "/":
                case "*":
                case ",":
                    processOperand(buttonContent);
                    break;
                case "=":
                    calculateResult();
                    break;
            }
        }
        private void negateString(ref string input)
        {
            if (input.Length == 0 || input == "0") return;
            if (input[0] == '-') input = input.Substring(1);
            else input = "-" + input;
        }
        private string negateString(string input)
        {
            if (input.Length == 0 || input == "0") return "";
            if (input[0] == '-') return input.Substring(1);
            else return "-" + input;
        }
        private void processNumberPress(string number)
        {
            switch (_state)
            {
                case State.FirstNumber:
                    firstNumber += number;
                    break;
                case State.SecondNumber:
                    secondNumber += number;
                    break;
                case State.Operation:
                    secondNumber += number;
                    _state = State.SecondNumber;
                    break;
                case State.Result:
                    firstNumber += number;
                    _state = State.FirstNumber;
                    break;
            }
        }
        private void processMemoryOperation(string operationButton)
        {

        }
        private void processClearOperation(string clearButton)
        {
            switch (clearButton)
            {
                case "C":
                    _state = State.FirstNumber;
                    firstNumber = "";
                    break;
                case "CE":
                    switch (_state)
                    {
                        case State.FirstNumber:
                            firstNumber = "";
                            break;
                        case State.SecondNumber:
                            secondNumber = "";
                            break;
                        case State.Result:
                            firstNumber = "";
                            _state = State.FirstNumber;
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
        private void processOperand(string operandButton)
        {
            switch (operandButton)
            {
                case "+/-":
                    switch (_state)
                    {
                        case State.FirstNumber:
                            negateString(ref firstNumber);
                            break;
                        case State.Operation:
                            secondNumber = negateString(firstNumber);
                            _state = State.SecondNumber;
                            break;
                        case State.SecondNumber:
                            negateString(ref secondNumber);
                            break;
                        case State.Result:
                            firstNumber = (-result).ToString();
                            _state = State.FirstNumber;
                            break;
                    }
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    switch (_state)
                    {
                        case State.FirstNumber:
                            if (firstNumber == "") firstNumber = "0";
                            break;
                        case State.SecondNumber:
                            calculateResult();
                            firstNumber = result.ToString();
                            break;
                        case State.Result:
                            firstNumber = result.ToString();
                            break;
                        default:
                            break;
                    }
                    operand = operandButton;
                    _state = State.Operation;
                    break;
                case ",":
                    switch (_state)
                    {
                        case State.FirstNumber:
                            firstNumber += ",";
                            break;
                        case State.SecondNumber:
                            secondNumber += ",";
                            break;
                        case State.Operation:
                            secondNumber = "0,";
                            break;
                        case State.Result:
                            firstNumber = "0,";
                            _state = State.FirstNumber;
                            break;
                    }
                    break;
            }

        }
        private void calculateResult()
        {

            if (_state == State.Result) return;
            if (firstNumber == "") firstNumber = "0";
            if (secondNumber == "") secondNumber = "0";
            error = "";
            switch (operand)
            {
                case "+":
                    result = Double.Parse(firstNumber) + Double.Parse(secondNumber);
                    break;
                case "-":
                    result = Double.Parse(firstNumber) - Double.Parse(secondNumber);
                    break;
                case "*":
                    result = Double.Parse(firstNumber) * Double.Parse(secondNumber);
                    break;
                case "/":
                    if (secondNumber == "0") error = "NaN";
                    else result = Double.Parse(firstNumber) / Double.Parse(secondNumber);
                    break;
                default:
                    break;
            }
            firstNumber = "";
            secondNumber = "";
            _state = State.Result;
        }
    }
}
