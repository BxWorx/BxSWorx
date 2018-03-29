using System;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................

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

						this._NCORfcFunction	= new Lazy< SMC.IRfcFunction >
																					(		()=>	this.Profile.CreateRfcFunction()
																						, LazyThreadSafetyMode.ExecutionAndPublication );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy< SMC.IRfcFunction > _NCORfcFunction;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	MyID	{	get; }
				//.................................................
				public	IRfcFncProfile		Profile					{ get; }
				public	SMC.IRfcFunction	NCORfcFunction	{ get { return	this._NCORfcFunction.Value	; } }
				//.................................................
				private	SMC.RfcCustomDestination	NCODestination	{ get { return	this.Profile.NCODestination	; } }

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
								this.Profile.ReadyProfile();
								this._NCORfcFunction.Value.Invoke( rfcDestination );
								lb_Ret	= true;
							}
						catch ( Exception ex )
							{
								throw new Exception( "NCO invoke error" , ex );
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
