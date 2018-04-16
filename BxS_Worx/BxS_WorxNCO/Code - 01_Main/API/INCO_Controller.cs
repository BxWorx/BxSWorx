using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.API;

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
			#region "Methods: Exposed: Destination"

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
			#region "Methods: Exposed: BDC Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				IList< ISAPSession_Header >		SAPSessionList(		String		userId      = "*"
																											,	String		sessionName	= "*"
																											,	DateTime  dateFrom    =	default(DateTime)
																											,	DateTime	dateTo      = default(DateTime)	);

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				ISAPSession_Profile	SAPBDCSession(	string	sessionName
																					,	string	QID
																					, bool		onlyHeader	= false );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				BDC_Session_Manager	CreateBDCSessionManager(	IRfcDestination	rfcDestination
																										, bool						useAltBDCFunction );

			#endregion

		}
}