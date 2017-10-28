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
		internal class DataLayerController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DataLayerController(string path)
					{
						this._SchemaFullName	= Path.Combine(path,	this.SchemaName);
						this._ReposFullName		= Path.Combine(path,	this.RepositoryName);
						//.............................................
						this.UsrDataSet	= new UsrDataSet(this._SchemaFullName, this._ReposFullName);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly string		_SchemaFullName;
				private readonly string		_ReposFullName;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal UsrDataSet	UsrDataSet			{ get; }
				internal string			SchemaName			{ get	{ return "SAPGUI_USR_Schema.xml"; } }
				internal string			RepositoryName	{ get	{ return "SAPGUI_USR_Repos.xml"; } }

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
						DataTable	lo_Tbl	= this.UsrDataSet.Tables["Services"];
						return true;
						//return this.ParseTableRow(lo_Tbl, Mapping.Servic, dto);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdate(DTOMsgServer dto)
					{
						DataTable	lo_Tbl	= this.UsrDataSet.Tables["MsgServer"];
						return true;
						//return this.ParseTableRow(lo_Tbl, Mapping.ServicesMap, dto);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

			#endregion

		}
}
