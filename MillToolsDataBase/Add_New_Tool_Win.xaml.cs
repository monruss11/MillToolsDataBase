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
using System.Windows.Shapes;

namespace MillToolsDataBase
{
	/// <summary>
	/// Interaction logic for Add_New_Tool_Win.xaml
	/// </summary>
	public partial class Add_New_Tool_Win : Window
	{
		public WriteReadXML writeXML = new WriteReadXML();
		// public ToolsDataBase cls_ToolsDataBase = new 	ToolsDataBase();
		public Add_New_Tool_Win( )
		{
			InitializeComponent(); 
			Double width  = SystemParameters.FullPrimaryScreenWidth;
            Double height = SystemParameters.FullPrimaryScreenHeight;
			this.Top = (height - this.Height) / 2+70;
            this.Left = (width - this.Width) / 2+70;
			if (System.IO.File.Exists(writeXML.filename) == true)
			ToolsDataBase.toolData.ID = writeXML.readIDfromXML();
			else { MessageBox.Show("File not exist /n/r Create file"); ToolsDataBase.toolData.ID = 0; }
			txtType.Focus();
		}
		
		private void Close_ToolWindow_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		public  void Add_Tool_Click(object sender, RoutedEventArgs e )
		{
				if (txtType.Text == "" ||  txtLength.Text =="" || txtDiametr.Text =="" || txtCut_length.Text =="" || 
				txtCorner_radius.Text ==""||  txtQuantity_receiving.Text=="")  
				{	MessageBox.Show("Input empty"); txtType.Focus();
					 txtType.Text = txtLength.Text = txtDiametr.Text = txtCut_length.Text = txtCorner_radius.Text = txtQuantity_receiving.Text= "";
				}
			else // Diametr > corner radius *2
			{ int i = Int32.Parse(txtDiametr.Text); float  j = float.Parse(txtCorner_radius.Text);
				if (i / 2 >= j)
				{
					// if Date Choised from DatePicker
					string date_receiving;
					if (Calendar.SelectedDate != null)
						date_receiving = Calendar.SelectedDate.Value.Day.ToString() + "/" + Calendar.SelectedDate.Value.Month.ToString() + "/" + Calendar.SelectedDate.Value.Year.ToString();
					else date_receiving = "03/09/2000";
					ToolsDataBase.AddTool(txtType, txtDiametr, txtLength, txtCut_length, txtCorner_radius, txtQuantity_receiving , date_receiving/*txtQuantity_issuing*/);
					writeXML.writeToXML(ToolsDataBase.toolData);
					 txtType.Text = txtLength.Text = txtDiametr.Text = txtCut_length.Text = txtCorner_radius.Text = txtQuantity_receiving.Text= "";
				}
				else { MessageBox.Show("Corner radius big more then half diametr"); txtCorner_radius.Focus(); txtCorner_radius.Text = ""; }
			}
		}
		private void Grid_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			short value;
			if (!Int16.TryParse(e.Text, out value)) e.Handled = true;
		}
		private void TxtCorner_radius_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			short value;
			if (!Int16.TryParse(e.Text, out value) & e.Text!=",") e.Handled = true;
		}
		private void TxtType_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			short value;
			if (Int16.TryParse(e.Text, out value))  e.Handled = true;
		}
		private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space || e.Key == Key.RightShift || e.Key == Key.LeftShift) e.Handled = true;
		}

		private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			Calendar.Text = "ttt"; 
			//ToolsDataBase.toolData.date_receiving = Calendar.SelectedDate.Value.Day.ToString()+"/"+Calendar.SelectedDate.Value.Month.ToString()+"/"+Calendar.SelectedDate.Value.Year.ToString();
		}

		//private void TxtType_LostFocus(object sender, RoutedEventArgs e)
		//{
		//	if (txtType.Text == "")
		//	{
		//		e.Handled = true;
		//		txtType.Focus();
		//	}
		//	e.Handled = true;
		//	ToolsDataBase.CheckField(txtType);
		//}// lost focus
	} //partial class
} //namespace

