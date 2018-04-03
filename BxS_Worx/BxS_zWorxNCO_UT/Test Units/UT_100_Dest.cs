using System.Security;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxNCO.Destination.API;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_Dest
		{
			private readonly	UT_000_NCO	co_NCO;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_Dest()
				{
					this.co_NCO			= new	UT_000_NCO();
					Assert.IsNotNull	( this.co_NCO									, "" );
					Assert.IsNotNull	( this.co_NCO.DestController	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_10_Instantiate()
				{
					IConfigGlobal				x		= Destination_Factory.CreateGlobalConfig();
					IConfigDestination	y		= Destination_Factory.CreateDestinationConfig();
					Assert.IsNotNull	( x , "" );
					Assert.IsNotNull	( y	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_20_SAPINI()
				{
					IList< string > lt_Ini		=	this.co_NCO.DestController.GetSAPINIList();
					Assert.AreNotEqual	(	0	, lt_Ini.Count	, "" );
					//...............................................
					IList< ISAPSystemReference >	lt_Rep	= this.co_NCO.DestController.GetSAPSystems();
					Assert.AreEqual	(	0	, lt_Rep.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_30_Dest()
				{
					IRfcDestination lo_Dest =	this.co_NCO.GetSAPDest();
					//...............................................
					IList< ISAPSystemReference >	lt_Rep	= this.co_NCO.DestController.GetSAPSystems();
					Assert.AreEqual	(	1	, lt_Rep.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_40_Connect()
				{
					IRfcDestination lo_Dest =	this.co_NCO.GetSAPDest();
					//...............................................
					IConfigLogon	lo_Logon	= Destination_Factory.CreateLogonConfig();

					lo_Logon.Client		= UT_000_NCO.cz_Client	;
					lo_Logon.User			= UT_000_NCO.cz_User		;
					lo_Logon.Password	= UT_000_NCO.cz_PWrd		;

					lo_Dest.LoadConfig( lo_Logon )	;
					//...............................................
					Assert.IsTrue( lo_Dest.IsConnected	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_42_ConnectSecure()
				{
					var lc_Pwd	= new SecureString();
					IRfcDestination lo_Dest =	this.co_NCO.GetSAPDest();
					//...............................................
					IConfigLogon	lo_Logon	= Destination_Factory.CreateLogonConfig();

					lo_Logon.Client		= UT_000_NCO.cz_Client	;
					lo_Logon.User			= UT_000_NCO.cz_User		;
					//...............................................
					foreach (char lc_C in UT_000_NCO.cz_PWrd)
						{
							lc_Pwd.AppendChar(lc_C);
						}

					lc_Pwd.MakeReadOnly();
					lo_Logon.SecurePassword	= lc_Pwd;

					lo_Dest.LoadConfig( lo_Logon )	;
					//...............................................
					Assert.IsTrue(	lo_Dest.IsConnected	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_50_CoonectRepos()
				{
					IRfcDestination lo_Dest =	this.co_NCO.GetSAPDest();
					//...............................................
					IConfigLogon	lo_Logon	= Destination_Factory.CreateLogonConfig( true );

					lo_Logon.Client		= UT_000_NCO.cz_Client	;
					lo_Logon.User			= UT_000_NCO.cz_User		;
					lo_Logon.Password	= UT_000_NCO.cz_PWrd		;

					lo_Dest.LoadConfig( lo_Logon )	;
					//...............................................
					//Assert.IsTrue( lo_Dest.IsConnected	, "" );
				}
		}
}
