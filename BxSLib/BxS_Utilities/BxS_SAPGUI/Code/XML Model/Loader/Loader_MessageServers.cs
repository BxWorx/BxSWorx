using System.Collections.Generic;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.XML
{
	internal partial class Loader
		{
			#region "Methods: Private: XML: Message Server Section"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, DTOMsgServer> Load_MsgServers(XmlDocument xmlDoc)
					{
						Dictionary<string, DTOMsgServer>	lt_MsgSrvrs	= new	Dictionary<string, DTOMsgServer>();
						//.............................................
						foreach (XmlElement lo_MsgSvr in xmlDoc.GetElementsByTagName("Messageserver"))
							{
								DTOMsgServer lo_DTO = new DTOMsgServer
									{	UUID				= lo_MsgSvr.GetAttribute("uuid")				,
										Name				= lo_MsgSvr.GetAttribute("name")				,
										Host				= lo_MsgSvr.GetAttribute("host")				,
										Port				= lo_MsgSvr.GetAttribute("port")				,
										Description	= lo_MsgSvr.GetAttribute("description")		};

								lt_MsgSrvrs.Add(lo_DTO.UUID, lo_DTO);
							}
						//.............................................
						return	lt_MsgSrvrs;
					}

			#endregion

		}
}
