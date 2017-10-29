using System;
using System.IO;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class Schema
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Schema()
					{
						this._string	= typeof(string);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const			string	_UUID	= "UUID";
				//.................................................
				private readonly	Type		_string;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal string DataSetName	{ get	{ return "SAPGUI_USR_Repository"; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSet Create()
					{
						var	lo_DS	= new DataSet(this.DataSetName);
						//.............................................
						this.AddTable_Services(lo_DS);
						this.AddTable_MsgServer(lo_DS);
						this.AddTable_WorkSpace(lo_DS);
						//.............................................
						this.AddRelation_Service2MsgSrv(lo_DS);
						this.AddRelation_Service2Workspace(lo_DS);
						//.............................................
						return	lo_DS;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2MsgSrv(DataSet dataSet)
					{
						DataColumn	lo_ColParent	= dataSet.Tables["MsgServer"].Columns["UUID"];
						DataColumn	lo_ColChild		= dataSet.Tables["Services"].Columns["MsgServer_ID"];
						var					lo_Rel				= new DataRelation("Service2MsgServer", lo_ColParent, lo_ColChild);
						//.............................................
						dataSet.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2Workspace(DataSet dataSet)
					{
						DataColumn	lo_ColParent	= dataSet.Tables["WorkSpace"].Columns["UUID"];
						DataColumn	lo_ColChild		= dataSet.Tables["Services"].Columns["Workspace_ID"];
						var					lo_Rel				= new DataRelation("Service2Workspace", lo_ColParent, lo_ColChild);
						//.............................................
						dataSet.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpace(DataSet dataSet)
					{
						var WorkSpace	= new DataTable("WorkSpace");
						//.............................................
						this.AddColumn_UUID(WorkSpace);
						//.............................................
						WorkSpace.Columns.Add("Description"		, this._string);
						WorkSpace.Columns.Add("Parent_UUID"		, this._string);
						WorkSpace.Columns.Add("Hierarchy_ID"	, this._string);
						//.............................................
						dataSet.Tables.Add(WorkSpace);	
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_MsgServer(DataSet dataSet)
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
						dataSet.Tables.Add(MsgServer);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_Services(DataSet dataSet)
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
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]	= Services.Columns[_UUID];
						Services.PrimaryKey	= Keys;
						//.............................................
						dataSet.Tables.Add(Services);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Common private routines
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_UUID(DataTable	dataTable)
					{
						this.AddColumn_TypeString(dataTable, _UUID, 36, true);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Name(DataTable	dataTable)
					{
						this.AddColumn_TypeString(dataTable, "Name", 20);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Description(DataTable	dataTable)
					{
						this.AddColumn_TypeString(dataTable, "Description", 50);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeString(DataTable	dataTable	,
																					string		name			,
																					int				maxLength	,
																					bool			isKey = false)
					{
						var Col		= new DataColumn()
													{	ColumnName	= name			,
														Unique			= isKey			,
														MaxLength		= maxLength	,
														DataType		= this._string	};

						dataTable.Columns.Add(Col);
					}

			#endregion

		}
}
