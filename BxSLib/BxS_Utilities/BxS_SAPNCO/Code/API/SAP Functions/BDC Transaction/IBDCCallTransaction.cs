//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions
{
	public interface IBDCCallTransaction
		{
			#region "Properties"

				int			Count	{	get;	}
				BDCData	Data	{ get;	}

				string							Name						{ get; }
				SMC.IRfcFunction		RfcFunction			{ get; set; }
				SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				BDCEntry	CreateBDCEntry	();
				BDCEntry	CreateBDCEntry	(	string	programName	= null	,
																		int			dynpro			= 0			,
																		bool		begin				= false	,
																		string	field				= null	,
																		string	value				= null	,
																		bool		AutoAdd			= true		);

				BDCEntry	CreateBDCEntry	(	string	programName	= null	,
																		string	dynpro			= null	,
																		string	begin				= null	,
																		string	field				= null	,
																		string	value				= null	,
																		bool		AutoAdd			= true		);
				//.................................................
				bool	AddBDCEntry( BDCEntry	entry );
				//.................................................
				void	Reset();

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
