﻿using System;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	public class BDC_Session_Manager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_Manager(		BDC_Session_Factory factory )
					{
						this._Factory		=	factory	;
						//.............................................
						this._ParserCfg			= new	Lazy< ObjectPoolConfig< BDC_Parser > >										(	()=>	this._Factory.CreateParserPoolConfig()					,	cz_LM );
						this._ParserPool		= new	Lazy< ObjectPool			< BDC_Parser > >										(	()=>	this._Factory.CreateParserPool()								, cz_LM );

						this._BDCSessCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_TranProcessor > >			(	()=>	this._Factory.CreateBDCSessionPoolConfig()			,	cz_LM );
						this._BDCSessPool		= new	Lazy< ObjectPool			< BDC_Session_TranProcessor > >			(	()=>	this._Factory.CreateBDCSessionPool()						, cz_LM );

						this._SAPMsgCfg			= new	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgProcessor > >		(	()=>	this._Factory.CreateSAPMsgsPoolConfig()					,	cz_LM );
						this._SAPMsgPool		= new	Lazy< ObjectPool			< BDC_Session_SAPMsgProcessor > >		(	()=>	this._Factory.CreateSAPMsgsPool()								, cz_LM );

						this._BDCConsCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_TranConsumer > >			(	()=>	this._Factory.CreateBDCTransConsumerPoolConfig	( true )	,	cz_LM );
						this._BDCConsPool		= new	Lazy< ObjectPool			< BDC_Session_TranConsumer > >			(	()=>	this._Factory.CreateBDCTransConsumerPool				()				, cz_LM );

						this._MsgConsCfg		= new	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgConsumer > >		(	()=>	this._Factory.CreateBDCSAPMsgConsumerPoolConfig	( true )	,	cz_LM );
						this._MsgConsPool		= new	Lazy< ObjectPool			< BDC_Session_SAPMsgConsumer > >		(	()=>	this._Factory.CreateBDCSAPMsgConsumerPool				()				, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDC_Session_Factory	_Factory	;
				//.................................................
				private	readonly	Lazy< ObjectPoolConfig< BDC_Parser > >										_ParserCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Parser > >										_ParserPool		;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_TranProcessor > >			_BDCSessCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_TranProcessor >	>			_BDCSessPool	;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgProcessor > >		_SAPMsgCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_SAPMsgProcessor > >		_SAPMsgPool		;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_TranConsumer > >			_BDCConsCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_TranConsumer > >			_BDCConsPool	;

				private	readonly	Lazy< ObjectPoolConfig< BDC_Session_SAPMsgConsumer > >		_MsgConsCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Session_SAPMsgConsumer > >		_MsgConsPool	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal ObjectPoolConfig< BDC_Parser										> ParserConfiguration				{ get { return	this._ParserCfg		.Value	; } }
				internal ObjectPoolConfig< BDC_Session_TranConsumer			> BDCConsumerConfiguration	{ get { return	this._BDCConsCfg	.Value	; } }
				internal ObjectPoolConfig< BDC_Session_TranProcessor		> BDCSessionConfiguration		{ get { return	this._BDCSessCfg	.Value	; } }
				internal ObjectPoolConfig< BDC_Session_SAPMsgProcessor	> SAPMessageConfiguration		{ get { return	this._SAPMsgCfg		.Value	; } }
				internal ObjectPoolConfig< BDC_Session_SAPMsgConsumer		> MsgConsumerConfiguration	{ get { return	this._MsgConsCfg	.Value	; } }

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

				public async Task<bool>	ReadySessionAsync( bool optimise = true )
					{
						try
							{
								return await this._Factory.ReadyEnvironmentAsync( optimise ).ConfigureAwait(false);
							}
						catch (Exception)
							{
								throw;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<bool> Process(	IExcelBDCSessionRequest							request
																				,	CancellationToken										CT
																				, ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
					  const	bool	lb_Ret	= false;
						DTO_BDC_Session lo_DTOSession	=	this._Factory.CreateSessionDTO();
						//.............................................
						// Parse request, data from an excel spreadsheet, into an BDC Session DTO.
						// used by Process Session.
						//
						bool	lb_ParseOk;

						using (	BDC_Parser lo_Parser = this._ParserPool.Value.Acquire() )
							{
								lb_ParseOk	=	await Task.Run(	()=>	lo_Parser.Parse( request , lo_DTOSession ) ).ConfigureAwait(false);
							}
						//.............................................
						if ( lb_ParseOk )
							{
								using ( BDC_Session_TranProcessor lo_BDCSession = this._BDCSessPool.Value.Acquire() )
									{
										int ln_Trn	=	await	lo_BDCSession.Process_SessionAsync(		lo_DTOSession
																																						, CT
																																						, progressHndlr
																																						, this._BDCConsPool.Value
																																						,	this._Factory.SMCDestination ).ConfigureAwait(false);
									}
								//.............................................
								using ( BDC_Session_SAPMsgProcessor lo_SAPMsgs	= this._SAPMsgPool.Value.Acquire() )
									{
										int ln_Msg	=	await	lo_SAPMsgs.Process_SessionAsync(	lo_DTOSession
																																				, CT
																																				, progressHndlr
																																				, this._MsgConsPool.Value
																																				,	this._Factory.SMCDestination ).ConfigureAwait(false);
									}
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Reconfigure"

				// Applies changes made to individual configurations (made direct in the configuration).
				// This is done as a number of rules exist within the configuration which tracks if any
				// changes made are relevant based on the status of the individual pools.
				//
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ReConfigureBDCSessionPool		()=>	this._BDCSessPool	.Value	.ConfigurePool( this._BDCSessCfg .Value );
				internal void ReConfigureBDCConsumerPool	()=>	this._BDCConsPool	.Value	.ConfigurePool( this._BDCConsCfg .Value );
				internal void ReConfigureParserPool				()=>	this._ParserPool	.Value	.ConfigurePool( this._ParserCfg	 .Value );
				internal void ReConfigureSAPMsgPool				()=>	this._SAPMsgPool	.Value	.ConfigurePool( this._SAPMsgCfg	 .Value );

			#endregion

		}
}
