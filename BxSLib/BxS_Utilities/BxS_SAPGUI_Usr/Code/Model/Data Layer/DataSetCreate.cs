using System;
using System.IO;
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
				internal DataSetCreate(string fullFileName, string dataSetName)
					{
						this._string	= typeof(string);
						this._sapgui	= new DataSet(dataSetName);
						//.............................................
						this.AddTable_Services();
						this.AddTable_MsgServer();
						this.AddTable_WorkSpace();
						//.............................................
						this.AddRelation_Service2MsgSrv();
						this.AddRelation_Service2Workspace();
						//.............................................
						using (var xmlSW = new StreamWriter(fullFileName))
							{
								this._sapgui.WriteXmlSchema(xmlSW);
								xmlSW.Close();
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2MsgSrv()
					{
						DataColumn	lo_ColParent	= this._sapgui.Tables["MsgServer"].Columns["UUID"];
						DataColumn	lo_ColChild		= this._sapgui.Tables["Services"].Columns["MsgServer_ID"];
						var					lo_Rel				= new DataRelation("Service2MsgServer", lo_ColParent, lo_ColChild);
						//.............................................
						this._sapgui.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2Workspace()
					{
						DataColumn	lo_ColParent	= this._sapgui.Tables["WorkSpace"].Columns["UUID"];
						DataColumn	lo_ColChild		= this._sapgui.Tables["Services"].Columns["Workspace_ID"];
						var					lo_Rel				= new DataRelation("Service2Workspace", lo_ColParent, lo_ColChild);
						//.............................................
						this._sapgui.Relations.Add(lo_Rel);
					}

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
						this.AddColumn_TypeString(MsgServer, "Host", 20);
						this.AddColumn_TypeString(MsgServer, "Port", 20);
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
						this.AddColumn_TypeString(Services, "MsgServer_ID", 32);
						this.AddColumn_TypeString(Services, "Workspace_ID", 32);
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
