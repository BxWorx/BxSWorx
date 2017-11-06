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
				internal void ParseDS2Rep(UsrDataSet usrDS, Repository repository)
					{
						this.MsgServersDT2DTO	(usrDS.MsgServers			, repository.MsgServers	);
						this.ServicesDT2DTO		(usrDS.Services				, repository.Services		);
						this.WorkspaceDT2DTO	(usrDS.Workspaces			, repository.WorkSpaces	);
						this.WSNodeDT2DTO 		(usrDS.WorkspaceNodes	, repository.WorkSpaces	);
						this.WSItemDT2DTO 		(usrDS.WorkspaceItems	, repository.WorkSpaces	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ParseRep2DS(Repository repository, UsrDataSet usrDS)
					{
						this.MsgServersDTO2DT	(repository.MsgServers	,	usrDS.MsgServers			);
						this.ServicesDTO2DT		(repository.Services		, usrDS.Services				);
						this.WorkspaceDTO2DT	(repository.WorkSpaces	, usrDS.Workspaces
																													,	usrDS.WorkspaceItems
																													, usrDS.WorkspaceNodes	);
					}

			#endregion

		}
}
