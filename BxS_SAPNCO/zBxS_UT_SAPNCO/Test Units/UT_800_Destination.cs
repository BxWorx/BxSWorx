using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Common;
using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_800_Destination
		{
			#region "Declarations"

				private	readonly	UT_Destination		lo_UTDest	;
				private readonly	SAPFncConstants		lo_SapCon ;

			#endregion

			//...................................................
			public UT_800_Destination()
				{
					this.lo_SapCon	= new SAPFncConstants();
					this.lo_UTDest	= new UT_Destination(	2 , true );
				}

			//...................................................
			[TestMethod]
			public void UT_800_10_ProfileHandling()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var x = new UT_Profile( this.lo_SapCon.BDCCallTran );
					Assert.IsNotNull(	x						,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					this.lo_UTDest.DestRfc.RegisterProfile(x);
					bool z = this.lo_UTDest.DestRfc.TryGetProfile(x.FunctionName, out object yy);
					var y = (UT_Profile)yy;

					y.ParIdx_CTUOpt	= 99;

					Assert.IsTrue		(			z	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					Assert.AreSame	(	y	,	x	, $"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					Assert.AreEqual	(	y.ParIdx_CTUOpt	,	x.ParIdx_CTUOpt	, $"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

				//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
				private class UT_Profile : RfcFncProfileBase
					{
						#region "Constructors"

							//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
							internal UT_Profile( string functionName )	: base( functionName )
								{
								}

						#endregion

						//=====================================================================================
						#region "Properties:  Parameters Indicies"

							internal	int	ParIdx_TCode	{ get; set;	}
							internal	int ParIdx_Skip1	{ get; set;	}
							internal	int ParIdx_CTUOpt	{ get; set;	}
							internal	int ParIdx_TabBDC	{ get; set;	}
							internal	int	ParIdx_TabMSG	{ get; set;	}
							internal	int ParIdx_TabSPA	{ get; set;	}

						#endregion

					}
		}
}
