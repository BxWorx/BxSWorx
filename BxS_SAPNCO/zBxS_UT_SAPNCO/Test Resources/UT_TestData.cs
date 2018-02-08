using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	//***************************************************************************
	public class UT_TestData
		{
			//-----------------------------------------------------------------------
			internal Pipeline<IUT_TranData, UT_ProgInfo>	CreatePipeline()
				{
					OpEnv<IUT_TranData, UT_ProgInfo> lo_Openv	= this.CreateOpEnv();
					return	new Pipeline<IUT_TranData, UT_ProgInfo>(	lo_Openv, this.CreateConsMaker(lo_Openv) );
				}

			//-----------------------------------------------------------------------
			internal IConsumerMaker<IUT_TranData>	CreateConsMaker(OpEnv<IUT_TranData,UT_ProgInfo> opEnv)
				{
					return	new	UT_ConsMaker(opEnv);
				}

			//-----------------------------------------------------------------------
			internal OpEnv<IUT_TranData,UT_ProgInfo>	CreateOpEnv()
				{
					IProgress<UT_ProgInfo>	lo_PH	= new Progress<UT_ProgInfo>();
					var											lo_PI	= new UT_ProgInfo();
					var											lo_CT	= new CancellationToken();

					return	new OpEnv<IUT_TranData, UT_ProgInfo>(lo_PH, lo_PI, lo_CT);
				}
		}
}
