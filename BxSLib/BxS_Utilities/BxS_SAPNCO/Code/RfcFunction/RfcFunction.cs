using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.Function
{
	public class RFCFunction : IRFCFunction
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public RFCFunction(	string	RfcFncName)
					{
						this.Name	= RfcFncName;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string							Name						{ get;			}
				public	SMC.IRfcFunction		RfcFunction			{ get; set; }
				public	SMC.RfcDestination	RfcDestination	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke()
					{
						bool	lb_Ret	= true;
						//.............................................
						try
							{
								this.RfcFunction.Invoke(this.RfcDestination);
							}
						catch (System.Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
