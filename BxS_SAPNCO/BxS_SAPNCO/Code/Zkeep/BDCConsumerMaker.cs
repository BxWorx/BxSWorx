using BxS_SAPNCO.API.Function;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	internal class BDCConsumerMaker	: IConsumerMaker<IBDCTranData>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCConsumerMaker(	PipelineOpEnv< IBDCTranData	,	DTO_SessionProgressInfo>	opEnv		,
																		BDC2RfcParser														parser	,
																		IBDCProfile															profile		)
					{
						this._OpEnv		= opEnv		;
						this._Parser	= parser	;
						this._Profile	= profile	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	PipelineOpEnv<	IBDCTranData	, DTO_SessionProgressInfo	>		_OpEnv		;
				private	readonly	BDC2RfcParser																_Parser		;
				private	readonly	IBDCProfile																	_Profile	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IConsumer<IBDCTranData>	 CreateConsumer()
					{
						IBDCTranProcessor	lo_TranProc	= new BDCTranProcessor( new RFCFunction()	,
																																	this._Profile				);

						var	lo_RfcData	=	new DTO_RFCData()	{	CTUOpts	= this._Profile.CTUStr	,
																									BDCData = this._Profile.BDCTbl	,
																									SPAData = this._Profile.SPATbl	,
																									MSGData = this._Profile.MSGTbl		};

						return	new BDCConsumer<IBDCTranData,DTO_SessionProgressInfo>(this._OpEnv, this._Parser, lo_TranProc, lo_RfcData );
					}

			#endregion

		}
}
