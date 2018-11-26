using System;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace Wpf.CartesianChart.PointShapeLine
{
	public partial class PointShapeLineExample : UserControl
	{
		public PointShapeLineExample()
		{
			InitializeComponent();

			SeriesCollection = new SeriesCollection
			{
				
			};

			//Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
			YFormatter = value => value.ToString("C");

			DataContext = this;
		}

		public SeriesCollection SeriesCollection { get; set; }
		public string[] Labels { get; set; }
		public Func<double, string> YFormatter { get; set; }

	}
}