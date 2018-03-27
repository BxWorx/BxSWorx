using System;
using System.Threading;
//.........................................................
using BxS_WorxNCO.Destination.API			;

using BxS_WorxNCO.BDCSession.DTO			;
using BxS_WorxNCO.BDCSession.Parser		;

using BxS_WorxNCO.RfcFunction.Main		;
using BxS_WorxNCO.RfcFunction.BDCTran	;

using BxS_WorxNCO.Helpers.Common			;
using BxS_WorxNCO.Helpers.ObjectPool	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal sealed class BDCSession_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession_Factory( IRfcDestination	rfcDestination )
					{
						this._RfcDest		= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDCSession_Factory).Namespace}:- RfcDest Factory null" );
						//.................................................
						this._ParserFactory		= new Lazy< BDC_Parser_Factory >(	()=>	BDC_Parser_Factory.Instance , _LM			);
						this._RfcFncCntlr			= new	Lazy< IRfcFncController >	(	()=>	new	RfcFncController( this._RfcDest ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const LazyThreadSafetyMode	_LM		= LazyThreadSafetyMode.ExecutionAndPublication;
				//.................................................
				private readonly	Lazy< BDC_Parser_Factory >	_ParserFactory	;
				private	readonly	Lazy< IRfcFncController >		_RfcFncCntlr		;

				private	readonly	IRfcDestination		_RfcDest;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDC_Session >		CreateSessionPool(	CancellationToken	CT
																																,	int		minPoolSize = 1
																																,	int		maxPoolSize	= 5
																																, bool	limiterOn						= false
																																, bool	activateDiagnostics	= false
																																, bool	autoStartMin				= false	)
					{
						return	new ObjectPool< BDC_Session >(	minPoolSize
																									, maxPoolSize
																									, limiterOn
																									, activateDiagnostics
																									, autoStartMin
																									, CT
																									, ()=> this.CreateSession( CT ,   )	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDCSessionConsumer >		CreateSessionConsumerPool( IRfcDestination	rfcDestination)
					{
						IRfcDestination X = rfcDestination;
						return	new ObjectPool< BDCSessionConsumer >( factory:  ()=> this.CreateSessionConsumer()	);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Parser"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDC_Parser >		CreateParserPool(		CancellationToken	CT
																															,	int		minPoolSize = 1
																															,	int		maxPoolSize	= 5
																															, bool	limiterOn						= false
																															, bool	activateDiagnostics	= false
																															, bool	autoStartMin				= false	)
					{
						return	new ObjectPool< BDC_Parser >(		minPoolSize
																									, maxPoolSize
																									, limiterOn
																									, activateDiagnostics
																									, autoStartMin
																									, CT
																									, ()=> this.CreateParser() );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDCSessionConsumer	CreateSessionConsumer()
					{
						//BDCCall_Function	lo_Func			= this._FncCntlr.CreateBDCCallFunction();
						//BDCCall_Lines			lo_BDCData	=	this._Profile.CreateBDCCallLines();
						//lo_Func.Config( this._Header );
						//var X = new BDCSessionConsumer( this._Profile , lo_Func, lo_BDCData );
			
						BDCSessionConsumer		lo_S = null;
						return	lo_S;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Session	CreateSession(	CancellationToken				CT
																					, IProgress<ProgressDTO>	progressHndlr
																					, DTO_BDC_SessionConfig		config				)
					{
						BDCCall_Header				lo_Header = null;

						return	new	BDC_Session(	lo_Header
																		,	this.CreateSessionConsumerPool( this._RfcDest )
																		, config
																		, CT
																		, progressHndlr																			);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Parser CreateParser()
					{
						return	new	BDC_Parser( this._ParserFactory );
					}

			#endregion

		}
}