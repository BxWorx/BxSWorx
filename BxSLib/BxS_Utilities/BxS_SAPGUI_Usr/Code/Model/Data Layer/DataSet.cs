using System;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class DataSetCreate
		{
			#region "Declarations"

				private readonly	DataSet	_sapgui;
				private readonly	Type		_string;

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSetCreate(string fullName)
					{
						this._string	= typeof(string);
						this._sapgui	= new DataSet("SAPGUI_USR");
						//.............................................
						this.AddTable_Services();
						this.AddTable_MsgServer();
						this.AddTable_WorkSpace();
						//.............................................
						var xmlSW	= new System.IO.StreamWriter(fullName);
						this._sapgui.WriteXmlSchema(xmlSW); //, XmlWriteMode.WriteSchema);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpace()
					{
						var WorkSpace	= new DataTable("WorkSpace");
						//.............................................
						this.AddColumn_UUID(WorkSpace);
						//.............................................
						WorkSpace.Columns.Add("Description"		, this._string);
						WorkSpace.Columns.Add("Parent_UUID"		, this._string);
						WorkSpace.Columns.Add("Hierarchy_ID"	, this._string);
						//.............................................
						this._sapgui.Tables.Add(WorkSpace);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_MsgServer()
					{
						var MsgServer	= new DataTable("MsgServer");
						//.............................................
						this.AddColumn_UUID					(MsgServer);
						this.AddColumn_Name					(MsgServer);
						this.AddColumn_Description	(MsgServer);
						//.............................................
						MsgServer.Columns.Add("Host"				, this._string);
						MsgServer.Columns.Add("Port"				, this._string);
						//.............................................
						this._sapgui.Tables.Add(MsgServer);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_Services()
					{
						var Services	= new DataTable("Services");
						//.............................................
						this.AddColumn_UUID					(Services);
						this.AddColumn_Name					(Services);
						this.AddColumn_Description	(Services);
						//.............................................
						Services.Columns.Add("Type"						, this._string);
						Services.Columns.Add("Server"					, this._string);
						Services.Columns.Add("SystemID"				, this._string);
						Services.Columns.Add("SNC_Name"				, this._string);
						Services.Columns.Add("SNC_Op"					, this._string);
						Services.Columns.Add("SAPCodePage"		, this._string);
						Services.Columns.Add("DownUpCodePage"	, this._string);
						//.............................................
						this._sapgui.Tables.Add(Services);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_UUID(DataTable	dataTable)
					{	this.AddColumn_TypeString(dataTable, "UUID", 32, true); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Name(DataTable	dataTable)
					{	this.AddColumn_TypeString(dataTable, "Name", 20); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Description(DataTable	dataTable)
					{	this.AddColumn_TypeString(dataTable, "Description", 50); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeString(DataTable	dataTable, string name, int maxLength, bool isKey = false)
					{
						var Col		= new DataColumn(name, this._string)
													{	Unique		= isKey			,
														MaxLength	= maxLength		};
						dataTable.Columns.Add(Col);
					}

			#endregion

		}
}
