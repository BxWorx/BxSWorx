using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal abstract class RfcFncBase : IRfcFncBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncBase( IRfcFncProfile profile	)
					{
						this.Profile	= profile	??	throw		new	ArgumentException( $"{typeof(RfcFncBase).Namespace}:- Profile null" );
						//.............................................
						this.MyID	= Guid.NewGuid();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid		MyID				{	get; }
				public	string	SAPFncName	{	get		{	return	this.Profile.FunctionName	;	}	}
				//.................................................
				public	IRfcFncProfile		Profile					{ get; }
				public	SMC.IRfcFunction	NCORfcFunction	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke( SMC.RfcCustomDestination rfcDestination )
					{
						bool	lb_Ret	= false;
						//.............................................
						try
							{
								this.NCORfcFunction.Invoke( rfcDestination );
								lb_Ret	= true;
							}
						catch ( Exception ex )
							{
								throw	new Exception( "NCO invoke error" , ex );
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
