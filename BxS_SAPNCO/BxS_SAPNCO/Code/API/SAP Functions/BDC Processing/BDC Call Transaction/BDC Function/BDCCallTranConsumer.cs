using System;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranConsumer<T,P> : ConsumerBase<T,P>		where T:DTO_SessionTran
																																where	P:DTO_ProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCallTranConsumer(		ConsumerOpEnv<T,P>		OpEnv
																		,	BDCCallTranProcessor	processor
																		, BDCCallTranParser			parser		)	: base(OpEnv)
					{
						this._Processor		= processor ;
						this._Parser			= parser		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCallTranProcessor	_Processor	;
				private readonly	BDCCallTranParser			_Parser			;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Configure(DTO_SessionHeader DTO)
					{
						if (this._Processor.Ready())
							this._Parser.Header( DTO , this._Processor.Header );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool Execute( T workItem )
					{
						try
							{
								this._Processor.Reset();
								this._Parser.ParseTran( workItem , this._Processor.Transaction );
								this._Processor.Process();
								this._Parser.ParseResults( this._Processor.Transaction , workItem );
								return	true;
							}
						catch (Exception)
							{
								return	false;
							}
					}

			#endregion

		}
}
