using System;
using System.Threading;
//.........................................................
using BxS_SAPNCO.API.SAPFunctions.BDC.Session;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCOpEnv<P>
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCOpEnv(	DestinationRfc								destination							,
														IBDCProfile										profile									,
														BDC2RfcParser									parser									,
														BDCProfileConfigurator				configurator						,
														IProgress<P>									progressHndlr						,
														CancellationTokenSource				CTS											,
														Func<Guid, BDCSessionTran>		createBDCSessionTran		,
														Func<DTO_RFCSessionHeader>		createRFCSessionHeader	,
														Func<DTO_RFCSessionTran>			createRFCSessionTran		,
														Func<DTO_SessionProgressInfo>	createDTOProgressInfo			)
					{
						this.Destination		= destination		;
						this.Profile				= profile				;
						this.Parser					= parser				;
						this.Configurator		= configurator	;
						this.ProgressHndlr	= progressHndlr	;
						this._CTS						= CTS						;
						//.............................................
						this.CreateSessionBDCTran			= createBDCSessionTran		;
						this.CreateSessionRFCHeader		= createRFCSessionHeader	;
						this.CreateSessionRFCTran			= createRFCSessionTran		;
						this.CreateSessionDTOProgInfo	= createDTOProgressInfo		;
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

				internal Func<Guid, BDCSessionTran>			CreateSessionBDCTran			{ get; }
				internal Func<DTO_RFCSessionHeader>			CreateSessionRFCHeader		{ get; }
				internal Func<DTO_RFCSessionTran>				CreateSessionRFCTran			{ get; }
				internal Func<DTO_SessionProgressInfo>	CreateSessionDTOProgInfo	{ get; }

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
