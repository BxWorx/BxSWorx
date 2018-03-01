using System.Linq;
using System.Security;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxDestination.Main;
using BxS_WorxDestination.API.Destination;
using BxS_WorxDestination.API.Main;
using BxS_WorxDestination.API.Config;

namespace BxS_zWorx_UT_Destination.Test_Units
{
	[TestClass]
	public class UT_100_Dest
		{
			private	readonly	IController	co_Cntlr;

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public UT_100_Dest()
				{
					this.co_Cntlr		= new Controller();
					Assert.IsNotNull	( this.co_Cntlr, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_10_Instantiate()
				{
					IConfigSetupGlobal			x		= this.co_Cntlr.CreateGlobalConfig();
					IConfigSetupDestination y		= this.co_Cntlr.CreateDestinationConfig();
					Assert.IsNotNull	( x , "" );
					Assert.IsNotNull	( y	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_20_SAPINI()
				{
					IList< string > lt_Ini		=	this.co_Cntlr.GetSAPINIList();
					Assert.AreNotEqual	(	0	, lt_Ini.Count	, "" );
					//...............................................
					IList< ISAPSystemReference >	lt_Rep	= this.co_Cntlr.GetSAPSystems();
					Assert.AreEqual	(	0	, lt_Rep.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_30_Dest()
				{
					IDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					IList< ISAPSystemReference >	lt_Rep	= this.co_Cntlr.GetSAPSystems();
					Assert.AreEqual	(	1	, lt_Rep.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_40_Connect()
				{
					IDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					lo_Dest.Client		= "700";
					lo_Dest.User			= "DERRICKBINGH";
					lo_Dest.Password	= "M@@n1234";
					//...............................................
					Assert.IsTrue(	lo_Dest.Procure()		, "" );
					Assert.IsTrue(	lo_Dest.IsConnected	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_42_ConnectSecure()
				{
					var lc_Pwd	= new SecureString();
					IDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					lo_Dest.Client		= "700";
					lo_Dest.User			= "DERRICKBINGH";
					//...............................................
					foreach (char lc_C in "M@@n1234")
						{
							lc_Pwd.AppendChar(lc_C);
						}
					lc_Pwd.MakeReadOnly();
					lo_Dest.SecurePassword	= lc_Pwd;
					//...............................................
					Assert.IsTrue(	lo_Dest.Procure()		, "" );
					Assert.IsTrue(	lo_Dest.IsConnected	, "" );
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private IDestination GetSAPDest()
				{
					IList< string > lt_Ini	=	this.co_Cntlr.GetSAPINIList();
					string					lc_ID		= lt_Ini.FirstOrDefault( s => s.Contains("PWD") );
					Assert.IsNotNull	( lc_ID	, "" );
					//...............................................
					IDestination lo_Dest = this.co_Cntlr.GetDestination( lc_ID );
					Assert.IsNotNull	( lo_Dest	, "" );
					return	lo_Dest;
				}
		}
}
