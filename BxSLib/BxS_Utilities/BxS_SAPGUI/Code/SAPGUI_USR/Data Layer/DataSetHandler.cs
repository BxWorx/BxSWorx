using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
//.........................................................
using SAPGUI.API.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
		internal class DataSetHandler
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataSetHandler(string path)
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

				private readonly string		_SchemaFullName;
				private readonly string		_ReposFullName;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal DataSet	Repository			{ get; private set; }
				internal string		SchemaName			{ get	{ return "SAPGUI_USR_Schema.xml"; } }
				internal string		RepositoryName	{ get	{ return "SAPGUI_USR_Repos.xml"; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Save()
					{
						return true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdate(DTOService dto)
					{
						DataTable	lo_Tbl	= this.Repository.Tables["Services"];
						return this.ParseTableRow(lo_Tbl, DTOMappings.ServicesMap, dto);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdate(DTOMsgServer dto)
					{
						DataTable	lo_Tbl	= this.Repository.Tables["MsgServer"];
						return this.ParseTableRow(lo_Tbl, DTOMappings.ServicesMap, dto);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"


				private bool ParseTableRow<T>(DataTable dataTable, Dictionary<string, string> map,  T dto) where T : class, new()
					{
						DataRow	lo_Row	= dataTable.NewRow();

						foreach (var lo_Fld in map)
							{
								lo_Row[lo_Fld.Key]	= dto;
							}
						//lo_Row["UUID"]	= dto.UUID;

						dataTable.Rows.Add(lo_Row);
						return true;
				
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadData()
					{
						try
							{
								this.Repository.ReadXml(this._ReposFullName, XmlReadMode.IgnoreSchema);
							}
						catch (System.IO.FileNotFoundException)
							{	/* do nothing as this will be a new repository */ }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadSchema()
					{
						this.Repository	= new DataSet();

						try
							{
								this.Repository.ReadXmlSchema	(this._SchemaFullName);
							}
						catch (System.IO.FileNotFoundException)
							{
								this.Repository	= new Schema().Create();

								using (var SW = new StreamWriter(this._SchemaFullName))
									{
										this.Repository.WriteXmlSchema(SW);
										SW.Close();
									}
							}
					}

			#endregion

		}
}
