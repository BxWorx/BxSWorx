using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCOpFnc
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpFnc(	Func< Guid, DTO_SessionTran	>	createSessionTran			,
														Func< DTO_SessionHeader >			createSessionHeader		,
														Func< DTO_SessionOptions >		createSessionOptions	,
														Func< DTO_RFCHeader >					createRFCHeader				,
														Func< DTO_RFCTran	>						createRFCTran					,
														Func< DTO_ProgressInfo >			createProgressInfo			)
					{
						this.CreateSessionTran		= createSessionTran			;
						this.CreateSessionHeader	= createSessionHeader		;
						this.CreateSessionOptions	= createSessionOptions	;
						this.CreateRFCHeader			= createRFCHeader				;
						this.CreateRFCTran				= createRFCTran					;
						this.CreateProgressInfo		= createProgressInfo		;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Func< Guid, DTO_SessionTran >	CreateSessionTran			{ get; }
				internal Func< DTO_SessionHeader >			CreateSessionHeader		{ get; }
				internal Func< DTO_SessionOptions >			CreateSessionOptions	{ get; }
				internal Func< DTO_RFCHeader >					CreateRFCHeader				{ get; }
				internal Func< DTO_RFCTran >						CreateRFCTran					{ get; }
				internal Func< DTO_ProgressInfo	>				CreateProgressInfo		{ get; }

			#endregion

		}
}
