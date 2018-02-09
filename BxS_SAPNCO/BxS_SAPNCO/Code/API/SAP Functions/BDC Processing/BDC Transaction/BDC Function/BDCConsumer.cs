using System;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	internal class BDCConsumer<T,P> : ConsumerBase<T,P>	where T:class
																											where	P:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCConsumer( OpEnv<T,P>					OpEnv					,
														BDC2RfcParser				parser				,
														IBDCTranProcessor		tranProcessor	,
														DTO_RFCData					dtoRfcData			)	: base(OpEnv)
					{
						this._BDCTran	= tranProcessor	;
						this._RfcData	= dtoRfcData		;
						this._Parser	= parser				;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IBDCTranProcessor		_BDCTran	;
				private readonly	DTO_RFCData					_RfcData	;
				private	readonly	BDC2RfcParser				_Parser		;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool Execute(T workItem)
					{
						try
							{
								this._Parser.ParseFrom	(	(IBDCTranData)workItem	, this._RfcData	);
								this._BDCTran.Process		(	this._RfcData														);
								this._Parser.ParseTo		(	this._RfcData	,	(IBDCTranData)workItem	);

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
