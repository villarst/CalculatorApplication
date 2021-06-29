using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorApplication
{
    /*****************************************************************
    A simple calculator that does addition, subtraction, division, and 
    mulitplication. It also does order of operations and has clear 
    button and a delete button. The keys on the keyboard have also been 
    implemented for better use and has the ability to process decimal
    calculations.

    Windows Forms C# Application.

    @author Steven Villarreal.
    @version Summer 2021
    *****************************************************************/

    public partial class CalculatorPage : Form
    {
        // Used to display and draw the form. 
        public CalculatorPage()
        {
            InitializeComponent();
        }

        #region The Equals Key "=" is pressed
        // If the "ENTER" key or "=" button is clicked the equation will
        // be calculated.
        private void equalBtn_Click(object sender, EventArgs e)
        {
            // If the input is bad or nothing is inputted while the "ENTER"
            // key is pressed or the "=" key is pressed, a dialog box will
            // appear letting the user know something is wrong and to try 
            // and fix the incorrect equation.
            try
            {
                // Array that stores the numbers as a string.
                string[] nums = textBox1.Text.Split('+', '-', 'x', '/');
                // Array that stores the numbers as doubles.
                double[] numbers = Array.ConvertAll(textBox1.Text.Split('+', '-', 'x', '/'), double.Parse);

                // Arraylist that stores the signs.
                var signs = new ArrayList();
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (textBox1.Text.Substring(i, 1) == "+" ||
                        textBox1.Text.Substring(i, 1) == "-" ||
                        textBox1.Text.Substring(i, 1) == "x" ||
                        textBox1.Text.Substring(i, 1) == "/")
                    {
                        signs.Add(textBox1.Text.Substring(i, 1));
                    }
                }

                // The length of nums is less than or equal to 1.
                if (nums.Length <= 1)
                {
                    textBox1.Text = nums[0];
                }

                // There are only two numbers to compute. 
                else if (nums.Length == 2)
                {
                    if (signs[0].ToString() == "+")
                    {
                        double result = numbers[0] + numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                    else if (signs[0].ToString() == "-")
                    {
                        double result = numbers[0] - numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                    else if (signs[0].ToString() == "x")
                    {
                        double result = numbers[0] * numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                    else if (signs[0].ToString() == "/")
                    {
                        double result = numbers[0] / numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                }

                // If the amount of numbers if greater than 0
                else if (nums.Length > 2)
                {
                    // There is addition and/or subtraction.
                    if ((signs.Contains("+") || signs.Contains("-")) &&
                        !signs.Contains("/") && !signs.Contains("x"))
                    {
                        int count = 0;
                        var tempList = new ArrayList();
                        for (int i = 0; i < signs.Count; i++)
                        {
                            if (count == 0)
                            {
                                if (signs[i].ToString() == "+")
                                {
                                    tempList.Add(numbers[i] + numbers[i + 1]);
                                    count++;
                                }
                                else if (signs[i].ToString() == "-")
                                {
                                    tempList.Add(numbers[i] - numbers[i + 1]);
                                    count++;
                                }
                            }
                            else
                            {
                                if (signs[i].ToString() == "+")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num + numbers[i + 1]);
                                }
                                else if (signs[i].ToString() == "-")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num - numbers[i + 1]);
                                }
                            }
                        }
                        textBox1.Text = tempList[0].ToString();
                    }

                    // There is mulitplication and/or division.
                    else if ((signs.Contains("/") || signs.Contains("x")) &&
                        !signs.Contains("+") && !signs.Contains("-"))
                    {
                        int count = 0;
                        var tempList = new ArrayList();
                        for (int i = 0; i < signs.Count; i++)
                        {
                            if (count == 0)
                            {
                                if (signs[i].ToString() == "x")
                                {
                                    tempList.Add(numbers[i] * numbers[i + 1]);
                                    count++;
                                }
                                else if (signs[i].ToString() == "/")
                                {
                                    tempList.Add(numbers[i] / numbers[i + 1]);
                                    count++;
                                }
                            }
                            else
                            {
                                if (signs[i].ToString() == "x")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num * numbers[i + 1]);
                                }
                                else if (signs[i].ToString() == "/")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num / numbers[i + 1]);
                                }
                            }
                        }
                        textBox1.Text = tempList[0].ToString();
                    }

                    // There is mulitplication/division along with addition/subtraction.
                    else if ((signs.Contains("/") || signs.Contains("x")) &&
                        (signs.Contains("+") || signs.Contains("-")))
                    {
                        List<double> list1 = numbers.ToList();
                        var tempList = new ArrayList();

                        int i = 0;
                        while (signs.Contains("x") == true || signs.Contains("/") == true)
                        {
                            if (signs[i].ToString() == "x")
                            {
                                signs.RemoveAt(i);
                                list1[i] = (list1[i] * list1[i + 1]);
                                list1.RemoveAt(i + 1);
                            }
                            else if (signs[i].ToString() == "/")
                            {
                                signs.RemoveAt(i);
                                list1[i] = (list1[i] / list1[i + 1]);
                                list1.RemoveAt(i + 1);
                            }
                            else
                            {
                                i++;
                            }
                        }

                        // After the multiplication and division is computed the addition
                        // and/or subtraction is ran through the new equation.
                        if ((signs.Contains("+") || signs.Contains("-")) &&
                        !signs.Contains("/") && !signs.Contains("x"))
                        {
                            int count = 0;
                            var tempList2 = new ArrayList();
                            for (int x = 0; x < signs.Count; x++)
                            {
                                if (count == 0)
                                {
                                    if (signs[x].ToString() == "+")
                                    {
                                        tempList2.Add(list1[x] + list1[x + 1]);
                                        count++;
                                    }
                                    else if (signs[x].ToString() == "-")
                                    {
                                        tempList2.Add(list1[x] - list1[x + 1]);
                                        count++;
                                    }
                                }
                                else
                                {
                                    if (signs[x].ToString() == "+")
                                    {
                                        double num = Convert.ToDouble(tempList2[0]);
                                        tempList2[0] = (num + list1[x + 1]);
                                    }
                                    else if (signs[x].ToString() == "-")
                                    {
                                        double num = Convert.ToDouble(tempList2[0]);
                                        tempList2[0] = (num - list1[x + 1]);
                                    }
                                }
                            }
                            textBox1.Text = tempList2[0].ToString();
                        }
                    }
                }
            }
            // Dialog box for incorrect data or no data entered.
            catch(Exception ex)
            {
                string message = "You did not enter a correct operation.";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
            }
        }
        #endregion

        #region "ENTER" Key Pressed
        // This is for the "ENTER" key being pressed but is the exact same
        // as what is typed above.
        private void equalBtn_Click1()
        {
            // If the input is bad or nothing is inputted while the "ENTER"
            // key is pressed or the "=" key is pressed, a dialog box will
            // appear letting the user know something is wrong and to try 
            // and fix the incorrect equation.
            try
            {
                // Array that stores the numbers as a string.
                string[] nums = textBox1.Text.Split('+', '-', 'x', '/');
                // Array that stores the numbers as doubles.
                //double[] numbers = new double[nums.Length];
                double[] numbers = Array.ConvertAll(textBox1.Text.Split('+', '-', 'x', '/'), double.Parse);
                //for (int i = 0; i < nums.Length; i++)
                //{
                //    numbers[i] = Int32.Parse(nums[i]);
                //}



                // Arraylist that stores the signs.
                var signs = new ArrayList();
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (textBox1.Text.Substring(i, 1) == "+" ||
                        textBox1.Text.Substring(i, 1) == "-" ||
                        textBox1.Text.Substring(i, 1) == "x" ||
                        textBox1.Text.Substring(i, 1) == "/")
                    {
                        signs.Add(textBox1.Text.Substring(i, 1));
                    }
                }

                // The length of nums is less than or equal to 1.
                if (nums.Length <= 1)
                {
                    textBox1.Text = nums[0];
                }

                // There are only two numbers to compute. 
                else if (nums.Length == 2)
                {
                    if (signs[0].ToString() == "+")
                    {
                        double result = numbers[0] + numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                    else if (signs[0].ToString() == "-")
                    {
                        double result = numbers[0] - numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                    else if (signs[0].ToString() == "x")
                    {
                        double result = numbers[0] * numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                    else if (signs[0].ToString() == "/")
                    {
                        double result = numbers[0] / numbers[1];
                        string str = result.ToString();
                        textBox1.Text = str;
                    }
                }

                // If the amount of numbers if greater than 0
                else if (nums.Length > 2)
                {
                    // There is addition and/or subtraction.
                    if ((signs.Contains("+") || signs.Contains("-")) &&
                        !signs.Contains("/") && !signs.Contains("x"))
                    {
                        int count = 0;
                        var tempList = new ArrayList();
                        for (int i = 0; i < signs.Count; i++)
                        {
                            if (count == 0)
                            {
                                if (signs[i].ToString() == "+")
                                {
                                    tempList.Add(numbers[i] + numbers[i + 1]);
                                    count++;
                                }
                                else if (signs[i].ToString() == "-")
                                {
                                    tempList.Add(numbers[i] - numbers[i + 1]);
                                    count++;
                                }
                            }
                            else
                            {
                                if (signs[i].ToString() == "+")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num + numbers[i + 1]);
                                }
                                else if (signs[i].ToString() == "-")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num - numbers[i + 1]);
                                }
                            }
                        }
                        textBox1.Text = tempList[0].ToString();
                    }

                    // There is mulitplication and/or division.
                    else if ((signs.Contains("/") || signs.Contains("x")) &&
                        !signs.Contains("+") && !signs.Contains("-"))
                    {
                        int count = 0;
                        var tempList = new ArrayList();
                        for (int i = 0; i < signs.Count; i++)
                        {
                            if (count == 0)
                            {
                                if (signs[i].ToString() == "x")
                                {
                                    tempList.Add(numbers[i] * numbers[i + 1]);
                                    count++;
                                }
                                else if (signs[i].ToString() == "/")
                                {
                                    tempList.Add(numbers[i] / numbers[i + 1]);
                                    count++;
                                }
                            }
                            else
                            {
                                if (signs[i].ToString() == "x")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num * numbers[i + 1]);
                                }
                                else if (signs[i].ToString() == "/")
                                {
                                    double num = Convert.ToDouble(tempList[0]);
                                    tempList[0] = (num / numbers[i + 1]);
                                }
                            }
                        }
                        textBox1.Text = tempList[0].ToString();
                    }

                    // There is mulitplication/division along with addition/subtraction.
                    else if ((signs.Contains("/") || signs.Contains("x")) &&
                        (signs.Contains("+") || signs.Contains("-")))
                    {
                        List<double> list1 = numbers.ToList();
                        var tempList = new ArrayList();

                        int i = 0;
                        while (signs.Contains("x") == true || signs.Contains("/") == true)
                        {
                            if (signs[i].ToString() == "x")
                            {
                                signs.RemoveAt(i);
                                list1[i] = (list1[i] * list1[i + 1]);
                                list1.RemoveAt(i + 1);
                            }
                            else if (signs[i].ToString() == "/")
                            {
                                signs.RemoveAt(i);
                                list1[i] = (list1[i] / list1[i + 1]);
                                list1.RemoveAt(i + 1);
                            }
                            else
                            {
                                i++;
                            }
                        }

                        // After the multiplication and division is computed the addition
                        // and/or subtraction is ran through the new equation.
                        if ((signs.Contains("+") || signs.Contains("-")) &&
                        !signs.Contains("/") && !signs.Contains("x"))
                        {
                            int count = 0;
                            var tempList2 = new ArrayList();
                            for (int x = 0; x < signs.Count; x++)
                            {
                                if (count == 0)
                                {
                                    if (signs[x].ToString() == "+")
                                    {
                                        tempList2.Add(list1[x] + list1[x + 1]);
                                        count++;
                                    }
                                    else if (signs[x].ToString() == "-")
                                    {
                                        tempList2.Add(list1[x] - list1[x + 1]);
                                        count++;
                                    }
                                }
                                else
                                {
                                    if (signs[x].ToString() == "+")
                                    {
                                        double num = Convert.ToDouble(tempList2[0]);
                                        tempList2[0] = (num + list1[x + 1]);
                                    }
                                    else if (signs[x].ToString() == "-")
                                    {
                                        double num = Convert.ToDouble(tempList2[0]);
                                        tempList2[0] = (num - list1[x + 1]);
                                    }
                                }
                            }
                            textBox1.Text = tempList2[0].ToString();
                        }
                    }
                }
            }
            // Dialog box for incorrect data or no data entered.
            catch (Exception ex)
            {
                string message = "You did not enter a correct operation.";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
            }
        }
        #endregion

        #region "ENTER" Key Determination Method
        // This method is used to determine if the "ENTER" key is pressed on a 
        // keyboard. It is an overwritten method and runs in the background
        // every time.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != (Keys.Enter))
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {
                equalBtn_Click1();
            }
            return true;
        }
        #endregion

        #region On-Screen Button Clicks
        // Button clicks for if the buttons are pressed on screen.
        private void oneBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "1";
        }

        private void twoBtn_Clicked(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "2";
        }

        private void threeBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "3";
        }

        private void fourBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "4";
        }

        private void fiveBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "5";
        }

        private void sixBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "6";
        }

        private void sevenBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "7";
        }

        private void eightBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "8";
        }

        private void nineBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "9";
        }

        private void zeroBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "0";
        }

        private void divideBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "/";
        }

        private void multiplyBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "x";
        }

        private void plusBtn_Clicked(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "+";
        }

        private void minusBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "-";
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string nameFull = textBox1.Text;
                string removedLastChar = nameFull.Remove(nameFull.Length - 1, 1);
                textBox1.Text = removedLastChar;
            }
            catch(Exception ex)
            {
                // Do nothing.
            }
        }

        private void decimalBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ".";
        }
        #endregion

        #region Keyboard Clicks
        // This method is used for the keyboard clicks except the enter key.
        private void FormBtn_KeyDown(object sender, KeyEventArgs e)
        {
            // CHAR NUM PAD 1
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                oneBtn_Click(sender, e);
            }
            // CHAR NUM PAD 2
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                twoBtn_Clicked(sender, e);
            }
            // CHAR NUM PAD 3
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                threeBtn_Click(sender, e);
            }
            // CHAR NUM PAD 4
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                fourBtn_Click(sender, e);
            }
            // CHAR NUM PAD 5
            else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                fiveBtn_Click(sender, e);
            }
            // CHAR NUM PAD 6
            else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                sixBtn_Click(sender, e);
            }
            // CHAR NUM PAD 7
            else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                sevenBtn_Click(sender, e);
            }
            // CHAR NUM PAD 8
            else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                eightBtn_Click(sender, e);
            }
            // CHAR NUM PAD 9
            else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                nineBtn_Click(sender, e);
            }
            // CHAR NUM PAD 0
            else if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                zeroBtn_Click(sender, e);
            }
            // SHIFT + "+"
            else if ((e.Shift) && e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                plusBtn_Clicked(sender, e);
            }
            // Subtraction or "-"
            else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            {
                minusBtn_Click(sender, e);
            }
            // "/"
            else if (e.KeyCode == Keys.OemQuestion || e.KeyCode == Keys.Divide)
            {
                divideBtn_Click(sender, e);
            }
            // Backspace for delete.
            else if (e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                deleteBtn_Click(sender, e);
            }
            // SHIFT + "x" for multiplication.
            else if (e.KeyCode == Keys.D8 && e.Shift || e.KeyCode == Keys.Multiply
                || e.KeyCode == Keys.X)
            {
                multiplyBtn_Click(sender, e);
            }
            
            // The period key is clicked for decimal.
            else if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
            {
                decimalBtn_Click(sender, e);
            }
        }
        #endregion

        
    }
}
