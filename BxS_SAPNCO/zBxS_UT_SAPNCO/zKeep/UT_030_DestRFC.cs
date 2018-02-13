using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]

	public class UT_30_DestRFC
		{
			#region "Declarations"

				private	const			string					lz_RfcFnc	= "/ISDFPS/CALL_TRANSACTION";
				private readonly	UT_Destination	co_Dest;

			#endregion

			//...................................................
			public UT_30_DestRFC()
				{
					this.co_Dest	= new UT_Destination();
				}

				//-------------------------------------------------------------------------------------------
				[TestMethod]
				public void UT_DestRFC_Instantiate()
					{
						int	ln_Cnt	= 0;
						//...............................................
						ln_Cnt	++;

						Assert.IsNotNull( this.co_Dest								,	$"SAPNCO:DestRfc:Inst {ln_Cnt}: Check" );
						Assert.IsTrue		( this.co_Dest.RfcDest.Ping()	,	$"SAPNCO:DestRfc:Inst {ln_Cnt}: Ping"  );
					}

				//-------------------------------------------------------------------------------------------
				[TestMethod]
				public void UT_DestRFC_LoadRFCFncMetadata()
					{
						int	ln_Cnt	= 0;
						//...............................................
						ln_Cnt	++;

						var lo_RFCFnc1	= new UTFncProfile	(this.co_Dest.RfcDest, lz_RfcFnc);
						var lo_BDCFnc1	= new UTFncProfile	(this.co_Dest.RfcDest, lz_RfcFnc);

						Assert.IsNotNull( lo_RFCFnc1.Metadata	,	$"SAPNCO:DestRfc:Meta {ln_Cnt}: RFC" );
						Assert.IsNotNull( lo_BDCFnc1.Metadata	,	$"SAPNCO:DestRfc:Meta {ln_Cnt}: BDC" );
					}

				//-------------------------------------------------------------------------------------------
				[TestMethod]
				public void UT_DestRFC_Profile()
					{
						int	ln_Cnt	= 0;
						//...............................................
						ln_Cnt	++;

						IUTProfile	lo_Profile0	= null;
						IUTProfile	lo_Profile1	= null;
						//.............................................
						this.co_Dest.RfcDest.TryGetProfile(lz_RfcFnc, out object lo_ProfileObj);
						if (lo_ProfileObj == null)
							{
								lo_Profile0	= new UTFncProfile( this.co_Dest.RfcDest, lz_RfcFnc );
								this.co_Dest.RfcDest.RegisterProfile(lo_Profile0);
								this.co_Dest.RfcDest.TryGetProfile(lz_RfcFnc, out lo_ProfileObj);
							}

						lo_Profile0	= (IUTProfile)lo_ProfileObj;
						//.............................................
						lo_ProfileObj	= null;
						this.co_Dest.RfcDest.TryGetProfile(lz_RfcFnc, out lo_ProfileObj);
						if (lo_ProfileObj != null)
							{
								lo_Profile1	= (IUTProfile)lo_ProfileObj;
							}
						//.............................................
						Assert.IsNotNull( lo_Profile0								,	$"SAPNCO:DestRfc:Profile {ln_Cnt}: 1st"		);
						Assert.IsNotNull( lo_Profile1								,	$"SAPNCO:DestRfc:Profile {ln_Cnt}: 2nd"		);
						Assert.AreEqual	(	lo_Profile0	, lo_Profile1	,	$"SAPNCO:DestRfc:Profile {ln_Cnt}: Equal"	);
					}

			//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
			internal interface IUTProfile : IRfcFncProfile
				{
					#region "Properties:  Indicies"

						int ParIdx_TCode	{ get; set;	}
						int ParIdx_Skip1	{ get; set;	}
						int ParIdx_CTUOpt	{ get; set;	}
						int ParIdx_TabBDC	{ get; set;	}
						int	ParIdx_TabMsg	{ get; set;	}
						int ParIdx_TabSPA	{ get; set;	}

					#endregion
				}

			//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
			internal class UTFncProfile	:	RfcFncProfileBase	 ,
																			IUTProfile
				{
					#region "Constructors"

						//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
						internal UTFncProfile	(	DestinationRfc destination, string rfcFncName)
											: base			( destination, rfcFncName)
							{
							}

					#endregion

					//=========================================================================================
					#region "Properties:  Indicies"

						public	int ParIdx_TCode	{ get; set;	}
						public	int ParIdx_Skip1	{ get; set;	}
						public	int ParIdx_CTUOpt	{ get; set;	}
						public	int ParIdx_TabBDC	{ get; set;	}
						public	int	ParIdx_TabMsg	{ get; set;	}
						public	int ParIdx_TabSPA	{ get; set;	}

					#endregion

				}
		}
}
