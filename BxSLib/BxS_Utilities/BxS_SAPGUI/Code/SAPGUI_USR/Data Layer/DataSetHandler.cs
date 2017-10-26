using System;
using System.IO;
using System.Data;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class DataSetHandler
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSetHandler(string path)
					{
						this.IsReady	= false;
						//.............................................
						if (Directory.Exists(path))
							{
								this._SchemaFullName	= Path.Combine(path,	this.SchemaName);
								this._ReposFullName		= Path.Combine(path,	this.RepositoryName);
								this.IsReady					= true;
							}
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly string	_SchemaFullName;
				private readonly string	_ReposFullName;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal bool		IsReady					{ get; }
				internal string SchemaName			{ get	{ return "SAPGUI_USR_Schema.xml"; } }
				internal string RepositoryName	{ get	{ return "SAPGUI_USR_Repos.xml"; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSet	GetDataSet()
					{
						var	lo_DS	= default(DataSet);
						//.............................................
						if (!this.IsReady)	return	lo_DS;
						//.............................................
						if (!File.Exists(this._SchemaFullName))
							{
								lo_DS	= this.CreateSchema();
							}
						//.............................................
						this.LoadDataset(lo_DS);
						//.............................................
						return	lo_DS;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadDataset(DataSet dataSet)
					{
						try
							{
								dataSet.ReadXmlSchema	(this._SchemaFullName);
								dataSet.ReadXml				(this._ReposFullName, XmlReadMode.IgnoreSchema);
							}
							catch (Exception)
								{
									bool x = false;
									x = !x;
									// TO-DO: log exception
								}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DataSet CreateSchema()
					{
						var					lo_Schema	= new Schema();
						DataSet			lo_DS			= lo_Schema.Create();
						FileStream	lo_FS			= null;

						using (var lo_fs	= new FileStream(this._SchemaFullName, FileMode.Create))
							{
								using (var SW = new StreamWriter(lo_fs, System.Text.Encoding.UTF8, 512, false ))
									{
										lo_DS.WriteXmlSchema(SW);
										SW.Close();
									}
							}


						try
							{
								lo_FS	= new FileStream(this._SchemaFullName, FileMode.Create);

								using (var SW = new StreamWriter(lo_FS, System.Text.Encoding.UTF8, 512, false ))
									{
										lo_DS.WriteXmlSchema(SW);
										SW.Close();
									}

							}
							catch (Exception)
								{
									bool x = false;
									x = !x;
									// TO-DO: log exception
								}
							finally
								{
									lo_FS?.Dispose();
								}
						//.............................................
						return	lo_DS;
					}

			#endregion

		}
}
