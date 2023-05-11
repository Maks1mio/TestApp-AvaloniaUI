using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestApp_AvaloniaUI.Views
{
    enum Operation
    {
        None,

        Multiplication,

        Addition,

        Subtraction
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
        private double _previousValue;
        private double _currentValue;
        private Operation _operation;

        public MainWindow()
        {
            InitializeComponent();

            var buttonNamePrefix = "button";
            foreach (var buttonIndex in 0..9)
            {
                var button = this.Get<Button>($"{buttonNamePrefix}{buttonIndex}");
                button.Click += (_, _) => AppendNumber(buttonIndex);
            }

            buttonPerc.Click += (_, _) =>
            {
                if (_operation == Operation.None)
                    return;

                _currentValue = double.Parse(test.Text);
                _currentValue /= 100;
                test.Text = _currentValue.ToString();
            };

            buttonX.Click += (_, _) =>
            {
                SetOperation(Operation.Multiplication);
            };

            buttonMinus.Click += (_, _) =>
            {
                SetOperation(Operation.Subtraction);
            };

            buttonComma.Click += (_, _) =>
            {
                if (test.Text.Contains(','))
                    return;

                test.Text += ",";
            };

            buttonPlus.Click += (_, _) =>
            {
                SetOperation(Operation.Addition);
            };

            buttonExa.Click += (_, _) =>
            {
                if (_operation == Operation.None)
                    return;

                CalculateResult();
                _previousValue = 0;
                _currentValue = 0;
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

            _currentValue = double.Parse(test.Text);
        }

        private void SetOperation(Operation operation)
        {
            if (_previousValue == 0)
            {
                _previousValue = double.Parse(test.Text);
            }
            else
            {
                if (_operation == Operation.None)
                   return;

                CalculateResult();
            }

            _operation = operation;
            test.Text = string.Empty;
        }

        private void CalculateResult()
        {
            _previousValue = _operation switch
            {
                Operation.Addition => _previousValue + _currentValue,
                Operation.Subtraction => _previousValue - _currentValue,
                Operation.Multiplication => _previousValue * _currentValue,
                _ => throw new ArgumentException()
            };

            test.Text = _previousValue.ToString();
            _operation = Operation.None;
            _currentValue = 0;
        }
    }
}