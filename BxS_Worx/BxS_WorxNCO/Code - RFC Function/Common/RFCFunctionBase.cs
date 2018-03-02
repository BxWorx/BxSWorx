using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal abstract class RfcFunctionBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFunctionBase( IRfcFncProfile	profile	)
					{
						this._Profile			= profile	;
						//.............................................
						this._FncCreated	= false		;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	IRfcFncProfile	_Profile;

				protected	bool							_FncCreated		;
				protected	SMC.IRfcFunction	_RfcFunction	;

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
								if ( this.CreateFunction() )
									{
										this._RfcFunction.Invoke( rfcDest );
										lb_Ret	= true;
									}
							}
						catch
							{	}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool CreateFunction()
					{
						if (!this._FncCreated)
							{
								try
									{
										//this._RfcFunction		= this._Profile.Metadata.CreateFunction();
										this._FncCreated		= !this._FncCreated;
									}
								catch (System.Exception)
									{
									throw;
									}
							}
						//.............................................
						return	this._FncCreated;
					}

			#endregion

		}
}
