using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	public class UT_000_NCO
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_000_NCO()
				{
					this.DestController	= new DestinationController();
				}

			//public const string	cz_Client	= "700"					;
			//public const string	cz_User		= "DERRICKBINGH";
			//public const string	cz_PWrd		= "M@@n4321"		;

			public const string	cz_Client	= "100"				;
			public const string	cz_User		= "DERRICKB"	;
			public const string	cz_PWrd		= "moon123"		;

			internal IDestinationController DestController { get; }

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal string GetSAPID()
				{
					IList< string > lt_Ini	=	this.DestController.GetSAPINIList();
					string					lc_ID		= lt_Ini.FirstOrDefault( s => s.Contains("05.01") );
					Assert.IsNotNull	( lc_ID	, "" );
					return	lc_ID;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal IRfcDestination GetSAPDest()
				{
					IRfcDestination lo_Dest = this.DestController.GetDestination( this.GetSAPID() );
					//...............................................
					Assert.IsNotNull	( lo_Dest	, "" );
					//...............................................
					return	lo_Dest;
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal IRfcDestination GetSAPDestLoggedOn( bool DoLogonCheck = false , bool showSAPGui = false )
				{
					IRfcDestination			lo_Dest		=	this.GetSAPDest();
					IConfigLogon				lo_Logon	= Destination_Factory.CreateLogonConfig();
					IConfigDestination	lo_Cnfg		= Destination_Factory.CreateDestinationConfig();
					//...............................................
					lo_Logon.Client			=	cz_Client			;
					lo_Logon.User				= cz_User				;
					lo_Logon.Password		= cz_PWrd				;
					//...............................................
					lo_Cnfg.DoLogonCheck	= DoLogonCheck	;
					lo_Cnfg.UseSAPGUI			= showSAPGui	? lo_Cnfg.SAPGUIUse	: lo_Cnfg.SAPGUIHidden	;
					//...............................................
					lo_Dest.LoadConfig( lo_Cnfg )		;
					lo_Dest.LoadConfig( lo_Logon )	;
					//...............................................
					return	lo_Dest	;
				}
		}
}
