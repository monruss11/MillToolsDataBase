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

namespace MillToolsDataBase
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	
	public partial class MainWindow : Window
	{	 WriteReadXML writereadXML = new WriteReadXML();
	     DataSet dataSet = new DataSet();
		//public ToolsDataBase cls_ToolsDataBase = new 	ToolsDataBase();
		public MainWindow()
		{
			InitializeComponent();  
			Double width  = SystemParameters.FullPrimaryScreenWidth;
            Double height = SystemParameters.FullPrimaryScreenHeight;
			this.Top = (height - this.Height) / 2;
            this.Left = (width - this.Width) / 2;
		}
		public void NewTool_Click(object sender, RoutedEventArgs e)
		{
			Add_New_Tool_Win newtoolwindow = new Add_New_Tool_Win();
			newtoolwindow.ShowDialog();
		}
		private void ShowDataSetToolsDataBase_Click(object sender, RoutedEventArgs e)
		{
			if (dataSet.Tables.Count==0 )
			{	
				if (System.IO.File.Exists(writereadXML.filename) == true) 
				{  	dataSet.ReadXml(writereadXML.filename); dataGrid_ToolsDataBase.ItemsSource = dataSet.Tables[0].DefaultView;  }
				else MessageBox.Show("File not exist \n\r Create file");
				writereadXML.readToListfromXML();
			}
			else
			{
				dataSet.Tables[0].Clear();  /// !!! Reset DataSet.Table elements
				dataSet.ReadXml(writereadXML.filename);
				dataGrid_ToolsDataBase.ItemsSource = dataSet.Tables[0].DefaultView; 
				writereadXML.readToListfromXML();
			}
		}
		private void RemoveTool_Click(object sender, RoutedEventArgs e)
		{
			int index=dataGrid_ToolsDataBase.SelectedIndex;
			var intTried = Convert.ChangeType(dataGrid_ToolsDataBase.SelectedItem, typeof(int)) as int?;
			//int iiii =Convert.ToInt32( dataGrid_ToolsDataBase.SelectedItem);
			index=(int)(dataGrid_ToolsDataBase.CurrentCell.Item);
			//dataSet.Tables[0].Rows[0].IndexOf() = "5";
			//int i=dataGrid_ToolsDataBase.CurrentColumn.GetCellContent;
			//dataSet.Tables[0]..Rows.Contains. .FindName("quantity_receiving");
			dataSet.AcceptChanges();
			dataSet.Tables[0].Rows.RemoveAt(index);
			dataSet.WriteXml(writereadXML.filename);
		}
	}
}