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
			#region "Declarations"

				private						bool			_IsDirty;
				private readonly	DataSet		_SapGUI;

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UsrDataSet(string fullFileName, string schemaFileName)
					{
						try
							{
									this._SapGUI	= new DataSet();
									this._SapGUI.ReadXmlSchema(schemaFileName);
									this._SapGUI.ReadXml(fullFileName, XmlReadMode.IgnoreSchema);
							}
							catch (Exception)
								{
								}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool AddUpdateService(DTOService dto)
					{
						DataTable	lo_Tbl	= this._SapGUI.Tables["Services"];
						DataRow		lo_Row	= lo_Tbl.NewRow();

						lo_Row["UUID"]	= dto.UUID;

						lo_Tbl.Rows.Add(lo_Row);

						return	true;

					}

			#endregion

			//===========================================================================================
			#region "Events: Private"
			#endregion

			//===========================================================================================
			#region "Methods: Private"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void AddRelation_Service2MsgSrv()
				//	{
				//		DataColumn	lo_ColParent	= this._sapgui.Tables["MsgServer"].Columns["UUID"];
				//		DataColumn	lo_ColChild		= this._sapgui.Tables["Services"].Columns["MsgServer_ID"];
				//		var					lo_Rel				= new DataRelation("Service2MsgServer", lo_ColParent, lo_ColChild);
				//		//.............................................
				//		this._sapgui.Relations.Add(lo_Rel);
				//	}

			#endregion

		}
}
