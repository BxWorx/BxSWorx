using System;
using System.Collections.Generic;
using System.Linq;
//.........................................................
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.CTU;
using BxS_SAPNCO.Common;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_910_BDC_CallTran
		{
			#region "Declarations"

				private	readonly	UT_Destination		lo_UTDest	;
				private readonly	SAPFncConstants		lo_SapCon ;

			#endregion

			//...................................................
			public UT_910_BDC_CallTran()
				{
					this.lo_SapCon	= new SAPFncConstants();
					this.lo_UTDest	= new UT_Destination(	2 , true );
				}

			//...................................................
			[TestMethod]
			public void UT_910_10_CallTranProfile()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var x = new BDCCallTranProfile( this.lo_SapCon.BDCCallTran );
					Assert.IsNotNull(	x						,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
					x.ReadyProfile();
					Assert.IsFalse	(	x.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					this.lo_UTDest.DestRfc.RegisterProfile(x);

					x.ReadyProfile();
					Assert.IsTrue	(	x.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );

					bool z = this.lo_UTDest.DestRfc.TryGetProfile(x.FunctionName, out BDCCallTranProfile y);
					Assert.IsTrue	(	y.IsReady	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_900_20_BDCOpEnv()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;
				}
		}
}
