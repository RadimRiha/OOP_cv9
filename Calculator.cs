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

        private double firstNumber;
        private double secondNumber;
        private string operand;
        private double result;
        private bool decimalEntry;
        private int decimalCount;
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
            Display = "";
            Memory = "";
            firstNumber = 0;
            secondNumber = 0;
            operand = "";
            _state = State.FirstNumber;
            decimalEntry = false;
            decimalCount = 0;
        }
        public string Display
        {
            get
            {
                switch (_state)
                {
                    case State.FirstNumber:
                        return firstNumber.ToString();
                    case State.Operation:
                        return firstNumber.ToString() + operand;
                    case State.SecondNumber:
                        return firstNumber.ToString() + operand + secondNumber.ToString();
                    case State.Result:
                        return result.ToString();
                    default:
                        return "";
                }
            }
            private set
            {
                Display = value;
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
                    processNumberPress(Int16.Parse(buttonContent));
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
        private void addToNumber(ref double number, int numberToAdd)
        {
            number = number * 10 + (double)numberToAdd;
        }
        private void processNumberPress(int number)
        {
            switch (_state)
            {
                case State.FirstNumber:
                    addToNumber(ref firstNumber, number);
                    break;
                case State.SecondNumber:
                    addToNumber(ref secondNumber, number);
                    break;
                case State.Operation:
                    addToNumber(ref secondNumber, number);
                    _state = State.SecondNumber;
                    break;
            }
        }
        private void processMemoryOperation(string operationButton)
        {

        }
        private void processClearOperation(string operationButton)
        {

        }
        private void processOperand(string operandButton)
        {
            switch (operandButton)
            {
                case "+/-":
                    if(_state == State.Operation)
                    {
                        secondNumber = -firstNumber;
                        calculateResult();
                    }
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    if (_state == State.SecondNumber)
                    {
                        calculateResult();
                        firstNumber = result;
                    }
                    operand = operandButton;
                    _state = State.Operation;
                    break;
                case ",":
                    decimalEntry = true;
                    decimalCount = 0;
                    break;
            }
            
        }
        private void calculateResult()
        {
            _state = State.Result;
        }
    }
}
