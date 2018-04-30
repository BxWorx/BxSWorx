using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.RfcFunction.Main;

using BxS_WorxIPX.BDC;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDC_ParserPipeline
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_ParserPipeline(	IRfcDestination							rfcDestination
																		, Lazy< IRfcFncController		>	rfcFncCntlr
																		, Lazy< BDC_Session_Factory > factory					)
					{
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_ParserPipeline).Namespace}:- RfcDest null"		);
						this._ReqFactory			= factory					??	throw		new	ArgumentException( $"{typeof(BDC_ParserPipeline).Namespace}:- Factory null"		);
						this._RfcFncCntlr			= rfcFncCntlr			??	throw		new	ArgumentException( $"{typeof(BDC_ParserPipeline).Namespace}:- FNC Cntlr null" );
						//...
						this._Factory		= new Lazy<BDC_Parser_Factory>				(	()=>	BDC_Parser_Factory.Instance	, cz_LM	);
						this._Pool			= new	Lazy< ObjectPool<BDC_Parser> >	(	()=>	this.CreatePool()						, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IRfcDestination								_RfcDestination	;
				private	readonly	Lazy< BDC_Session_Factory	>		_ReqFactory			;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr		;
				//...
				private readonly	Lazy< BDC_Parser_Factory >			_Factory	;
				private	readonly	Lazy< ObjectPool<BDC_Parser> >	_Pool			;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IUTL_Controller			_Toolset					{ get	{	return	UTL_Controller.Instance	; } }
				//private	SMC.RfcDestination	_SMCDestination		{ get	{	return	this._RfcDestination.SMCDestination; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjectPoolConfig<BDC_Parser> GetConfig()																				=>	this._Pool.Value.ConfigCopy;
				public void													Configure( ObjectPoolConfig<BDC_Parser> config )	=>	this._Pool.Value.ConfigurePool( config );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task ProcessAsync(		BlockingCollection<ISession>				queueIn
																				, BlockingCollection<DTO_BDC_Session>	queueOut
																				, CancellationToken										CT
																				,	ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
						using ( BDC_Parser lo_Parser	= this._Pool.Value.Acquire() )
							{
								foreach ( ISession lo_SsnIn in queueIn.GetConsumingEnumerable( CT ) )
									{
										DTO_BDC_Session lo_SsnOut	=	this._ReqFactory.Value.CreateSessionDTO();
										if ( await Task.Run( ()=> lo_Parser.Parse( lo_SsnIn , lo_SsnOut ) ).ConfigureAwait(false) )
											{
												queueOut.Add( lo_SsnOut );
												DTO_BDC_Progress x = progressHndlr.Create();
												progressHndlr.Report( x );
											}
									}
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	BDC_Parser							CreateParser()	=>	new	BDC_Parser	( this._Factory );
				private	ObjectPool<BDC_Parser>	CreatePool	()	=>	this._Toolset.CreateObjectPool( this.CreateParser	);

			#endregion

		}
}
