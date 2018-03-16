using System;
//.........................................................
using BxS_WorxNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCCall
{
	internal class BDCCallTranConsumer<T,P> : ConsumerBase<T,P>		where T:DTO_SessionTran
																																where	P:DTO_ProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCallTranConsumer(		ConsumerOpEnv<T,P>		OpEnv
																		, DTO_SessionHeader     header
																		, BDCCallTranProfile		profile
																		,	BDCCallTranProcessor	processor )	: base(OpEnv)
					{
						this._Processor		= processor ;
						this._CallProfile	= profile		;
						this._Header			= header		;
						//.............................................
						this._IsSetup		= false						;
						this._MyID			= Guid.NewGuid()	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				private BDCCallTranParser		_Parser { get { return	this._CallProfile.Parser; } }

			#endregion

			//===========================================================================================
			#region "Declarations"

				private Guid	_MyID			;
				private bool	_IsSetup	;
				//.................................................
				private	readonly	BDCCallTranProfile		_CallProfile	;
				private	readonly	BDCCallTranProcessor	_Processor		;
				private readonly	DTO_SessionHeader     _Header				;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool Execute( T workItem )
					{
						try
							{
								if (!this._IsSetup)
									{
										if (this._Processor.Ready())
											{
												this._Parser.Header( this._Header , this._Processor.Header );
												this._IsSetup	= !this._IsSetup;
											}
										if (!this._IsSetup)	return	false;
									}
								//.........................................
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
