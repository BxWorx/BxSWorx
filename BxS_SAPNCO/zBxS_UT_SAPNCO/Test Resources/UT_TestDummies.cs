using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	//***************************************************************************
	public class UT_ProgInfo
		{
			public	int	Processed		{ get; set; }
			public	int	Successful	{ get; set; }
			public	int	Faulty			{ get; set; }
		}

	//***************************************************************************
	public interface IUT_TranData
		{
			Guid	ID	{ get; }
		}

	//***************************************************************************
	public class UT_TranData	: IUT_TranData
		{
			public Guid	ID	{ get; }
		}

	//***************************************************************************
	internal class UT_Consumer<T,P> : ConsumerBase<T,P>	where T:class
																										where	P:class
		{
			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			internal UT_Consumer( OpEnv<T,P>	OpEnv ): base(OpEnv)
				{
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public override bool Execute(T workItem)
				{
					return	true;
				}
		}


	//***************************************************************************
	public class UT_ConsMaker	: IConsumerMaker<IUT_TranData>
		{
			public	IConsumer<IUT_TranData>	 CreateConsumer()
				{
					return	new UT_Consumer<IUT_TranData, UT_ProgInfo>()
				}
		}


	//***************************************************************************
	public class UT_Handler
		{
			//-----------------------------------------------------------------------
			internal IConsumerMaker<IUT_TranData>	CreateConsMaker()
				{
					return	new	UT_ConsMaker();
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
