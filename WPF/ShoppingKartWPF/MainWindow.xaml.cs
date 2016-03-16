using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoppingKartWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow ( )
		{
			InitializeComponent ( );
		}

		private void OnClickButton ( object sender, RoutedEventArgs e )
		{

		}

		private void DataGrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
		{

		}
	}

	public class DummyModel
	{
		public string ItemName { get; set; }
		public decimal ItemPrice { get; set; }
		public int ItemStock { get; set; }

	}
}
