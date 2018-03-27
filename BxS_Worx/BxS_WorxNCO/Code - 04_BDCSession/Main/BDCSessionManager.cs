using System;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxNCO.Helpers;
using BxS_WorxNCO.Helpers.ObjectPool;

using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	public class BDCSessionManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionManager(	IRfcDestination	rfcDestination )
					{
						this._RfcDest			= rfcDestination	;
						//.............................................
						this._LM	= LazyThreadSafetyMode.ExecutionAndPublication;
						//.............................................
						this._ReqQueue			=	new	Lazy< PriorityQueue< IExcelBDCSessionRequest > >
																		(	()=>	new PriorityQueue<IExcelBDCSessionRequest>()		, this._LM );

						this._SessionPool		= new	Lazy< ObjectPool< BDC_Session > >
																		(	()=> BDCSession_Factory.Instance.CreateSessionPool()	, this._LM );

						this._ParserPool		= new	Lazy< ObjectPool< BDC_Parser > >
																		(	()=> BDCSession_Factory.Instance.CreateParserPool()		, this._LM );

						this._ConsumerPool	= new	Lazy< ObjectPool< BDCSessionConsumer > >
																		(	()=> BDCSession_Factory.Instance.CreateSessionConsumerPool( this._RfcDest )	, this._LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	LazyThreadSafetyMode	_LM	;
				//.................................................
				private	readonly	IRfcDestination		_RfcDest	;
				//.................................................
				private	readonly	Lazy< PriorityQueue< IExcelBDCSessionRequest > >	_ReqQueue;
				//.................................................
				private	readonly	Lazy< ObjectPool< BDC_Parser	> >						_ParserPool		;
				private	readonly	Lazy< ObjectPool< BDC_Session > >						_SessionPool	;
				private	readonly	Lazy< ObjectPool< BDCSessionConsumer	> >		_ConsumerPool	;

				private	CancellationTokenSource		_CTS			;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Create BDC session config DTO to configure session environment
				//
				public DTO_BDC_SessionConfig CreateSessionConfig()
					{
						return	new DTO_BDC_SessionConfig {
																									IsSequential			= true
																								, ConsumersMax			= 1
																								,	ConsumersNo				= 1
																								,	PauseTime					= 0
																								, ConsumerThreshold	= 0
																								, QueueAddTimeout		= 0
																								, ProgressInterval	= 10
																																						};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session destination environment
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ConfigureDestination( IConfigSetupDestination dto )
					{
						//this._FncCntlr.RfcDestination.LoadConfig( dto );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task Process( IExcelBDCSessionRequest request )
					{
						DTO_BDC_Session dtoSession	= BDCSession_Factory.Instance.CreateSessionDTO();
						//.............................................
						// Parse request, data from an excel spreadsheet, into an BDC Session DTO.
						// used by Process Session.
						//
						bool	lb_ParseOk;

						using (	BDC_Parser lo_Parser = this._ParserPool.Value.Acquire() )
							{
								lb_ParseOk	=	await Task.Run(	()=>	lo_Parser.Parse( request , dtoSession ) )
																													.ConfigureAwait(false);
							}
						//.............................................
						using ( BDC_Session lo_Session = this._SessionPool.Value.Acquire() )
							{
								int i = await	lo_Session.Process_SessionAsync( dtoSession ).ConfigureAwait(false);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Cancel processing of session
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void CancelProcessing()
					{
						this._CTS?.Cancel();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDCSessionManager	CreateBDCSessionManager( IRfcDestination rfcDestination )
					{
						IRfcFncController				lo_F	= new RfcFncController( rfcDestination );
						CancellationTokenSource	cts		= new CancellationTokenSource();
						IProgress<ProgressDTO>	prog	= new	Progress<ProgressDTO>();
						//.............................................
						return	new BDC_Session( lo_F , this.CreateSessionConfig() , cts.Token , prog );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CloseSession()
					{
						this._CTS		=	null;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void PrepareSession()
					{
						this._CTS		=	new CancellationTokenSource();
					}

			#endregion

		}
}
