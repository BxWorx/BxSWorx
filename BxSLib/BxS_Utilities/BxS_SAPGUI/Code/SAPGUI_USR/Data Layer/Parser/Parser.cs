using System.Data;
using SAPGUI.COM.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DL
{
	internal partial class Parser
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
				internal void ParseDS2Rep(DataSet usrDS, Datacontainer repository)
					{
						this.MsgServersDT2DTO	(usrDS.Tables[this._Ref.MsgServerTableName]			, repository.MsgServers	);
						this.ServicesDT2DTO		(usrDS.Tables[this._Ref.ServiceTableName]				, repository.Services		);
						this.WorkspaceDT2DTO	(usrDS.Tables[this._Ref.WorkspaceTableName]			, repository.WorkSpaces	);
						this.WSNodeDT2DTO 		(usrDS.Tables[this._Ref.WorkspaceNodeTableName]	, repository.WorkSpaces	);
						this.WSItemDT2DTO 		(usrDS.Tables[this._Ref.WorkspaceItemTableName]	, repository.WorkSpaces	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseRep2DS(Datacontainer repository, DataSet usrDS)
					{
						this.MsgServersDTO2DT	(repository.MsgServers	,	usrDS.Tables[this._Ref.MsgServerTableName]	);
						this.ServicesDTO2DT		(repository.Services		, usrDS.Tables[this._Ref.ServiceTableName]		);
						this.WorkspaceDTO2DT	(repository.WorkSpaces	, usrDS.Tables[this._Ref.WorkspaceTableName]
																													,	usrDS.Tables[this._Ref.WorkspaceNodeTableName]
																													, usrDS.Tables[this._Ref.WorkspaceItemTableName]	);
					}

			#endregion

		}
}
