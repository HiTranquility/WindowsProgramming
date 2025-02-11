using System;
using System.Windows.Forms;
using Day1.Functions;

namespace Day1
{
    public partial class ComplexNumberForm : Form
    {
        private ComplexNumber number1 = new ComplexNumber();
        private ComplexNumber number2 = new ComplexNumber();
        public ComplexNumberForm()
        {
            InitializeComponent();
        }

        private void firstButton_Click(object sender, EventArgs e)
        {
            number1.RealPart = Int32.Parse(realTextBox.Text);
            number1.ImaginaryPart = Int32.Parse(imaginaryTextBox.Text);
            realTextBox.Clear();
            imaginaryTextBox.Clear();
            statusLabel.Text = "First Complex Number is: " + number1.ToString();
        }

        private void secondButton_Click(object sender, EventArgs e)
        {
            number2.RealPart = Int32.Parse(realTextBox.Text);
            number2.ImaginaryPart = Int32.Parse(imaginaryTextBox.Text);
            realTextBox.Clear();
            imaginaryTextBox.Clear();
            statusLabel.Text = "Second Complex Number is: " + number2.ToString();
        }

        private void substractButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = number1.ToString() + " - (" + number2.ToString() + ") = " + (number1 - number2).ToString();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = number1.ToString() + " + (" + number2.ToString() + ") = " + (number1 + number2).ToString();
        }
        private void multiplyButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = number1.ToString() + " * (" + number2.ToString() + ") = " + (number1 * number2).ToString();
        }

        private void realTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void realLabel_Click(object sender, EventArgs e)
        {

        }

        private void ComplexNumberForm_Load(object sender, EventArgs e)
        {

        }

        private void statusLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
