using System;
using System.Windows.Forms;

namespace ChethanKacham_Assignment02
{
    public partial class CalculatorForm : System.Windows.Forms.Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            this.Text = "Unsigned Integer Calculator";
            this.number1.KeyPress += new KeyPressEventHandler(Number_KeyPress);
            this.number2.KeyPress += new KeyPressEventHandler(Number_KeyPress);
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            Calculator calculator = new Calculator();
            switch (selectedOperation.Text)
            {
                case "+":
                    resultBox.Text = calculator.PerformAddition(number1.Text, number2.Text);
                    break;
                case "-":
                    resultBox.Text = calculator.PerformSubstraction(number1.Text, number2.Text);
                    break;
                case "*":
                    resultBox.Text = calculator.PerformMultiplication(number1.Text, number2.Text);
                    break;
                case "/":
                    resultBox.Text = calculator.PerformDivision(number1.Text, number2.Text);
                    break;
                default:
                    resultBox.Text = "Select valid operation to perform";
                    return;
            }
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow digits to be entered
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // no decimal values or only integers are allowed
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }

            //zeros are not allowed at the start of a number
            if (e.KeyChar == '0' && (sender as TextBox).Text.Length == 0)
            {
                e.Handled = true;
            }
        }
    }
}