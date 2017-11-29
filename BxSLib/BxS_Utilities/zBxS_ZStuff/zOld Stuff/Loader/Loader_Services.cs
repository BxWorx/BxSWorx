using System.Collections.Generic;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Methods: Private: XML: Services Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load_Services(bool onlySAPGUI)
					{
						foreach (XmlElement lo_Elem in this._XmlDoc.GetElementsByTagName("Service"))
							{
								string lc_Type = lo_Elem.GetAttribute("type");
								//................................................
								if (onlySAPGUI && !lc_Type.Equals("SAPGUI"))	continue;
								//................................................
								DTOMsgService lo_DTO = new DTOMsgService
									{	Type				= lc_Type															,
										UUID				= lo_Elem.GetAttribute("uuid")				,
										Name				= lo_Elem.GetAttribute("name")				,
										DCPG				= lo_Elem.GetAttribute("dcpg")				,
										MSID				= lo_Elem.GetAttribute("msid")				,
										SAPCPG			= lo_Elem.GetAttribute("sapcpg")			,
										Server			= lo_Elem.GetAttribute("server")			,
										SNCName			= lo_Elem.GetAttribute("sncname")			,
										SNCOp				= lo_Elem.GetAttribute("sncop")				,
										SystemID		= lo_Elem.GetAttribute("systemid")		,
										Mode				= lo_Elem.GetAttribute("mode")				,
										Description	= lo_Elem.GetAttribute("description")		};
								//................................................
								this._Repos.Services.Add(lo_DTO.UUID, lo_DTO);
							}
					}

			#endregion

		}
}
