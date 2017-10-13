using System.Collections.Generic;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Methods: Private: XML: Message Server Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Load_MsgServers()
					{
						foreach (XmlElement lo_MsgSvr in this._XmlDoc.GetElementsByTagName("Messageserver"))
							{
								DTOMsgServer lo_DTO = new DTOMsgServer
									{	UUID				= lo_MsgSvr.GetAttribute("uuid")				,
										Name				= lo_MsgSvr.GetAttribute("name")				,
										Host				= lo_MsgSvr.GetAttribute("host")				,
										Port				= lo_MsgSvr.GetAttribute("port")				,
										Description	= lo_MsgSvr.GetAttribute("description")		};

								this._Repos.MsgServers.Add(lo_DTO.UUID, lo_DTO);
							}
					}

			#endregion

		}
}
