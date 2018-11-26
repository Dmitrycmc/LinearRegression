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
				model = new Model(10);
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
