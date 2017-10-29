using System;
using System.IO;
using System.Data;
//.........................................................
using SAPGUI.API.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class UsrDataSet
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UsrDataSet(string path)
					{
						this._SchemaFullName	= Path.Combine(path,	this.SchemaName);
						this._ReposFullName		= Path.Combine(path,	this.RepositoryName);
						//.............................................
						this.LoadSchema();
						this.LoadData();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	string		_SchemaFullName;
				private readonly	string		_ReposFullName;
				private	static		DataSet		_DataSet;

				private readonly Lazy<DataTable>	_DTSrv
									=	new Lazy<DataTable>( () =>	_DataSet.Tables["Services"]	,
																								System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	DataSet	DataSetUsr			{ get { return	_DataSet; } }
				//.................................................
				internal	string	SchemaName			{ get	{ return "SAPGUI_USR_Schema.xml"; } }
				internal	string	RepositoryName	{ get	{ return "SAPGUI_USR_Repos.xml"; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Save()
					{
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataRow NewSrvRow()
					{
						return	this._DTSrv.Value.NewRow();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdateService(Guid keyVal, DataRow data)
					{
						if (this._DTSrv.Value.Rows.Contains(keyVal))
							{ }
						else
							{
								this._DTSrv.Value.Rows.Add(data);
							}
						//.............................................
						return true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadData()
					{
						try
							{	_DataSet.ReadXml(this._ReposFullName, XmlReadMode.IgnoreSchema); }
						catch (System.IO.FileNotFoundException)
							{	/* do nothing as this will be a new repository */ }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSchema()
					{
						_DataSet	= new DataSet();
						//.............................................
						try
							{	_DataSet.ReadXmlSchema	(this._SchemaFullName); }
						catch (System.IO.FileNotFoundException)
							{
								_DataSet	= new Schema().Create();

								using (var SW = new StreamWriter(this._SchemaFullName))
									{
										_DataSet.WriteXmlSchema(SW);
										SW.Close();
									}
							}
					}

			#endregion

		}
}
