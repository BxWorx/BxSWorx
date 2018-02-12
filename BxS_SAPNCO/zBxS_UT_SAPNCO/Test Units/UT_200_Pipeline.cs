using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_200_Pipeline
		{
			#region "Declarations"

				private readonly UT_Handler		co_Hnd;

			#endregion

			//...................................................
			public UT_200_Pipeline()
				{
					this.co_Hnd	= new UT_Handler();
				}

			//...................................................
			[TestMethod]
			public void UT_Pipeline_010_OpEnv()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					PipelineOpEnv<IUT_TranData, UT_ProgInfo> x =	this.co_Hnd.CreateOpEnv();
					Assert.IsNotNull(	x	,	$"SAPNCO:OpEnv:Inst {ln_Cnt}: 1st" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_Pipeline_020_Instantiate()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					Pipeline<IUT_TranData, UT_ProgInfo> lo_Pipe =	this.co_Hnd.CreatePipeline();
					Assert.IsNotNull(	lo_Pipe	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public async Task UT_Pipeline_030_Start()
				{
								int	ln_Cnt	= 00;
								int	ln_Tot	= 00;
					const int ln_Max	= 2000;
					const int ln_Con	= 20;
					//...............................................
					ln_Cnt	++;

					Pipeline<IUT_TranData, UT_ProgInfo> lo_Pipe =	this.co_Hnd.CreatePipeline();

					for (int i = 0; i < ln_Max; i++)
						{
							IUT_TranData o = new UT_TranData();
							bool b	=	lo_Pipe.Post(o);
						}
					lo_Pipe.AddingCompleted();

					int y = await lo_Pipe.StartAsync(ln_Con).ConfigureAwait(false);

					while (!y.Equals(ln_Con))
						{
							Thread.Sleep(10);
						}

						foreach (Task<IConsumer<IUT_TranData>> lo_Task in lo_Pipe.TasksCompleted)
							{
								ln_Tot	+= lo_Task.Result.Successful.Count ;
							}

					Assert.AreEqual( ln_Con	, y												,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
					Assert.AreEqual( ln_Max	, ln_Tot									,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 2nd" );
					Assert.AreEqual( ln_Con	, lo_Pipe.CompletedCount	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 3rd" );
				}
		}
}
