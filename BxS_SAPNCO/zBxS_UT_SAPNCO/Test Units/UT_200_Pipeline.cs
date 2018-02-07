using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.Helpers;
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

					var x = new Pipeline<IBDCTranData>(this.co_Progress, this.co_CT, null, 2);

					Assert.IsNotNull(	x	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public async Task UT_Pipeline_Start()
				{
								int	ln_Cnt	= 00;
					const int ln_Max	= 2000;
					const int ln_Con	= 10;
					//...............................................
					ln_Cnt	++;

					var lo_Pipe = new Pipeline<IBDCTranData>(this.co_Progress, this.co_CT, null, ln_Con);

					for (int i = 0; i < ln_Max; i++)
						{
							IBDCTranData o = new BDCTranData();
							bool b	=	lo_Pipe.Post(o);
						}

					lo_Pipe.AddingCompleted();

					int y = await lo_Pipe.StartAsync().ConfigureAwait(false);

					while (!y.Equals(ln_Con))
						{
							Thread.Sleep(10);
						}

					Assert.AreEqual( ln_Con	, y				,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 1st" );
					Assert.AreEqual( ln_Max	, lo_Pipe.Count	,	$"SAPNCO:Pipeline:Inst {ln_Cnt}: 2nd" );
				}

			//-------------------------------------------------------------------------------------------
			[TestMethod]
			public void UT_Pipeline_Consumer()
				{
								int	ln_Cnt	= 0;
								int ln_Tot	= 0;
					const	int	ln_Con	= 122;
					//...............................................
					ln_Cnt	++;

					var BC = new BlockingCollection<IBDCTranData>();

					for (int i = 0; i < ln_Con; i++)
						{
							BC.Add(new BDCTranData());
						}
					BC.CompleteAdding();

					Task[] backgroundTasks = new []
					{
						Task<int>.Run(	() => {	var X = new MyConsumer<IBDCTranData>(BC, this.co_Progress, this.co_CT)	;
																		X.Start();
																		return	X.TotalProcessed;																													}	)	,

						Task<int>.Run(	() => {	var X = new MyConsumer<IBDCTranData>(BC, this.co_Progress, this.co_CT)	;
																		X.Start();
																		return	X.TotalProcessed;																													}	)	,

						Task<int>.Run(	() => {	var X = new MyConsumer<IBDCTranData>(BC, this.co_Progress, this.co_CT)	;
																		X.Start();
																		return	X.TotalProcessed;																													}	)
					};

					Task.WaitAll(backgroundTasks);

					foreach (Task<int> item in backgroundTasks)
						{
							ln_Tot += item.Result;
						}

					Assert.AreEqual	(ln_Con	, ln_Tot		,	$"SAPNCO:Pipeline:Consumer {ln_Cnt}: Tot" );
					Assert.AreEqual	(0			, BC.Count	,	$"SAPNCO:Pipeline:Consumer {ln_Cnt}: BC"	);
				}

			//-------------------------------------------------------------------------------------------
			private class MyConsumer<T> : ConsumerBase<T> where T : class
				{
					public MyConsumer(	BlockingCollection<T>	queue			,
															IProgress<int>				progress	,
															CancellationToken			CT				,
															int										interval	= 10	)	: base(queue, progress, CT, interval)
						{ }

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public override bool Execute(T workItem)
						{
							Thread.Sleep(10);
							return	true;
						}

				}
		}
}
