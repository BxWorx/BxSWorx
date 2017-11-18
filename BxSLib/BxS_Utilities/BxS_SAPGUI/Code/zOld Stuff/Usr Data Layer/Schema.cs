using System;
using System.IO;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
		internal class Schema
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Schema(References references)
					{
						this._Ref	= references;
						//.............................................
						this._Str		= typeof(string);
						this._Guid	= typeof(Guid);
						this._Bool	= typeof(bool);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	References	_Ref;
				private readonly	Type				_Str;
				private readonly	Type				_Guid;
				private readonly	Type				_Bool;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSet Create()
					{
						var	lo_DS	= new DataSet(this._Ref.DataSetName);
						//.............................................
						this.AddTable_Services(lo_DS);
						this.AddTable_MsgServer(lo_DS);
						this.AddTable_WorkSpace(lo_DS);
						this.AddTable_WorkSpaceNode(lo_DS);
						this.AddTable_WorkSpaceItem(lo_DS);
						//.............................................
						return	lo_DS;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpace(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._Ref.WorkspaceTableName);
						//.............................................
						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.UUID, true);

						this.AddColumn_Description	(lo_Tbl);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.SeqNo	, 2);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[this._Ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpaceNode(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._Ref.WorkspaceNodeTableName);
						//.............................................
						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.ParentID	, false);
						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.UUID					, true);
						this.AddColumn_Description	(lo_Tbl);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[2];
						Keys[0]							= lo_Tbl.Columns[this._Ref.ParentID];
						Keys[1]							= lo_Tbl.Columns[this._Ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_WorkSpaceItem(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._Ref.WorkspaceItemTableName);
						//.............................................
						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.ParentID,	false, false);
						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.UUID, true);
						//.............................................
						this.AddColumn_TypeGUID	(lo_Tbl, this._Ref.ServiceID		,	false, false);
						this.AddColumn_TypeGUID	(lo_Tbl, this._Ref.WorkspaceID	,	false, false);
						this.AddColumn_TypeBool	(lo_Tbl, this._Ref.TypeWSItem);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[2];
						Keys[0]							= lo_Tbl.Columns[this._Ref.ParentID];
						Keys[1]							= lo_Tbl.Columns[this._Ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_MsgServer(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._Ref.MsgServerTableName);
						//.............................................
						this.AddColumn_TypeGUID(lo_Tbl, this._Ref.UUID, true);
						this.AddColumn_Name					(lo_Tbl);
						this.AddColumn_Description	(lo_Tbl);
						//.............................................
						this.AddColumn_TypeString(lo_Tbl, this._Ref.Host, 20);
						this.AddColumn_TypeString(lo_Tbl, this._Ref.Port, 20);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[this._Ref.UUID];
						lo_Tbl.PrimaryKey		= Keys;
						//.............................................
						dataSet.Tables.Add(lo_Tbl);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddTable_Services(DataSet dataSet)
					{
						var lo_Tbl	= new DataTable(this._Ref.ServiceTableName);
						//.............................................
						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.UUID, true, false);

						this.AddColumn_Name					(lo_Tbl);
						this.AddColumn_Description	(lo_Tbl);

						this.AddColumn_TypeString		(lo_Tbl, this._Ref.ConnType		);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.Server			);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.SystemID		);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.SNCName		);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.SNCOp			);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.CodePage		);
						this.AddColumn_TypeString		(lo_Tbl, this._Ref.DownUpCPge	);

						this.AddColumn_TypeGUID			(lo_Tbl, this._Ref.MsgSrvID, false, false);
						//.............................................
						DataColumn[]	Keys	= new DataColumn[1];
						Keys[0]							= lo_Tbl.Columns[this._Ref.UUID];
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
						this.AddColumn_TypeString(dataTable, this._Ref.Name, 20);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_Description(DataTable	dataTable)
					{
						this.AddColumn_TypeString(dataTable,this._Ref.Description, 50);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeGUID(	DataTable	dataTable						,
																					string		name								,
																					bool			isUnique		= false	,
																					bool			AllowNull		= true		)
					{
						var Col		= new DataColumn()	{	ColumnName		= name						,
																						Unique				= isUnique				,
																						AutoIncrement	= false						,
																						AllowDBNull		= AllowNull				,
																						DataType			= this._Guid				};

						dataTable.Columns.Add(Col);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeString(	DataTable	dataTable					,
																						string		name							,
																						int				maxLength	= -1		,
																						bool			isKey			= false	,
																						bool			AllowNull	= true		)
					{
						var Col		= new DataColumn()	{	ColumnName		= name			,
																						Unique				= isKey			,
																						AllowDBNull		= AllowNull	,
																						MaxLength			= maxLength	,
																						DataType			= this._Str		};

						Col.DefaultValue	= AllowNull	? null	: string.Empty;

						dataTable.Columns.Add(Col);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void AddColumn_TypeBool(	DataTable	dataTable	,
																					string		name				)
					{
						var Col		= new DataColumn()	{	ColumnName		= name				,
																						DataType			= this._Bool	,
																						DefaultValue	= false					};

						dataTable.Columns.Add(Col);
					}

			#endregion

		}
}
