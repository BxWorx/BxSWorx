using System.Security;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BxS_WorxNCO.Destination.API.Config;
using BxS_WorxNCO.Destination.API.Destination;

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
					IConfigSetupGlobal			x		= this.co_NCO.DestController.CreateGlobalConfig();
					IConfigSetupDestination y		= this.co_NCO.DestController.CreateDestinationConfig();
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
					IRfcDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					IList< ISAPSystemReference >	lt_Rep	= this.co_NCO.DestController.GetSAPSystems();
					Assert.AreEqual	(	1	, lt_Rep.Count	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_40_Connect()
				{
					IRfcDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					lo_Dest.Client		= "700";
					lo_Dest.User			= "DERRICKBINGH";
					lo_Dest.Password	= "M@@n4321";
					//...............................................
					//Assert.IsTrue(	lo_Dest.Procure()		, "" );
					Assert.IsTrue(	lo_Dest.IsConnected	, "" );
				}

			[TestMethod]
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void UT_100_Dest_42_ConnectSecure()
				{
					var lc_Pwd	= new SecureString();
					IRfcDestination lo_Dest =	this.GetSAPDest();
					//...............................................
					lo_Dest.Client		= "700";
					lo_Dest.User			= "DERRICKBINGH";
					//...............................................
					foreach (char lc_C in "M@@n4321")
						{
							lc_Pwd.AppendChar(lc_C);
						}
					lc_Pwd.MakeReadOnly();
					lo_Dest.SecurePassword	= lc_Pwd;
					//...............................................
					//Assert.IsTrue(	lo_Dest.Procure()		, "" );
					Assert.IsTrue(	lo_Dest.IsConnected	, "" );
					Assert.IsTrue(	lo_Dest.IsConnected	, "" );
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			private IRfcDestination GetSAPDest()
				{
					IRfcDestination lo_Dest = this.co_NCO.GetSAPDest();
					Assert.IsNotNull	( lo_Dest	, "" );
					return	lo_Dest;
				}
		}
}
