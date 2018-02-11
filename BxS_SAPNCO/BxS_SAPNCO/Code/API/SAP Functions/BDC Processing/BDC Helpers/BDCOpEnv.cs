using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCOpEnv
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv(	DestinationRfc								destination		,
														IBDCProfile										profile				,
														BDC2RfcParser									parser				,
														BDCProfileConfigurator				configurator	,
														IProgress<DTO_ProgressInfo>		progressHndlr	,
														CancellationTokenSource				CTS							)
					{
						this.Destination		= destination		;
						this.Profile				= profile				;
						this.Parser					= parser				;
						this.Configurator		= configurator	;
						this.ProgressHndlr	= progressHndlr	;
						this._CTS						= CTS						;
						//.............................................
						this.IsStarted	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly CancellationTokenSource		_CTS;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	IsStarted	{ get;	private set; }

				internal	DestinationRfc						Destination		{	get; }
				internal	BDCProfileConfigurator		Configurator	{	get; }
				internal	BDC2RfcParser							Parser				{	get; }
				internal	IBDCProfile								Profile				{	get; }
				internal	IProgress<P>							ProgressHndlr	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Cancel()
					{
						this._CTS?.Cancel();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Start()
					{
						if (this.IsStarted)	return;
						//.............................................
						this.Destination.Procure();
						this.Configurator.Configure(this.Profile);
						//.............................................
						this.IsStarted	= true;
					}

			#endregion

		}
}
