using System;
using BxS_SAPNCO.API.SAPFunctions.BDC;
//.........................................................
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	internal class BDCConsumer<T> : ConsumerBase<T> where T : IBDCTranData
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCConsumer( OperatingEnvironment<T> OpEnv	,
														IBDCTranProcessor				tranProcessor	,
														DTO_RFCData							dtoRfcData			)	: base(OpEnv)
					{
						this._BDCTran	= tranProcessor	;
						this._RfcData	= dtoRfcData		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	OperatingEnvironment<T>		_OpEnv		;
				private readonly	IBDCTranProcessor					_BDCTran	;
				private readonly	DTO_RFCData								_RfcData	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override bool Execute(T workItem)
					{
						try
							{
								this._OpEnv.Parser.ParseFrom	(	workItem				, this._RfcData	);
								this._BDCTran.Process					(	this._RfcData										);
								this._OpEnv.Parser.ParseTo		(	this._RfcData	,	workItem				);

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
