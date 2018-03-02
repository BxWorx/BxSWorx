using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal abstract class RfcFncBase : IRfcFncBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncBase( string	SAPFunctionName	)
					{
						this.SAPFunctionName	= SAPFunctionName;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	bool	_FncCreated		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	SAPFunctionName		{ get; }

				public	IRfcFncProfile		Profile						{ get; set; }
				public	SMC.IRfcFunction	NCORfcFunction		{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke( SMC.RfcCustomDestination rfcDest )
					{
						bool	lb_Ret	= false;
						//.............................................
						try
							{
								this.NCORfcFunction.Invoke( rfcDest );
								lb_Ret	= true;
							}
						catch
							{	}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
