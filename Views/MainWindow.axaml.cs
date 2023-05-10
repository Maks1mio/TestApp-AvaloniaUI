using Avalonia.Controls;
using System;

namespace TestApp_AvaloniaUI.Views
{
    public partial class MainWindow : Window
    {
        private double previousValue = 0;
        private double currentValue = 0;
        private string operation = "";

        public MainWindow()
        {
            InitializeComponent();

            button1.Click += (_, _) => {
                AppendNumber(1);
            };

            button2.Click += (_, _) => {
                AppendNumber(2);
            };

            button3.Click += (_, _) => {
                AppendNumber(3);
            };

            button4.Click += (_, _) => {
                AppendNumber(4);
            };

            button5.Click += (_, _) => {
                AppendNumber(5);
            };

            button6.Click += (_, _) => {
                AppendNumber(6);
            };

            button7.Click += (_, _) => {
                AppendNumber(7);
            };

            button8.Click += (_, _) => {
                AppendNumber(8);
            };

            button9.Click += (_, _) => {
                AppendNumber(9);
            };

            button0.Click += (_, _) => {
                AppendNumber(0);
            };

            buttonPerc.Click += (_, _) => {
                if (operation == "")
                {
                    currentValue = double.Parse(test.Text);
                    currentValue /= 100;
                    test.Text = currentValue.ToString();
                }
            };

            buttonX.Click += (_, _) => {
                SetOperation("*");
            };

            buttonMinus.Click += (_, _) => {
                SetOperation("-");
            };

            buttonComma.Click += (_, _) => {
                if (!test.Text.Contains(","))
                {
                    test.Text += ",";
                }
            };

            buttonPlus.Click += (_, _) => {
                SetOperation("+");
            };

            buttonExa.Click += (_, _) => {
                if (operation != "")
                {
                    CalculateResult();
                    previousValue = 0;
                    currentValue = 0;
                }
            };
        }

        private void AppendNumber(int number)
        {
            if (test.Text == "0")
            {
                test.Text = number.ToString();
            }
            else
            {
                test.Text += number.ToString();
            }

            currentValue = double.Parse(test.Text);
        }

        private void SetOperation(string op)
        {
            if (previousValue == 0)
            {
                previousValue = double.Parse(test.Text);
            }
            else
            {
                if (operation != "")
                {
                    CalculateResult();
                }
            }

            operation = op;
            test.Text = "";
        }

        private void CalculateResult()
        {
            switch (operation)
            {
                case "+":
                    previousValue += currentValue;
                    test.Text = previousValue.ToString();
                    break;
                case "-":
                    previousValue -= currentValue;
                    test.Text = previousValue.ToString();
                    break;
                case "*":
                    previousValue *= currentValue;
                    test.Text = previousValue.ToString();
                    break;
                case "^":
                    previousValue = Math.Pow(previousValue, currentValue);
                    test.Text = previousValue.ToString();
                    break;
            }

            operation = "";
            currentValue = 0;
        }
    }
}