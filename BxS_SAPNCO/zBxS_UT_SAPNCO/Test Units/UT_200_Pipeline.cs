using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.API;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_200_Pipeline
		{
			#region "Declarations"

				//private readonly	UT_Destination					co_Dest;
				//private readonly	NCOController						co_Cntlr;
				//private readonly	IBDCProfile							co_Profile;
				//private readonly	BDC2RfcParser						co_Parser;
				//private readonly	BDCProfileConfigurator	co_PrfCnfg;

				private IProgress<int>		co_Progress;
				private CancellationToken	co_CT;


			#endregion

			//...................................................
			public UT_200_Pipeline()
				{
					this.co_Progress	= new	Progress<int>			();
					this.co_CT				= new	CancellationToken	();


					//this.co_Dest		= new UT_Destination(2);
					//this.co_Cntlr		= new NCOController();
					//this.co_Profile	= this.co_Cntlr.GetAddBDCTranProcessorProfile(this.co_Dest.RfcDest);
					//this.co_Parser	= this.co_Cntlr.CreateBDC2RfcParser(this.co_Profile);
					//this.co_PrfCnfg	= this.co_Cntlr.CreateProfileConfigurator();
					//this.co_PrfCnfg.Configure(this.co_Profile);
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_Pipeline_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					var x = new Pipeline<IBDCTranData>(this.co_Progress, this.co_CT, 2);

					Assert.IsNotNull(	x	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public async Task UT_Pipeline_Start()
				{
								int	ln_Cnt	= 00;
					const int ln_Max	= 10;
					const int ln_Con	= 02;
					//...............................................
					ln_Cnt	++;

					var x = new Pipeline<IBDCTranData>(this.co_Progress, this.co_CT, ln_Con);

					for (int i = 0; i < ln_Max; i++)
						{
							IBDCTranData o = new BDCTranData();
							bool b	=	x.Post(o);
						}

					x.Complete();

					int y = await x.StartAsync().ConfigureAwait(false);

					while (!y.Equals(ln_Max))
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual( ln_Max, y ,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
				}

		}
}
