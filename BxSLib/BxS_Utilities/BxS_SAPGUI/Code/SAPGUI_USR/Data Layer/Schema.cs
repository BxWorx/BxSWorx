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
				internal Schema(References references)
					{
						this._ref	= references;
						//.............................................
						this._str		= typeof(string);
						this._guid	= typeof(Guid);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	References	_ref;
				private readonly	Type				_str;
				private readonly	Type				_guid;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSet Create()
					{
						var	lo_DS	= new DataSet(this._ref.DataSetName);
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
						DataColumn	lo_ColParent	= dataSet.Tables[this._ref.MsgServerTableName].Columns[this._ref.UUID];
						DataColumn	lo_ColChild		= dataSet.Tables[this._ref.ServiceTableName].Columns["MsgServer_ID"];
						var					lo_Rel				= new DataRelation($"{this._ref.ServiceTableName}2{this._ref.MsgServerTableName}", lo_ColParent, lo_ColChild);
						//.............................................
						dataSet.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddRelation_Service2Workspace(DataSet dataSet)
					{
						DataColumn	lo_ColParent	= dataSet.Tables[this._ref.WorkspaceTableName].Columns[this._ref.UUID];
						DataColumn	lo_ColChild		= dataSet.Tables[this._ref.ServiceTableName].Columns["Workspace_ID"];
						var					lo_Rel				= new DataRelation($"{this._ref.ServiceTableName}2{this._ref.WorkspaceTableName}", lo_ColParent, lo_ColChild);
						//.............................................
						dataSet.Relations.Add(lo_Rel);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpace(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._ref.WorkspaceTableName);
						//.............................................
						this.AddColumn_TypeGUID(lo_Tbl, this._ref.UUID, true);
						this.AddColumn_Description	(lo_Tbl);
						//.............................................
						lo_Tbl.Columns.Add("Parent_UUID"	, this._str);
						lo_Tbl.Columns.Add("Hierarchy_ID"	, this._str);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[this._ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);	
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_MsgServer(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._ref.MsgServerTableName);
						//.............................................
						this.AddColumn_TypeGUID(lo_Tbl, this._ref.UUID, true);
						this.AddColumn_Name					(lo_Tbl);
						this.AddColumn_Description	(lo_Tbl);
						//.............................................
						this.AddColumn_TypeString(lo_Tbl, "Host", 20);
						this.AddColumn_TypeString(lo_Tbl, "Port", 20);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[this._ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_Services(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._ref.ServiceTableName);
						//.............................................
						this.AddColumn_TypeGUID(lo_Tbl, this._ref.UUID, true);
						this.AddColumn_Name					(lo_Tbl);
						this.AddColumn_Description	(lo_Tbl);
						//.............................................
						lo_Tbl.Columns.Add("Type"						, this._str);
						lo_Tbl.Columns.Add("Server"					, this._str);
						lo_Tbl.Columns.Add("SystemID"				, this._str);
						lo_Tbl.Columns.Add("SNC_Name"				, this._str);
						lo_Tbl.Columns.Add("SNC_Op"					, this._str);
						lo_Tbl.Columns.Add("SAPCodePage"		, this._str);
						lo_Tbl.Columns.Add("DownUpCodePage"	, this._str);
						//.............................................
						this.AddColumn_TypeGUID(lo_Tbl, "MsgServer_ID", false);
						this.AddColumn_TypeGUID(lo_Tbl, "Workspace_ID", false);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[this._ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Common private routines
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

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
				private void AddColumn_TypeGUID(	DataTable	dataTable	,
																					string		name			,
																					bool			isUnique = false	)
					{
						var Col		= new DataColumn()	{	ColumnName		= name			,
																						Unique				= isUnique	,
																						AutoIncrement	= false			,
																						DataType			= this._guid	};

						dataTable.Columns.Add(Col);
//																						MaxLength			= 38				,
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeString(	DataTable	dataTable	,
																						string		name			,
																						int				maxLength	,
																						bool			isKey = false	)
					{
						var Col		= new DataColumn()	{	ColumnName	= name			,
																						Unique			= isKey			,
																						MaxLength		= maxLength	,
																						DataType		= this._str		};

						dataTable.Columns.Add(Col);
					}

			#endregion

		}
}
