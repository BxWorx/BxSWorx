using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace zBxS_SAPNCO_UT
{
	internal class UT_Pipeline
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal UT_Pipeline()
					{
						this.ProgInfo	= new Progress<DTO_ProgressInfo>();
						this.CTS			= new CancellationTokenSource();

						this.CNOpEnv	=	new ConsumerOpEnv< DTO_SessionTran , DTO_ProgressInfo >
																	( this.CreatePI, this.ProgInfo, this.CTS.Token, 10, 10 );

						this.PLOpEnv	= new PipelineOpEnv<DTO_RFCTran, DTO_ProgressInfo>
																	( this.CreatePI, this.ProgInfo, this.CTS.Token, 2, 20, 20);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

//				private	const int			lz_Clnt	= 700							;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal IProgress<DTO_ProgressInfo>	ProgInfo	{ get; }
				internal CancellationTokenSource			CTS				{	get; }

				internal PipelineOpEnv<DTO_RFCTran,DTO_ProgressInfo>	PLOpEnv		{ get; }

				internal ConsumerOpEnv<		DTO_SessionTran
																, DTO_ProgressInfo >	CNOpEnv		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//...................................................
				private DTO_ProgressInfo CreatePI()
					{
						return	new DTO_ProgressInfo();
					}

			#endregion

		}
}
