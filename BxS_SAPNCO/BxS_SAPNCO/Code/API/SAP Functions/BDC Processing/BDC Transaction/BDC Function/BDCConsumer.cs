using System;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCConsumer<T,P> : ConsumerBase<T,P>	where T:DTO_RFCTran
																											where	P:DTO_ProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCConsumer( PipelineOpEnv<T,P>	OpEnv					,
														IBDCTranProcessor		tranProcessor	,
														DTO_RFCTran					dtoRfcData			)	: base(OpEnv)
					{
						this._BDCTran	= tranProcessor	;
						this._RfcData	= dtoRfcData		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IBDCTranProcessor		_BDCTran	;
				private readonly	DTO_RFCTran					_RfcData	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool Execute(T workItem)
					{
						try
							{
								this._BDCTran.Process( workItem	);

								this.Successful.Enqueue(workItem);

								return	true;
							}
						catch (Exception)
							{
								this.Faulty.Enqueue(workItem);
								return	false;
							}
					}

			#endregion

		}
}
