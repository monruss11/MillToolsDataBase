using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace MillToolsDataBase
{
	public class WriteReadXML
	{
		internal string filename;
		 XmlDocument  xDoc = new XmlDocument();
		ToolsDataBase.ToolData stc_toolData;
		 public  WriteReadXML() //!!!!
         { filename = "c:/tooldatabase.xml"; }
		
	   public void writeToXML(ToolsDataBase.ToolData stc_toolData)
		{
			int i = 0;
			if ( System.IO.File.Exists(filename)==false || System.IO.File.ReadAllText(filename)=="")
			{
			    XmlElement toolsDataBase = xDoc.CreateElement("ToolsDataBase"); 
				xDoc.CreateComment("uraaaaa");
				xDoc.AppendChild(toolsDataBase);
				XmlNode xRoot = xDoc.DocumentElement;  
				XmlElement toolData = xDoc.CreateElement("ToolData");  
				toolsDataBase.AppendChild(toolData); //!!!! need add first element to file !!!
				XmlElement toolID = xDoc.CreateElement("toolID"); toolID.InnerText = stc_toolData.ID.ToString(); toolData.AppendChild(toolID);
				XmlElement toolType = xDoc.CreateElement("type"); toolType.InnerText = stc_toolData.type; toolData.AppendChild(toolType);
				XmlElement toolDiametr = xDoc.CreateElement("diametr"); toolDiametr.InnerText = stc_toolData.diametr.ToString(); toolData.AppendChild(toolDiametr);
				XmlElement toolLength = xDoc.CreateElement("length"); toolLength.InnerText = stc_toolData.length.ToString(); toolData.AppendChild(toolLength);
				XmlElement toolCutLength = xDoc.CreateElement("cut_length"); toolCutLength.InnerText = stc_toolData.cut_length.ToString(); toolData.AppendChild(toolCutLength);
				XmlElement toolCornerRadius = xDoc.CreateElement("corner_radius"); toolCornerRadius.InnerText = stc_toolData.corner_radius.ToString();
				toolData.AppendChild(toolCornerRadius);
				XmlElement toolQuantityReceiving = xDoc.CreateElement("quantity_receiving"); toolQuantityReceiving.InnerText = stc_toolData.quantity_receiving.ToString();
				toolData.AppendChild(toolQuantityReceiving);
				XmlElement toolDateReceiving = xDoc.CreateElement("date_receiving"); toolDateReceiving.InnerText = stc_toolData.date_receiving.ToString();
				toolData.AppendChild(toolDateReceiving);
				xDoc.Save(filename);
			}
			else
			{ 
				xDoc.Load(filename);
			    XmlNode xRoot = xDoc.DocumentElement;
              	XmlElement toolData = xDoc.CreateElement("ToolData"); 
				xRoot.AppendChild(toolData);
				XmlElement toolID = xDoc.CreateElement("toolID");  toolID.InnerText = stc_toolData.ID.ToString(); toolData.AppendChild(toolID);
				XmlElement toolType = xDoc.CreateElement("type");  toolType.InnerText = stc_toolData.type; toolData.AppendChild(toolType);
				XmlElement toolDiametr = xDoc.CreateElement("diametr"); toolDiametr.InnerText =  stc_toolData.diametr.ToString(); toolData.AppendChild(toolDiametr);
				XmlElement toolLength = xDoc.CreateElement("length"); toolLength.InnerText =  stc_toolData.length.ToString(); toolData.AppendChild(toolLength);
				XmlElement toolCutLength = xDoc.CreateElement("cut_length"); toolCutLength.InnerText =  stc_toolData.cut_length.ToString(); toolData.AppendChild(toolCutLength);
				XmlElement toolCornerRadius = xDoc.CreateElement("corner_radius"); toolCornerRadius.InnerText = stc_toolData.corner_radius.ToString();
				toolData.AppendChild(toolCornerRadius);
				XmlElement toolQuantityReceiving = xDoc.CreateElement("quantity_receiving"); toolQuantityReceiving.InnerText =  stc_toolData.quantity_receiving.ToString(); 
				toolData.AppendChild(toolQuantityReceiving);
				XmlElement toolDateReceiving = xDoc.CreateElement("date_receiving"); toolDateReceiving.InnerText =  stc_toolData.date_receiving.ToString(); 
				toolData.AppendChild(toolDateReceiving);
				xDoc.Save(filename);
			} //else
		}
		//public void writeElementsAfterRootToXML()
		//{
		//	XmlElement toolData = xDoc.CreateElement("ToolData"); toolsDataBase.AppendChild(toolData);
		//	XmlElement toolID = xDoc.CreateElement("toolID"); toolID.InnerText = stc_toolData.ID.ToString(); toolData.AppendChild(toolID);
		//	XmlElement toolType = xDoc.CreateElement("type"); toolType.InnerText = stc_toolData.type; toolData.AppendChild(toolType);
		//	XmlElement toolDiametr = xDoc.CreateElement("diametr"); toolDiametr.InnerText = stc_toolData.diametr.ToString(); toolData.AppendChild(toolDiametr);
		//	XmlElement toolLength = xDoc.CreateElement("length"); toolLength.InnerText = stc_toolData.length.ToString(); toolData.AppendChild(toolLength);
		//	XmlElement toolCutLength = xDoc.CreateElement("cut_length"); toolCutLength.InnerText = stc_toolData.cut_length.ToString(); toolData.AppendChild(toolCutLength);
		//	XmlElement toolCornerRadius = xDoc.CreateElement("corner_radius"); toolCornerRadius.InnerText = stc_toolData.corner_radius.ToString();
		//	toolData.AppendChild(toolCornerRadius);
		//	XmlElement toolQuantityReceiving = xDoc.CreateElement("quantity_receiving"); toolQuantityReceiving.InnerText = stc_toolData.quantity_receiving.ToString();
		//	toolData.AppendChild(toolQuantityReceiving);
		//	XmlElement toolDateReceiving = xDoc.CreateElement("date_receiving"); toolDateReceiving.InnerText = stc_toolData.date_receiving.ToString();
		//	toolData.AppendChild(toolDateReceiving);
	   //}
	public int readIDfromXML()
	   {
			XmlTextReader xmlReader = new XmlTextReader(filename);
			string nodename; int i = 0;
			while (xmlReader.Read())
			{
				nodename = xmlReader.Name;
				if (nodename == "toolID") i=Convert.ToInt32(xmlReader.ReadString()); 
				//{ toolList.Last t[i].value_name = xmlReader.ReadString(); }
				//{ arrDat[i].value = (float)Convert.ChangeType(xmlReader.ReadString(), typeof(float)); i++; }
	        }
			return i;			
	   } // readIDfromXML
	public List< ToolsDataBase.ToolData > readToListfromXML()
	   {
			List<ToolsDataBase.ToolData> list_tooldata = new List<ToolsDataBase.ToolData> { };
			XmlDocument xDoc = new XmlDocument(); xDoc.Load(filename);
			XmlNodeList node_toolID = xDoc.GetElementsByTagName("toolID");
			XmlNodeList node_type = xDoc.GetElementsByTagName("type");
			XmlNodeList node_diametr = xDoc.GetElementsByTagName("diametr");
			XmlNodeList node_length = xDoc.GetElementsByTagName("length");
			XmlNodeList node_cut_length = xDoc.GetElementsByTagName("cut_length");
			XmlNodeList node_corner_radius = xDoc.GetElementsByTagName("corner_radius");
			XmlNodeList node_quantity_receiving = xDoc.GetElementsByTagName("quantity_receiving");
			XmlNodeList node_date_receiving = xDoc.GetElementsByTagName("date_receiving");
			for (int i = 0; i < node_toolID.Count; i++) 
			{
					 ToolsDataBase.toolData.ID =Int32.Parse( node_toolID[i].InnerText);  
					 ToolsDataBase.toolData.type = node_type[i].InnerText;
					 ToolsDataBase.toolData.diametr = Int32.Parse(node_diametr[i].InnerText);
					 ToolsDataBase.toolData.length = Int32.Parse(node_length[i].InnerText);  
					 ToolsDataBase.toolData.cut_length = Int32.Parse(node_cut_length[i].InnerText);
				     ToolsDataBase.toolData.corner_radius = float.Parse(node_corner_radius[i].InnerText);
				    ToolsDataBase.toolData.quantity_receiving = Int32.Parse(node_quantity_receiving[i].InnerText);
					ToolsDataBase.toolData.date_receiving = node_date_receiving[i].InnerText;
					list_tooldata.Add(ToolsDataBase.toolData); 
			}
			return  list_tooldata;			
	 } // readToListfromXML
	}
}
