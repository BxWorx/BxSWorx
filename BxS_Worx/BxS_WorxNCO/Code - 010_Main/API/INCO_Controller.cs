using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;

using BxS_WorxNCO.BDCSession.API;

using BxS_WorxNCO.SAPSession.API;

using BxS_WorxIPX.Main;
using BxS_WorxIPX.NCO;

using BxS_WorxUtil.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API
{
	public interface INCO_Controller
		{
			#region "Properties"

				IIPX_Controller		IPX_Cntlr		{ get; }
				IUTL_Controller		UTL_Cntlr		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();
				//.................................................
				IConfigGlobal	CreateGlobalConfig();
				//...
				void LoadGlobalConfig( IConfigGlobal config );
				//.................................................
				IBxSDestination		GetDestination( Guid		ID , bool useSAPINI = true );
				IBxSDestination		GetDestination( string	ID , bool useSAPINI = true );
				//.................................................
				void	Reset();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				IBDC_Request_Manager	CreateBDCRequestManager( ISAP_Logon	sapLogon );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				ISAP_Session_Manager	CreateSAPSessionManager( IBxSDestination	rfcDestination );

			#endregion

		}
}