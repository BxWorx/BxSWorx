using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProfile(	string							fncName
																		,	SMC.RfcDestination	rfcDest	)
					{
						this.FncName	= fncName	;
						this.RfcDest	= rfcDest	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"


			#endregion

			//===========================================================================================
			#region "Properties:  Parameters Indicies"

				internal	string										FncName			{ get; }
				internal	SMC.RfcDestination				RfcDest			{ get; }
				internal	SMC.RfcFunctionMetadata		FncMetdata	{ get; }
				//.................................................
				internal	int	ParIdx_TCode	{ get; set;	}
				internal	int ParIdx_Skip1	{ get; set;	}
				internal	int ParIdx_CTUOpt	{ get; set;	}
				internal	int ParIdx_TabBDC	{ get; set;	}
				internal	int	ParIdx_TabMSG	{ get; set;	}
				internal	int ParIdx_TabSPA	{ get; set;	}

			#endregion

		}
}
