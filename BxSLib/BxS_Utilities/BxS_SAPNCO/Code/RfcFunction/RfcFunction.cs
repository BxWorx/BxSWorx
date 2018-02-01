using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	public class RFCFunction : IRFCFunction
		{
			#region "Methods: Exposed"

				public	string						Name { get; set; }
				public	SMC.IRfcFunction	RfcFunction { get; set; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public NCOController CreateNCOController(	bool	LoadSAPGUIConfig	= true	,
																									bool	FirstReset				= false		)
					{
						return	new NCOController(LoadSAPGUIConfig, FirstReset);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
