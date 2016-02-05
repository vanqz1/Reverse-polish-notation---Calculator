using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System;

namespace Calculator01
{
    public partial class MainWindow : Window
    {
        private bool existResult;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_To_Expression_Click(object sender, RoutedEventArgs e)
        {
            var keyword = (e.Source as Button).Content.ToString();
            Expression.Text += keyword;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            var watch = Stopwatch.StartNew();
            Calculator calc = new Calculator();
            List<string> record = new List<string>();
            Stack<string> operators = new Stack<string>();
            double result = 0;
            try
            {
                 result = calc.ReadExpression(Expression.Text.Trim() + " ", 0, record, operators).Result;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                Expression.Text = String.Empty;
            }
            Expression.Text = result.ToString();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Seconds.Text = elapsedMs + "ms";
         }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            Expression.Text = String.Empty;
        }
    }
}
