using Microsoft.ML.Probabilistic.Algorithms;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Math;
using Microsoft.ML.Probabilistic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
				double realA = Rand.UniformBetween(-2, 2);
				double realB = Rand.UniformBetween(-20, 20);


				double[] data = new double[100];
				for (int i = 0; i < data.Length; i++) data[i] = realA * i + realB + Rand.Normal(0, 1);
				
				Variable<double> a = Variable.GaussianFromMeanAndVariance(0, 100).Named("a");
				Variable<double> b = Variable.GaussianFromMeanAndVariance(0, 100).Named("b");
				Variable<double> precision = Variable.GammaFromShapeAndScale(1, 1).Named("precision");

				Range dataRange = new Range(data.Length);
				VariableArray<double> x = Variable.Array<double>(dataRange);
				for (int i = 0; i < data.Length; i++)
				{
					x[i] = a * i + b + Variable.GaussianFromMeanAndPrecision(0, precision);
				}

				x.ObservedValue = data;

				// Create an inference engine for VMP  
				InferenceEngine engine = new InferenceEngine(new ExpectationPropagation());
				MessageBox.Show("precision: " + engine.Infer(precision));
				MessageBox.Show("B: " + engine.Infer(b) + " real: " + realB);
				MessageBox.Show("A: " + engine.Infer(a) + " real: " + realA);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}
