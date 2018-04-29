using System;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.Main;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_Session_ParserPipeline
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_ParserPipeline(	IRfcDestination							rfcDestination
																						, Lazy< IRfcFncController		>	rfcFncCntlr
																						, Lazy< BDC_Session_Factory > factory					)
					{
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- RfcDest null"		);
						this._Factory					= factory					??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- Factory null"		);
						this._RfcFncCntlr			= rfcFncCntlr			??	throw		new	ArgumentException( $"{typeof(BDC_Session_SAPMsgPipeline).Namespace}:- FNC Cntlr null" );
						//...
						this._ParserFactory		= new Lazy< BDC_Parser_Factory	>		(	()=>	BDC_Parser_Factory.Instance	, cz_LM	);
						this._ParserCfg			= new	Lazy< ObjectPoolConfig< BDC_Parser > >										(	()=>	this.CreateParserPoolConfig()					,	cz_LM );
						this._ParserPool		= new	Lazy< ObjectPool			< BDC_Parser > >										(	()=>	this.CreateParserPool()								, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IRfcDestination								_RfcDestination	;
				private	readonly	Lazy< BDC_Session_Factory	>		_Factory				;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr		;
				//...
				private readonly	Lazy< BDC_Parser_Factory >	_ParserFactory	;
				private	readonly	Lazy< ObjectPoolConfig< BDC_Parser > >										_ParserCfg		;
				private	readonly	Lazy< ObjectPool			< BDC_Parser > >										_ParserPool		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IUTL_Controller			UTL_Cntlr				{ get	{	return	UTL_Controller.Instance	; } }
				private	SMC.RfcDestination	SMCDestination	{ get	{	return	this._RfcDestination.SMCDestination; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task ProcessSAPMsgPipelineAsync(		BlockingCollection<DTO_BDC_Session>				queueIn
																											, BlockingCollection<DTO_BDC_Session>				queueOut
																											, CancellationToken													CT
																											,	ProgressHandler< DTO_BDC_Progress >				progressHndlr )
					{
						using ( BDC_Session_SAPMsgProcessor lo_SAPMsgs	= this._SAPMsgPool.Value.Acquire() )
							{
								foreach ( DTO_BDC_Session lo_DTOSession in queueIn.GetConsumingEnumerable() )
									{
										int ln_Msg	=	await	lo_SAPMsgs.Process_SessionAsync(	lo_DTOSession
																																				, CT
																																				, progressHndlr
																																				, this._MsgConsPool.Value
																																				,	this.SMCDestination ).ConfigureAwait(false);
										queueOut.Add(lo_DTOSession);
									}
							}
					}

			#endregion


			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		BDC_Parser								CreateParser			()=>	new	BDC_Parser	( this._ParserFactory );
				internal	ObjectPool< BDC_Parser >	CreateParserPool	()=>	this.UTL_Cntlr.CreateObjectPool( this.CreateParser		);

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Parser > CreateParserPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig( this.CreateParser , defaults );
					}

			#endregion

		}
}
