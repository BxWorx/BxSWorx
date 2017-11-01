using System;
using System.IO;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class UsrDataSet
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UsrDataSet(References references, DataSet schema, string path, bool autoLoad = true)
					{
						this._Ref			= references;
						this._UsrDS		= schema;
						//.............................................
						this.DSFullName	= Path.Combine(path,	this.DSFileName);
						//.............................................
						if (autoLoad)	this.LoadData();
						//.............................................
						this._DTSrv	= new Lazy<DSTable>(	() => new DSTable(this._Ref, this._UsrDS.Tables[this._Ref.ServiceTableName]		) );
						this._DTMsg	= new Lazy<DSTable>(	() => new DSTable(this._Ref, this._UsrDS.Tables[this._Ref.MsgServerTableName]	) );
						this._DTWrk	= new Lazy<DSTable>(	() => new DSTable(this._Ref, this._UsrDS.Tables[this._Ref.WorkspaceTableName]	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	References	_Ref;
				private readonly	DataSet			_UsrDS;
				//.................................................
				private readonly	Lazy<DSTable>	_DTSrv;
				private readonly	Lazy<DSTable>	_DTMsg;
				private readonly	Lazy<DSTable>	_DTWrk;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	DSFullName	{ get	;	}
				internal	string	DSFileName	{ get	{ return "SAPGUI_USR_DataSet.xml"; } }
				//.................................................
				internal	int	ServiceCount		{ get	{ return this._DTSrv.Value.Count; } }
				internal	int	MsgServerCount	{ get	{ return this._DTMsg.Value.Count; } }
				internal	int	WorkspaceCount	{ get	{ return this._DTWrk.Value.Count; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Load()
					{
						this.LoadData();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Save()
					{
						try
							{
								using (var SW = new StreamWriter(this.DSFullName))
									{
										this._UsrDS.WriteXml(SW);
										//SW.Close();
									}
							}
						catch (Exception)
							{
								int x = 1;
								x++;
							}
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow NewSrvRow(bool LoadDefaults = true)
					{
						return	this._DTSrv.Value.NewRow();
						//if (LoadDefaults)	lo_Row[this._Ref.UUID]	= Guid.NewGuid();
					//		{
					//			lo_Row[this._Ref.UUID]	= Guid.NewGuid();

					//			foreach (object lo_Obj in lo_Row.Table.Columns)
					//				{
					//					var lo_Col	= (DataColumn)lo_Obj;

					//					var x = lo_Col.DataType;
					//switch (x)
					//	{
					//		case System.String;
					//	default:
					//		break;
					//	}

					//}
					//		}
						//return	lo_Row;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow NewMsgRow()
					{
						return	this._DTMsg.Value.NewRow();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow NewWrkRow()
					{
						return	this._DTWrk.Value.NewRow();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void AddUpdateService(DataRow data)
					{	this._DTSrv.Value.AddUpdate(data); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow GetService(Guid keyVal)
					{	return	this._DTSrv.Value.GetRow(keyVal); }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool RemoveService(Guid keyVal)
					{	return	this._DTSrv.Value.Remove(keyVal); }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadData()
					{
						try
							{	this._UsrDS.ReadXml(this.DSFullName, XmlReadMode.IgnoreSchema); }
						catch
							(System.IO.FileNotFoundException)	{	/* do nothing as this will be a new repository */ }
					}

			#endregion

		}
}
