using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Main;

using BxS_WorxIPX.Main;
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
			#region "Methods: Exposed"

				IList< string >								GetSAPINIList();
				IList< ISAPSystemReference >	GetSAPSystems();
				//.................................................
				void	LoadGlobalConfig( IConfigGlobal config );
				//.................................................
				IRfcDestination		GetDestination( Guid		ID );
				IRfcDestination		GetDestination( string	ID );
				//.................................................
				void	Reset();

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session"

				BDC_SessionManager	CreateBDCSessionManager( IRfcDestination	rfcDestination );

			#endregion

		}
}