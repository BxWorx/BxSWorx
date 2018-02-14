using Microsoft.VisualStudio.TestTools.UnitTesting;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.Common;
using BxS_SAPNCO.Pipeline;
using System;
using System.Threading;
//�������������������������������������������������������������������������������������������������
namespace zBxS_SAPNCO_UT
{
	[TestClass]
	public class UT_700_Pipeline
		{
			#region "Declarations"

				private	readonly	UT_Destination		co_UTDest	;
				private	readonly	UT_TestData				co_UTData	;
				private readonly	SAPFncConstants		co_SapCon ;

			#endregion

			//...................................................
			public UT_700_Pipeline()
				{
					this.co_SapCon	= new SAPFncConstants();
					this.co_UTData	= new UT_TestData();
					this.co_UTDest	= new UT_Destination(	2 , true );
				}

			//...................................................
			[TestMethod]
			public void UT_700_10_PLineOpEnv()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IProgress<DTO_ProgressInfo> lo_PH	= new Progress<DTO_ProgressInfo>();
					var CTS	= new CancellationTokenSource();

					var lo_PLOpEnv	= new PipelineOpEnv<DTO_RFCTran,DTO_ProgressInfo>(
							this.CreatePI, lo_PH, CTS.Token, 2, 20, 20 );

					Assert.IsNotNull(	lo_PLOpEnv	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			[TestMethod]
			public void UT_700_20_PLineConsumerOpEnv()
				{
					int	ln_Cnt	= 0;
					//...............................................
					ln_Cnt	++;

					IProgress<DTO_ProgressInfo> lo_PH	= new Progress<DTO_ProgressInfo>();
					var CTS	= new CancellationTokenSource();

					var lo_PLCOpEnv	= new ConsumerOpEnv<DTO_RFCTran,DTO_ProgressInfo>(
							this.CreatePI, lo_PH, CTS.Token, 10);

					Assert.IsNotNull(	lo_PLCOpEnv	,	$"SAPNCO:Session:Inst {ln_Cnt}: 1st" );
				}

			//...................................................
			private DTO_ProgressInfo CreatePI()
				{
					return	new DTO_ProgressInfo();
				}
		}
}
