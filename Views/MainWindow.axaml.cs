using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestApp_AvaloniaUI.Views
{
    enum Operation
    {
        None,

        Multiplication,

        Addition,

        Subtraction,

        Division,

        Comma,
    }

    static class RangeExtensions
    {
        public static IEnumerator<int> GetEnumerator(this Range range)
        {
            for (var i = range.Start.Value; i < range.End.Value; i++)
                yield return i;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var buttonNamePrefix = "button";
            foreach (var buttonIndex in 0..10)
            {
                var button = this.Get<Button>($"{buttonNamePrefix}{buttonIndex}");
                button.Click += (_, _) => AppendNumber(buttonIndex);
            }

            buttonTransfer.Click += (_, _) =>
            {
                buttonTransfer.IsEnabled = false;
                inputText.Text = resultText.Text;
                resultText.Text = null;
            };

            buttonDivide.Click += (_, _) =>
            {
                SetOperation(Operation.Division);
            };

            buttonMultiply.Click += (_, _) =>
            {
                SetOperation(Operation.Multiplication);
            };

            buttonSubtract.Click += (_, _) =>
            {
                SetOperation(Operation.Subtraction);
            };

            buttonComma.Click += (_, _) =>
            {
                if (inputText.Text.Contains('.'))
                    return;

                SetOperation(Operation.Comma);
            };

            buttonAdd.Click += (_, _) =>
            {
                SetOperation(Operation.Addition);
            };

            buttonEquals.Click += (_, _) =>
            {
                if (string.IsNullOrEmpty(inputText.Text) || inputText.Text.EndsWith("+") || inputText.Text.EndsWith("-") || inputText.Text.EndsWith("*") || inputText.Text.EndsWith("/"))
                    return;
                CalculateResult();
            };
        }

        private void AppendNumber(int number)
        {
            if (inputText.Text == "0")
            {
                inputText.Text = number.ToString();
            }
            else
            {
                inputText.Text += number.ToString();
            }
        }

        private void SetOperation(Operation operation)
        {
            if (string.IsNullOrEmpty(inputText.Text) || inputText.Text.EndsWith("+") || inputText.Text.EndsWith("-") || inputText.Text.EndsWith("*") || inputText.Text.EndsWith("/"))
            {
                return;
            }

            switch (operation)
            {
                case Operation.Addition:
                    inputText.Text += "+";
                    break;
                case Operation.Subtraction:
                    inputText.Text += "-";
                    break;
                case Operation.Multiplication:
                    inputText.Text += "*";
                    break;
                case Operation.Division:
                    inputText.Text += "/";
                    break;
                case Operation.Comma:
                    if (!string.IsNullOrEmpty(inputText.Text) && !"+-*/".Contains(inputText.Text.Last()))
                    {
                        string[] parts = inputText.Text.Split(new char[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
                        string lastPart = parts.Last();
                        if (!lastPart.Contains(','))
                        {
                            // добавляем запятую к тексту
                            inputText.Text += ",";
                        }
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void CalculateResult()
        {
            try
            {
                string expression = inputText.Text;
                expression = expression.Replace(',', '.');
                DataTable table = new DataTable();
                var result = table.Compute(expression, null);

                inputText.Text = null;
                resultText.Text = result.ToString();
                buttonTransfer.IsEnabled = true;
            }
            catch
            {
                // Handle the exception here
                // For example, you could display an error message to the user
                inputText.Text = null;
                resultText.Text = "Error:";
                return;
            }
        }

    }
}