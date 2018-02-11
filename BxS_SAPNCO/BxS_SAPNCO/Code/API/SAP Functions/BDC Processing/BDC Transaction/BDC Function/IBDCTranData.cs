using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public interface IBDCTranData
		{
			//===========================================================================================
			#region "Properties"

				Guid	ID	{ get; }
				//.................................................
				bool	ProcessedStatus	{ get;	set;	}
				bool	SuccesStatus		{ get;	set;	}
				//.................................................
				int	BDCCount	{ get;	}
				int	SPACount	{ get;	}
				int	MSGCount	{ get;	}
				//.................................................
				string				SAPTCode		{ get;	set;	}
				string				Skip1st			{ get;	set;	}

				DTO_CTUParameters	CTUOptions	{ get;	set;	}
				//.................................................
				IList<DTO_BDCData>	BDCData	{ get; }
				IList<DTO_SPAEntry>	SPAData	{ get; }
				IList<DTO_SessionTranMsg>	MSGData	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: BDC Data"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void AddBDCData(	string	programName	= BDCConstants.lz_E	,
														int			dynpro			= 0									,
														bool		begin				= false							,
														string	field				= BDCConstants.lz_E	,
														string	value				= BDCConstants.lz_E		);

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void	AddBDCData(DTO_BDCData entry);

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: SPA Data"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void	AddSPAData(	string	memoryID		,
														string	memoryValue		);

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void	AddSPAData(DTO_SPAEntry entry);

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: MSG Data"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void	AddMSGData(	string	TCode	,
														string	DynNm	,
														string	DynNo	,
														string	MsgTp	,
														string	MsgLg	,
														string	MsgID	,
														string	MsgNr	,
														string	MsgV1	,
														string	MsgV2	,
														string	MsgV3	,
														string	MsgV4	,
														string	Envir	,
														string	FldNm		);

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void	AddMSGData(DTO_SessionTranMsg entry);

				#endregion

				//•••••••••••••••••••••••••••••••••••••••••••••••••
				#region "Methods: Exposed: General"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					void	Reset();

				#endregion

			#endregion

		}
}
