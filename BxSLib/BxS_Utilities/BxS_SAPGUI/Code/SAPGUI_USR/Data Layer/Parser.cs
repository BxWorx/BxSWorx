using System;
using System.Data;
using System.Collections.Generic;
//.........................................................
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
	internal class Parser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Parser(References references)
					{
						this._Ref	= references;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly References		_Ref;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Parse(UsrDataSet usrDS, Repository repository)
					{
						this.ParseMsgServers(usrDS.MsgServers, repository.MsgServers);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Parse(Repository repository, UsrDataSet usrDS)
					{
						this.ParseMsgServers(repository.MsgServers, usrDS.MsgServers);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseMsgServers(DataTable DTMsgSvr, Dictionary<Guid, DTOMsgServer> dto)
					{
						foreach (DataRow lo_Row in DTMsgSvr.Rows)
							{
								var lo_DTO = new DTOMsgServer
									{
										UUID				= (Guid)lo_Row[this._Ref.UUID],
										Name				= (string)lo_Row[this._Ref.Name],
										Description = (string)lo_Row[this._Ref.Description],
										Host				= (string)lo_Row[this._Ref.Host],
										Port				= (string)lo_Row[this._Ref.Port]
									};
								//.............................................
								dto.Add(lo_DTO.UUID,	lo_DTO);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseMsgServers(Dictionary<Guid, DTOMsgServer> dto, DataTable DTMsgSvr)
					{
						foreach (KeyValuePair<Guid, DTOMsgServer> lo_Entry in dto)
							{
								DataRow lo_Row	= DTMsgSvr.NewRow();
								//.............................................
								lo_Row[this._Ref.UUID]				= lo_Entry.Value.UUID;
								lo_Row[this._Ref.Name]				= lo_Entry.Value.Name;
								lo_Row[this._Ref.Description]	= lo_Entry.Value.Description;
								lo_Row[this._Ref.Host]				= lo_Entry.Value.Host;
								lo_Row[this._Ref.Port]				= lo_Entry.Value.Port;
								//.............................................
								DTMsgSvr.LoadDataRow(lo_Row.ItemArray, true);
							}
					}

			#endregion

		}
}
