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

				//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
				public UT_Destination( int	useSAPGUI = 0			,
															 bool autoStart	= true		)
					{
						if (autoStart)	this.UT_Destination_Startup(useSAPGUI);
					}

				//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
				public void UT_Destination_Startup(int useSAPGUI = 0)
					{
						this.co_DestRepo	= new	DestinationRepository();
						this.co_Setup			= this.UT_Destination_User(useSAPGUI);

						SAPLogonINI.LoadRepository(this.co_DestRepo);

						IList<string> lt	= SAPLogonINI.GetSAPGUIConfigEntries();
						this.cc_ID				= lt.FirstOrDefault(s => s.Contains("PWD"));
						this.GuidID				= this.co_DestRepo.GetAddIDFor	(	this.cc_ID	);

						this.co_rfcConfig	=	this.co_DestRepo.GetParameters(	this.GuidID	);
						this.RfcDest			= new DestinationRfc(this.co_rfcConfig);
						this.RfcDest.LoadConfig(this.co_Setup);
						this.RfcDest.RfcDestination	= SDM.GetDestination(this.RfcDest.RfcConfig);
					}

				//จจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจจ
				public IDTOConfigSetupDestination UT_Destination_User(int useSAPGUI = 0)
					{
						var	lo_Setup	= new DTOConfigSetupDestination	{	Client		= 700							,
																														User			= "DERRICKBINGH"	,
																														Password	= lz_PWrd						};

						if			(useSAPGUI == 0)	lo_Setup.SetSAPGUIasNotUsed	();
						else if	(useSAPGUI == 1)	lo_Setup.SetSAPGUIasHidden	();
						else if	(useSAPGUI == 2)	lo_Setup.SetSAPGUIasUsed		();

						return	lo_Setup;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const string	lz_PWrd		= "M@@n1234";

				private string											cc_ID					;
				private DestinationRepository				co_DestRepo		;
				private SMC.RfcConfigParameters			co_rfcConfig	;
				private IDTOConfigSetupDestination	co_Setup			;

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid						GuidID	{	get; set;	}
				public DestinationRfc RfcDest	{	get; set;	}

			#endregion

		}
}
