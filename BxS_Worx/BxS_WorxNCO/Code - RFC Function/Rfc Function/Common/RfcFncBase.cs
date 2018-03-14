using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal abstract class RfcFncBase : IRfcFncBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncBase( IRfcFncProfile profile	)
					{
						this.Profile	= profile;
						//.............................................
						this.MyID	= Guid.NewGuid();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				protected	bool	_FncCreated	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	SAPFunctionName		{ get; }
				public	Guid		MyID							{	get; }
				//.................................................
				public	IRfcFncProfile		Profile						{ get; }
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
						catch ( Exception ex )
							{
								throw new System.Exception( "NCO invoke error" , ex );
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
