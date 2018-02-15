using System;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCConsumer<T,P> : ConsumerBase<T,P>		where T:DTO_RFCTran
																												where	P:DTO_ProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCConsumer(		ConsumerOpEnv<T,P>		OpEnv
														,	BDCCallTranProcessor	processor )	: base(OpEnv)
					{
						this._Processor		= processor;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCallTranProcessor	_Processor	;
				private readonly	DTO_RFCTran						_RFCTran		;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool Execute( T workItem )
					{
						try
							{
								this._Processor.Reset();
								//this._Processor.Transaction;
								this._Processor.Process();
								//this._Processor.Process( workItem	);
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
