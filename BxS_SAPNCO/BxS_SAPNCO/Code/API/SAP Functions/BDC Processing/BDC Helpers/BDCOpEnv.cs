using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCOpEnv
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv(	DestinationRfc	destination	,
														IBDCProfile			profile			,
														BDCOpFnc				opFnc					)
					{
						this.Destination	= destination		;
						this.Profile			= profile				;
						this._OpFnc				= opFnc					;
						//.............................................
						this.IsStarted	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCOpFnc	_OpFnc;
				//.................................................
				private CancellationTokenSource			_CTS						;
				private IProgress<DTO_ProgressInfo>	_ProgressHndlr	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	IsStarted	{ get;	private set; }

				internal	DestinationRfc					Destination		{	get; }
				internal	IBDCProfile							Profile				{	get; }

				internal	BDCProfileConfigurator	Configurator	{	get; private set;	}
				internal	BDC2RfcParser						Parser				{	get; private set;	}

				//internal	Pipeline<DTO_RFCTran,DTO_ProgressInfo>	Pipeline	{	get; private set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Cancel()
					{
						this._CTS?.Cancel();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Start()
					{
						if (!this.IsStarted)
							{
								this.Configurator		= this._OpFnc.CreateProfileConfigurator();
								this.Parser					= this._OpFnc.CreateParser(this.Profile);
								this._ProgressHndlr	= this._OpFnc.CreateProgressHandler();
								this._CTS						= new	CancellationTokenSource();
								//.............................................
								try
									{
										if (!this.Destination.Procure())								throw	new Exception();
										if (!this.Configurator.Configure(this.Profile))	throw	new Exception();
										//.............................................
										this.IsStarted	= true;
									}
								catch (Exception)
									{
										this.Configurator		= null;
										this.Parser					= null;
										this._ProgressHndlr	= null;
										this._CTS						= null;
									}
							}
						//.............................................
						return	this.IsStarted;
					}

			#endregion

		}
}
