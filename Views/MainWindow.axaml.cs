using Avalonia.Controls;
using Avalonia.Input;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using TestApp_AvaloniaUI.Features;

namespace TestApp_AvaloniaUI.Views
{
    public partial class MainWindow : Window
    {
        private static readonly char[] _operationSymbols;

        static MainWindow()
        {
            _operationSymbols = new char[] { '+', '-', '*', '/' };
        }

        public MainWindow()
        {
            InitializeComponent();
            
            InitializeOperationButton();

            buttonTransfer.Click += (_, _) =>
            {
                buttonTransfer.IsEnabled = false;
                inputText.Text += resultText.Text;
                resultText.Text = string.Empty;
            };

            buttonBack.Click += (_, _) =>
            {
                if (!string.IsNullOrEmpty(inputText.Text))
                {
                    inputText.Text = inputText.Text.Remove(inputText.Text.Length - 1);
                }
            };

            buttonClear.Click += (_, _) =>
            {
                buttonTransfer.IsEnabled = false;
                inputText.Text = string.Empty;
                resultText.Text = string.Empty;
            };

            buttonComma.Click += (_, _) =>
            {
                string result = Calculator.Comma(e: inputText.Text, _operationSymbols);
                inputText.Text += result;
            };

            buttonEquals.Click += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(inputText.Text) || IsInputEndingWithAnyOperation())
                    return;

                CalculateResult();
            };
        }

        private void InitializeOperationButton()
        {
            var buttonNamePrefix = "button";
            foreach (var buttonIndex in 0..9)
            {
                var button = this.Get<Button>($"{buttonNamePrefix}{buttonIndex}");
                button.Click += (_, _) => AppendNumber(buttonIndex);
            }

            var operationButtons = new Dictionary<Button, Operation>
            {
                [buttonDivide] = Operation.Division,
                [buttonMultiply] = Operation.Multiplication,
                [buttonSubtract] = Operation.Subtraction,
                [buttonAdd] = Operation.Addition,
            };

            var parenthesisButtons = new Dictionary<Button, Operation>
            {
                [buttonLeftParenthesis] = Operation.LeftParenthesis,
                [buttonRightParenthesis] = Operation.RightParenthesis
            };

            foreach (var pair in operationButtons)
            {
                pair.Key.Click += (_, _) => SetOperation(pair.Value);
            }

            foreach (var pair in parenthesisButtons)
            {
                pair.Key.Click += (_, _) => SetParenthesis(pair.Value);
            }
        }

        private bool IsInputEndingWithAnyOperation()
        {
            return _operationSymbols.Any(predicate: x => inputText.Text.EndsWith(x));
        }

        private void AppendNumber(int number)
        {
            if (inputText.Text == "0") 
            {
                inputText.Text = number.ToString();
                return;
            }
            inputText.Text += number.ToString();
        }

        private void SetOperation(Operation operation)
        {
            if (string.IsNullOrWhiteSpace(inputText.Text) || IsInputEndingWithAnyOperation())
                return;

            inputText.Text += operation switch
            {
                Operation.Addition => "+",
                Operation.Subtraction => "-",
                Operation.Multiplication => "*",
                Operation.Division => "/",
               _ => throw new ArgumentException()
            };
        }

        private void SetParenthesis(Operation operation)
        {
            // Check if the parenthesis is balanced
            int rightCount = inputText.Text.Count(c => c == ')');

            inputText.Text += operation switch
            {
                Operation.LeftParenthesis => "(", // Always add left parenthesis
                Operation.RightParenthesis => rightCount >= inputText.Text.Length / 2 ? "" : ")", // Do not add right parenthesis if there are more than half of the input length
                _ => throw new ArgumentException()
            };
        }

        private void CalculateResult()
        {
            try
            {
                var result = Calculator.Resultator(e: inputText.Text);
                resultText.Text = result.ToString();
                buttonTransfer.IsEnabled = true;
            }
            catch { resultText.Text = "Error:"; };

            inputText.Text = string.Empty;
        }
    }
}