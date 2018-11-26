using System;
using System.Windows;

namespace LinearRegression
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Model model = new Model(10);
				string res = model.infer();





				MessageBox.Show(res);
				MessageBox.Show("A: " + model.A + " " + model.predictedA);
				MessageBox.Show("B: " + model.B + " " + model.predictedB);
				MessageBox.Show("Var: " + model.Var + " " + model.predictedVar);





			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}
