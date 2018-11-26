using Microsoft.ML.Probabilistic.Algorithms;
using Microsoft.ML.Probabilistic.Math;
using Microsoft.ML.Probabilistic.Models;
using System;
using System.Windows;

namespace LinearRegression
{
	class Model
	{
		/* real */
		public double A { get; }
		public double B { get; }
		public double Var { get; }
		/* predicted */
		private double a;
		public double predictedA { get { return a; } }
		private double b;
		public double predictedB { get { return a; } }
		private double var;
		public double predictedVar { get { return a; } }

		public double[] sample { get; }
		public double[] realLine { get; }
		public double[] inferedLine { get; }

		public Model(int N = 100, 
			double downA = -2, double upA = 2, 
			double downB = -20, double upB = 20, 
			double downVar = 1, double upVar = 10
		) {
			A = Rand.UniformBetween(downA, upA);
			B = Rand.UniformBetween(downB, upB);
			Var = Rand.UniformBetween(downVar, upVar);

			sample = new double[N];
			realLine = new double[N];
			inferedLine = new double[N];
			for (int i = 0; i < sample.Length; i++)
			{
				double value = A * i + B;
				realLine[i] = value;
				sample[i] = value + Rand.Normal(0, Var);
			}
		}

		private string trimEnd(string str)
		{
			while (!char.IsNumber(str[str.Length - 1])) str = str.Substring(0, str.Length - 1);
			return str;
		}

		private double parseBetween(string str, char c1, char c2)
		{
			int firstSign = str.IndexOf(c1) + 1;
			int lastSign = str.IndexOf(c2);
			string substr = str.Substring(firstSign, lastSign - firstSign);
			substr = trimEnd(substr);
			double res = double.Parse(substr);
			return res;
		}

		public string infer()
		{
			Variable<double> a = Variable.GaussianFromMeanAndVariance(0, 100).Named("a");
			Variable<double> b = Variable.GaussianFromMeanAndVariance(0, 100).Named("b");
			Variable<double> precision = Variable.GammaFromShapeAndScale(1, 1).Named("precision");

			Range dataRange = new Range(sample.Length);
			VariableArray<double> y = Variable.Array<double>(dataRange);
			for (int i = 0; i < sample.Length; i++)
			{
				y[i] = a * i + b + Variable.GaussianFromMeanAndPrecision(0, precision);
			}

			y.ObservedValue = sample;
			
			InferenceEngine engine = new InferenceEngine(new ExpectationPropagation());
			
			string precisionString = engine.Infer(precision).ToString();
			string aString = engine.Infer(a).ToString();
			string bString = engine.Infer(b).ToString();

			string ans = "Precision: " + precisionString + Environment.NewLine;
			ans += "A: " + aString + Environment.NewLine;
			ans += "B: " + bString;

			this.var = (double)1 / parseBetween(precisionString, '=', ']');
			this.a = parseBetween(aString, '(', ' ');
			this.b = parseBetween(bString, '(', ' ');

			for (int i = 0; i < sample.Length; i++)
			{
				double value = this.a * i + this.b;
				inferedLine[i] = value;
			}

			return ans;
		}
	}
}
