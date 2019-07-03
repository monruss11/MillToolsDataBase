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
using System.Data;
using Microsoft.Win32;

namespace MillToolsDataBase
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window
	{
		WriteReadXML writereadXML = new WriteReadXML();
		DataSet dataSet = new DataSet();
		//public ToolsDataBase cls_ToolsDataBase = new 	ToolsDataBase();
		internal int rowIndex, columnIndex;
		public MainWindow()
		{
			InitializeComponent();
			Double width = SystemParameters.FullPrimaryScreenWidth;
			Double height = SystemParameters.FullPrimaryScreenHeight;
			this.Top = (height - this.Height) / 2;
			this.Left = (width - this.Width) / 2;
		
		}
		public void ColumnsProperty()
		{
			dataGrid_ToolsDataBase.Columns[0].Header = "Tool Id"; dataGrid_ToolsDataBase.Columns[1].Header = "Tool Type"; dataGrid_ToolsDataBase.Columns[2].Header = "Diametr";
			dataGrid_ToolsDataBase.Columns[3].Header = "Length Tool"; dataGrid_ToolsDataBase.Columns[4].Header = "Cutting Lengh"; 
			dataGrid_ToolsDataBase.Columns[5].Header = "Corner Radius"; dataGrid_ToolsDataBase.Columns[6].Header = "Quantity"; dataGrid_ToolsDataBase.Columns[7].Header = "Date Received"; 
		    dataGrid_ToolsDataBase.Columns[0].IsReadOnly=true;  dataGrid_ToolsDataBase.Columns[7].IsReadOnly=true;
		}
		public void ReadDataFromFile()
		{
			dataSet.ReadXml(writereadXML.filename);
			dataGrid_ToolsDataBase.ItemsSource = dataSet.Tables[0].DefaultView;
			ColumnsProperty();
			writereadXML.readToListfromXML();
		}
		#region Button Events
		public void btnNewTool_Click(object sender, RoutedEventArgs e)
		{
			Add_New_Tool_Win newtoolwindow = new Add_New_Tool_Win();
			newtoolwindow.ShowDialog();
		}
		private void btnShowDataSetToolsDataBase_Click(object sender, RoutedEventArgs e)
		{
			if (dataSet.Tables.Count == 0)
			{
				if (System.IO.File.Exists(writereadXML.filename) == true)
				{ dataSet.ReadXml(writereadXML.filename); dataGrid_ToolsDataBase.ItemsSource = dataSet.Tables[0].DefaultView; }
				else { MessageBox.Show("File not exist \n\r Create file"); e.Handled = true; goto exit; }
				ColumnsProperty();
				//dataGrid_ToolsDataBase.Columns[0].Header = "Tool Id";
				writereadXML.readToListfromXML();
			}
			else
			{
				dataSet.Tables[0].Clear();  /// !!! Reset DataSet.Table elements
				ReadDataFromFile();
			}
        exit:;
		}
		private void btnRemoveTool_Click(object sender, RoutedEventArgs e)
		{
			int index = dataGrid_ToolsDataBase.SelectedIndex;
			dataSet.AcceptChanges();
			dataSet.Tables[0].Rows.RemoveAt(index);
			dataSet.WriteXml(writereadXML.filename);
		}
		//======== WORK !!!
		private void btnSaveChange_Click(object sender, RoutedEventArgs e)
		{
			dataSet.WriteXml(writereadXML.filename);
		}
		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
			ofd.Filter = "XML Files (*.xml)|*.xml";
			  Nullable<bool> result = false; ofd.ShowDialog(); result = ofd.ValidateNames;
			// Get the selected file name and display in a TextBox.
			// Load content of file in a TextBlock
	        try
	        {  if (result == true)  writereadXML.filename= ofd.FileName; ReadDataFromFile(); }
	        catch (Exception ex)
	        {  MessageBox.Show("File not exist \n\r Create file " + ex.Message); }
		}
		#endregion
#region DataGtid Events
		

		private void DataGrid_ToolsDataBase_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			 rowIndex = e.Row.GetIndex();
			 columnIndex = e.Column.DisplayIndex;
		}

		
		private void DataGrid_ToolsDataBase_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			//	dataGrid_ToolsDataBase.Columns.[columnIndex];
			short value;
			if (columnIndex == 1) { if (Int16.TryParse(e.Text, out value)) e.Handled = true; }
			else  if (!Int16.TryParse(e.Text, out value)) e.Handled = true; 
		}
		//private void DataGrid_ToolsDataBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	object item = dataGrid_ToolsDataBase.SelectedItem;
		//	int rowID = Convert.ToInt32((dataGrid_ToolsDataBase.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
		//	string strID = (dataGrid_ToolsDataBase.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;

		//	int selectedCellValue = Convert.ToInt32(((DataRowView)dataGrid_ToolsDataBase.SelectedItem).Row[6]);
		//	string ID = ((DataRowView)dataGrid_ToolsDataBase.SelectedItem).Row["type"].ToString();
		//}
#endregion DataGtid Events
	}//MainWindow
} // NameSpace