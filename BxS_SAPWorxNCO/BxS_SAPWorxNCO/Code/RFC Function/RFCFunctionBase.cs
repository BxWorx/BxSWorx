using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.RfcFunction
{
	internal abstract class RFCFunctionBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RFCFunctionBase( IRfcFncProfile	profile	)
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
				public bool Invoke()
					{
						bool	lb_Ret	= true;
						//.............................................
						try
							{
								this._RfcFunction.Invoke( this._Profile.RfcDestination );
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool CreateFunction()
					{
						if (this._Profile.Ready())
							{
								if (!this._FncCreated)
									{
										try
											{
												this._RfcFunction		= this._Profile.Metadata.CreateFunction();
												this._FncCreated		= !this._FncCreated;
											}
										catch (System.Exception)
											{
											throw;
											}
									}
							}
						//.............................................
						return	this._FncCreated;
					}

			#endregion

		}
}
