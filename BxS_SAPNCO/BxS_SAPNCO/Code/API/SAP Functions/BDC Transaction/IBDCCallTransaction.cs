//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public interface IBDCCallTransaction
		{
			#region "Documentation"

				//	FUNCTION /isdfps/call_transaction.
				//	*"----------------------------------------------------------------------
				//	*"  IMPORTING
				//	*"     VALUE(IF_TCODE)							TYPE	TCODE
				//	*"     VALUE(IF_SKIP_FIRST_SCREEN)	TYPE	FLAG DEFAULT SPACE
				//	*"     VALUE(IT_BDCDATA)						TYPE	BDCDATA_TAB OPTIONAL
				//	*"     VALUE(IS_OPTIONS)						TYPE	CTU_PARAMS OPTIONAL
				//	*"  EXPORTING
				//	*"     VALUE(ET_MSG)								TYPE	ETTCD_MSG_TABTYPE
				//	*"  TABLES
				//	*"      CT_SETGET_PARAMETER					STRUCTURE	RFC_SPAGPA OPTIONAL
				//	*"  EXCEPTIONS
				//	*"      IMPORT_PARA_ERROR
				//	*"      TCODE_ERROR
				//	*"      AUTH_ERROR
				//	*"      TRANS_ERROR

			#endregion

			//===========================================================================================
			#region "Properties"

				string	RFCFunctionName	{ get; }
				int			BDCDataCount		{	get; }
				int			SPADataCount		{	get; }
				//.................................................
				DTO_CTUParams	DTO_CTUParm	{ get; }
				DTO_BDCData		DTO_BDCData	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				bool Invoke();
				//.................................................
				DTO_SPAEntry	CreateSPAEntry(	string	MemoryID		,
																			string	MemoryValue	,
																			bool		autoAdd			= true );

				void	AddSPAEntry( DTO_SPAEntry	entry );
				//.................................................
				DTO_BDCEntry	CreateBDCEntry(	string	programName	= BDCConstants.lz_E	,
																			int			dynpro			= 0									,
																			bool		begin				= false							,
																			string	field				= BDCConstants.lz_E	,
																			string	value				= BDCConstants.lz_E	,
																			bool		autoAdd			= true								);

				void	AddBDCEntry( DTO_BDCEntry	entry );
				//.................................................
				void	Reset();

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
