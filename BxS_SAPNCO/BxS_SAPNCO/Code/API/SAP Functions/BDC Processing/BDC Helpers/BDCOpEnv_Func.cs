using System;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC.Session;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCOpEnv_Func
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv_Func(	Func< Guid, BDCSessionTran		>	createBDCSessionTran		,
																Func< DTO_RFCSessionHeader		>	createRFCSessionHeader	,
																Func< DTO_RFCSessionTran			>	createRFCSessionTran		,
																Func< DTO_SessionProgressInfo	>	createDTOProgressInfo			)
					{
						this.CreateSessionBDCTran				= createBDCSessionTran		;
						this.CreateSessionRFCHeader			= createRFCSessionHeader	;
						this.CreateSessionRFCTran				= createRFCSessionTran		;
						this.CreateSessionDTOProgInfo		= createDTOProgressInfo		;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal Func< Guid, BDCSessionTran		 >	CreateSessionBDCTran			{ get; }

				internal Func< DTO_RFCSessionHeader		 >	CreateSessionRFCHeader		{ get; }
				internal Func< DTO_RFCSessionTran			 >	CreateSessionRFCTran			{ get; }
				internal Func< DTO_SessionProgressInfo >	CreateSessionDTOProgInfo	{ get; }

			#endregion

		}
}
