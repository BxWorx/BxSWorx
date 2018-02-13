using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProfile(	SMC.IRfcFunction		rfcFnc
																		,	SMC.RfcDestination	rfcDest	)
					{
						this.RfcFnc		= rfcFnc	;
						this.RfcDest	= rfcDest	;
					}

			#endregion

			//===========================================================================================
			#region "Properties:  Parameters Indicies"

				internal	SMC.IRfcFunction		RfcFnc	{ get; }
				internal	SMC.RfcDestination	RfcDest { get; }
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
