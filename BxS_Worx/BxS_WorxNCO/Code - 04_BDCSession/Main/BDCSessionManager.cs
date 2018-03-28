using System;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxNCO.Helpers.ObjectPool;
using BxS_WorxNCO.Helpers.Common;

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
						this._RfcDest				= rfcDestination	;
						//.............................................
						this._Factory				= new	Lazy< BDCSession_Factory >											(	()=>	new BDCSession_Factory( this._RfcDest )						, _LM );

						this._ParserCfg			= new	Lazy< ObjectPoolConfig< BDC_Parser > >					(	()=>	this._Factory.Value.CreateParserPoolConfig()			,	_LM );
						this._ParserPool		= new	Lazy< ObjectPool			< BDC_Parser > >					(	()=>	this._Factory.Value.CreateParserPool()						, _LM );

						this._BDCConsCfg		= new	Lazy< ObjectPoolConfig< BDCSessionConsumer > >	(	()=>	this._Factory.Value.CreateBDCConsumerPoolConfig()	,	_LM );
						this._BDCConsPool		= new	Lazy< ObjectPool			< BDCSessionConsumer > >	(	()=>	this._Factory.Value.CreateBDCConsumerPool()				, _LM );

						this._BDCSessCfg		= new	Lazy< ObjectPoolConfig< BDC_Session > >					(	()=>	this._Factory.Value.CreateBDCSessionPoolConfig()	,	_LM );
						this._BDCSessPool		= new	Lazy< ObjectPool			< BDC_Session > >					(	()=>	this._Factory.Value.CreateBDCSessionPool()				, _LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const LazyThreadSafetyMode	_LM		= LazyThreadSafetyMode.ExecutionAndPublication;
				//.................................................
				private	readonly	IRfcDestination		_RfcDest	;
				//.................................................
				private	readonly	Lazy< BDCSession_Factory >	_Factory	;
				//.................................................
				private	readonly	Lazy< ObjectPoolConfig< BDC_Parser > >					_ParserCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Parser > >					_ParserPool		;

				private	readonly	Lazy< ObjectPoolConfig< BDCSessionConsumer > >	_BDCConsCfg		;
				private	readonly	Lazy< ObjectPool			< BDCSessionConsumer > >	_BDCConsPool	;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session > >					_BDCSessCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session > >					_BDCSessPool	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal ObjectPoolConfig< BDC_Session				> BDCSessionConfiguration		{ get { return	this._BDCSessCfg.Value; } }
				internal ObjectPoolConfig< BDCSessionConsumer > BDCConsumerConfiguration	{ get { return	this._BDCConsCfg.Value; } }
				internal ObjectPoolConfig< BDC_Parser					> ParserConfiguration				{ get { return	this._ParserCfg.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Create BDC session config DTO to configure SAP Logon environment
				//
				public IConfigSetupDestination CreateDestinationConfig()
					{
						return	this._RfcDest.CreateDestinationConfig();
					}



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
				#pragma warning disable RCS1163
				public void ConfigureDestination( IConfigSetupDestination dto )
					{
						//this._FncCntlr.RfcDestination.LoadConfig( dto );
					}
				#pragma warning restore RCS1163

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task Process(	IExcelBDCSessionRequest request
																	,	CancellationToken				CT
																	, Progress<ProgressDTO>		progressHndlr	)
					{
						DTO_BDC_Session dtoSession	=	this._Factory.Value.CreateSessionDTO();
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
						using ( BDC_Session lo_Session = this._BDCSessPool.Value.Acquire() )
							{
								int i = await	lo_Session.Process_SessionAsync(	dtoSession
																															, CT
																															, progressHndlr
																															, this._BDCConsPool.Value )
																.ConfigureAwait(false);
							}
					}

			#endregion

			//===========================================================================================
			//===========================================================================================
			#region "Methods: Exposed: Reconfigure"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReConfigureBDCSessionPool()
					{
						this._BDCSessPool.Value.ConfigurePool( this._BDCSessCfg.Value );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReConfigureBDCConsumerPool()
					{
						this._BDCConsPool.Value.ConfigurePool( this._BDCConsCfg.Value );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReConfigureParserPool()
					{
						this._ParserPool.Value.ConfigurePool( this._ParserCfg.Value );
					}

			#endregion

		}
}
