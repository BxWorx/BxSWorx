using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM = SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.DL;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	public class UT_Destination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public UT_Destination( int useSAPGUI = 0 )
					{
						this.co_DestRepo	= new	DestinationRepository();
						this.co_Setup			= new DTOConfigSetupDestination	{	Client		= 700							,
																																User			= "DERRICKBINGH"	,
																																Password	= lz_PWrd						};

						if			(useSAPGUI == 0)	this.co_Setup.SetSAPGUIasNotUsed()	;
						else if	(useSAPGUI == 1)	this.co_Setup.SetSAPGUIasHidden	()	;
						else if	(useSAPGUI == 2)	this.co_Setup.SetSAPGUIasUsed		()	;

						SAPLogonINI.LoadRepository(this.co_DestRepo);

						IList<string> lt	= SAPLogonINI.GetSAPGUIConfigEntries();
						this.cc_ID				= lt.FirstOrDefault(s => s.Contains("PWD"));
						this.GuidID				= this.co_DestRepo.GetAddIDFor	(	this.cc_ID	);

						this.co_rfcConfig	=	this.co_DestRepo.GetParameters(	this.GuidID			);
						this.RfcDest			= new DestinationRfc(this.co_rfcConfig);
						this.RfcDest.LoadConfig(this.co_Setup);
						this.RfcDest.RfcDestination	= SDM.GetDestination(this.RfcDest.RfcConfig);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const string	lz_PWrd		= "M@@n1234";

				private readonly	string											cc_ID					;
				private readonly	DestinationRepository				co_DestRepo		;
				private readonly	SMC.RfcConfigParameters			co_rfcConfig	;
				private readonly	IDTOConfigSetupDestination	co_Setup			;

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid						GuidID	{	get;	}
				public DestinationRfc RfcDest	{	get; set;	}

			#endregion

		}
}
