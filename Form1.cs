using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ApplyColorPalette(); 
        }
        public string main_text;
        public string zamena1;
        public string zamena2;
    
        private void ApplyColorPalette()
        {

            Color primaryColor = ColorTranslator.FromHtml("#217074");
            Color secondaryColor = ColorTranslator.FromHtml("#37745B");
            Color accentColor = ColorTranslator.FromHtml("#8B9D77");
            Color backgroundColor = ColorTranslator.FromHtml("#E7EAEF");
            Color buttonColor = ColorTranslator.FromHtml("#EDC5AB");

            this.BackColor = backgroundColor; 

            button1.BackColor = buttonColor;
            button2.BackColor = buttonColor;
            button3.BackColor = buttonColor;
            button4.BackColor = buttonColor;
            button5.BackColor = buttonColor;
            button6.BackColor = buttonColor;
            button7.BackColor = buttonColor;


            button1.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
            button3.ForeColor = Color.Black;
            button4.ForeColor = Color.Black;
            button5.ForeColor = Color.Black;
            button6.ForeColor = Color.Black;
            button7.ForeColor = Color.Black;


            richTextBox1.BackColor = secondaryColor;
            richTextBox1.ForeColor = Color.White;

            label1.ForeColor = primaryColor;
            label2.ForeColor = primaryColor;


            textBox2.BackColor = accentColor;
            textBox3.BackColor = accentColor;
            textBox2.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;
        }


        public static string Encode(string input)
        {
            StringBuilder encodedText = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                int asciiCode = (int)c;
                int r = i % 3;
                asciiCode += r;

                encodedText.Append((char)asciiCode);
            }

            return encodedText.ToString();
        }


        public static string Decode(string input)
        {
            StringBuilder decodedText = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                int asciiCode = (int)c;
                int r = i % 3;
                asciiCode -= r;

                decodedText.Append((char)asciiCode);
            }

            return decodedText.ToString();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            string inputText = richTextBox1.Text;

            string encodedText = Encode(inputText);

            richTextBox1.Text = encodedText;
        }


        private void button7_Click(object sender, EventArgs e)
        {

            string inputText = richTextBox1.Text;

            string decodedText = Decode(inputText);

            richTextBox1.Text = decodedText;
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            main_text = richTextBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            char[] main_textArray = main_text.ToCharArray();
            for (int i = 0; i < main_textArray.Length - 2; i++)
            {
                if (main_textArray[i] == '.' && char.IsLower(main_textArray[i + 2]))
                {
                    main_textArray[i + 2] = char.ToUpper(main_textArray[i + 2]);
                }
                main_textArray[0] = char.ToUpper(main_textArray[0]);
            }

            main_text = new string(main_textArray);
            richTextBox1.Text = main_text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            zamena1 = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            zamena2 = textBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = zamena1;
            string target = zamena2;
            Regex regex = new Regex(pattern);
            string result = regex.Replace(main_text, target);
            richTextBox1.Text = result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = 0;

            for (var i = 0; i < main_text.Length; i++)
            {
                if (main_text[i] == '.' || main_text[i] == '!' || main_text[i] == '?')
                {
                    count++;
                }
            }
            label1.Visible = true;
            label1.Text = $"{count}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pattern = @"\b\w+\b";
            Regex regex = new Regex(pattern);

            int index = 0;

            foreach (Match match in regex.Matches(main_text))
            {
                string word = match.Value;

                if (IsPalindrome(word))
                {
                    richTextBox1.Select(match.Index, match.Length);
                    richTextBox1.SelectionColor = Color.Red;
                }
            }
        }

        private bool IsPalindrome(string word)
        {
            string cleanedWord = word.ToLower();
            char[] charArray = cleanedWord.ToCharArray();
            Array.Reverse(charArray);
            string reversedWord = new string(charArray);
            return cleanedWord == reversedWord;
        }

        private double EvaluateExpression(string expression)
        {
            try
            {
                DataTable table = new DataTable();
                return Convert.ToDouble(table.Compute(expression, string.Empty));
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректное арифметическое выражение.");
                return 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string expression = richTextBox1.Text;
            double result = EvaluateExpression(expression);
            label2.Visible = true;
            label2.Text = $"{result}";
        }
    }
}
