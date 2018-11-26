using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;

namespace LinearRegression
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private Model model;

		public MainWindow()
		{
			InitializeComponent();
			
		}
		
		private void render_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				model = new Model(
					N: int.Parse(textBoxN.Text),
					lowerA: double.Parse(textBoxlowerA.Text),
					upperA: double.Parse(textBoxUpperA.Text),
					lowerB: double.Parse(textBoxlowerB.Text),
					upperB: double.Parse(textBoxUpperB.Text),
					lowerVar: double.Parse(textBoxlowerVar.Text),
					upperVar: double.Parse(textBoxUpperVar.Text)
				);

				if (plot.SeriesCollection.Count == 3)
				{
					plot.SeriesCollection.RemoveAt(2);
				}

				if (plot.SeriesCollection.Count == 0)
				{
					plot.SeriesCollection.Add(new LineSeries
					{
						Title = "Sample",
						Values = new ChartValues<double>(model.sample),
					});

					plot.SeriesCollection.Add(new LineSeries
					{
						Title = "Real line",
						PointGeometrySize = 0,
						Values = new ChartValues<double>(model.realLine),
					});
				} else {
					plot.SeriesCollection[0].Values = new ChartValues<double>(model.sample);
					plot.SeriesCollection[1].Values = new ChartValues<double>(model.realLine);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private string getAccuracyInfo(double real, double infered, string varName)
		{
			double absErr = infered - real;
			double relErr = Math.Round(absErr * 100 / real, 2);
			string info = "";
			info += "Real " + varName + ": " + real + Environment.NewLine;
			info += "Predicted: " + infered + Environment.NewLine;
			info += "AbsErr: " + absErr + Environment.NewLine;
			info += "RelErr: " + relErr + " %" + Environment.NewLine + Environment.NewLine;

			return info;
		}

		private void infer_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string res = model.infer();

				if (plot.SeriesCollection.Count < 3)
				{
					plot.SeriesCollection.Add(new LineSeries
					{
						Title = "Infered Line",
						PointGeometrySize = 0,
						Values = new ChartValues<double>(model.inferedLine),
					});
				}
				else
				{
					plot.SeriesCollection[2].Values = new ChartValues<double>(model.inferedLine);
				}

				textBlockrRes.Text = res + Environment.NewLine + getAccuracyInfo(model.A, model.inferedA, "A");
				textBlockrRes.Text += getAccuracyInfo(model.B, model.inferedB, "B");
				textBlockrRes.Text += getAccuracyInfo(model.Var, model.inferedVar, "Var");
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
			
		}
	}
}
