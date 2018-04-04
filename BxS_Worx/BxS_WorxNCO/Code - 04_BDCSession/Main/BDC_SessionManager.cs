using System;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxNCO.Helpers.ObjectPool;
using BxS_WorxNCO.Helpers.Progress;

using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	public class BDC_SessionManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_SessionManager(	BDC_SessionFactory factory )
					{
						this._Factory				=	factory;
						//.............................................
						this._ParserCfg			= new	Lazy< ObjectPoolConfig< BDC_Parser > >					(	()=>	this._Factory.CreateParserPoolConfig()			,	cz_LM );
						this._ParserPool		= new	Lazy< ObjectPool			< BDC_Parser > >					(	()=>	this._Factory.CreateParserPool()						, cz_LM );

						this._BDCConsCfg		= new	Lazy< ObjectPoolConfig< BDC_SessionConsumer > >	(	()=>	this._Factory.CreateBDCConsumerPoolConfig()	,	cz_LM );
						this._BDCConsPool		= new	Lazy< ObjectPool			< BDC_SessionConsumer > >	(	()=>	this._Factory.CreateBDCConsumerPool()				, cz_LM );

						this._BDCSessCfg		= new	Lazy< ObjectPoolConfig< BDC_Session > >					(	()=>	this._Factory.CreateBDCSessionPoolConfig()	,	cz_LM );
						this._BDCSessPool		= new	Lazy< ObjectPool			< BDC_Session > >					(	()=>	this._Factory.CreateBDCSessionPool()				, cz_LM );

						this._SAPMsgCfg			= new	Lazy< ObjectPoolConfig< BDC_SessionSAPMsgs > >	(	()=>	this._Factory.CreateSAPMsgsPoolConfig()			,	cz_LM );
						this._SAPMsgPool		= new	Lazy< ObjectPool			< BDC_SessionSAPMsgs > >	(	()=>	this._Factory.CreateSAPMsgsPool()						, cz_LM );
						//.............................................
						this._IsReady	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDC_SessionFactory	_Factory	;
				//.................................................
				private	readonly	Lazy< ObjectPoolConfig< BDC_Parser > >					_ParserCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Parser > >					_ParserPool		;

				private	readonly	Lazy< ObjectPoolConfig< BDC_SessionConsumer > >	_BDCConsCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_SessionConsumer > >	_BDCConsPool	;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session > >					_BDCSessCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session > >					_BDCSessPool	;

				private	readonly	Lazy< ObjectPoolConfig< BDC_SessionSAPMsgs > >	_SAPMsgCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_SessionSAPMsgs > >	_SAPMsgPool		;
				//.................................................
				private bool _IsReady;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal ObjectPoolConfig< BDC_Parser						> ParserConfiguration				{ get { return	this._ParserCfg	.Value	; } }
				internal ObjectPoolConfig< BDC_SessionConsumer	> BDCConsumerConfiguration	{ get { return	this._BDCConsCfg.Value	; } }
				internal ObjectPoolConfig< BDC_Session					> BDCSessionConfiguration		{ get { return	this._BDCSessCfg.Value	; } }
				internal ObjectPoolConfig< BDC_SessionSAPMsgs		> SAPMessageConfiguration		{ get { return	this._SAPMsgCfg	.Value	; } }

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

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				public async Task<bool>	ReadySessionAsync()
					{
						



					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task Process(	IExcelBDCSessionRequest							request
																	,	CancellationToken										CT
																	, ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
						DTO_BDC_Session lo_DTOSession	=	this._Factory.CreateSessionDTO();
						//.............................................
						// Parse request, data from an excel spreadsheet, into an BDC Session DTO.
						// used by Process Session.
						//
						bool	lb_ParseOk;

						using (	BDC_Parser lo_Parser = this._ParserPool.Value.Acquire() )
							{
								lb_ParseOk	=	await Task.Run(	()=>	lo_Parser.Parse( request , lo_DTOSession ) )
																											.ConfigureAwait(false);
							}
						//.............................................
						if ( lb_ParseOk )
							{
								using ( BDC_Session lo_BDCSession = this._BDCSessPool.Value.Acquire() )
									{
										int i = await	lo_BDCSession.Process_SessionAsync(		lo_DTOSession
																																			, CT
																																			, progressHndlr
																																			, this._BDCConsPool.Value
																																			,	this._Factory.RfcDestination.SMCDestination.CreateCustomDestination() )
																		.ConfigureAwait(false);
									}
								//.............................................
								using ( BDC_SessionSAPMsgs lo_SAPMsgs	= this._SAPMsgPool.Value.Acquire() )
									{
										int i = await	lo_SAPMsgs.ProcessAsync().ConfigureAwait(false);
									}
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Reconfigure"

				// Applies changes made to individual configurations (made direct in the configuration).
				// This is done as a number of rules exist within the configuration which tracks if any
				// changes made are relevant based on th status of the individual pools.
				//
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReConfigureBDCSessionPool		()=>	this._BDCSessPool.Value	.ConfigurePool( this._BDCSessCfg.Value );
				internal void ReConfigureBDCConsumerPool	()=>	this._BDCConsPool.Value	.ConfigurePool( this._BDCConsCfg.Value );
				internal void ReConfigureParserPool				()=>	this._ParserPool.Value	.ConfigurePool( this._ParserCfg.Value	 );

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Ready()
					{
						
					}

			#endregion

		}
}
