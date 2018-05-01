using System;
using System.Collections.Generic;
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
				internal BDC_ParserPipeline( Lazy<BDC_Session_Factory>	factory )
					{
						this._BDCFactory	= factory	??	throw		new	ArgumentException( $"{typeof(BDC_ParserPipeline).Namespace}:- Factory null"		);
						//...
						this._Factory		= new Lazy<BDC_Parser_Factory>			(	()=>	BDC_Parser_Factory.Instance	, cz_LM	);
						this._Pool			= new	Lazy<ObjectPool<BDC_Parser>>	(	()=>	this.CreatePool()						, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<BDC_Session_Factory>			_BDCFactory	;
				//...
				private readonly	Lazy<BDC_Parser_Factory>			_Factory	;
				private	readonly	Lazy<ObjectPool<BDC_Parser>>	_Pool			;

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
				public void Process(	IRequest														request
														, BlockingCollection<DTO_BDC_Session>	TranQueue
														, CancellationToken										CT
														,	ProgressHandler< DTO_BDC_Progress >	progressHndlr )
					{
						using ( BDC_Parser lo_Parser	= this._Pool.Value.Acquire() )
							{
								foreach ( KeyValuePair<int , ISession> ls_kvp in request.Sessions )
									{
										if ( ! CT.IsCancellationRequested )
											{
												DTO_BDC_Session lo_SsnOut	=	this._BDCFactory.Value.CreateSessionDTO();
												//...
												//if ( await Task.Run( ()=> lo_Parser.Parse( ls_kvp.Value , lo_SsnOut ) ).ConfigureAwait(false) )
												if ( lo_Parser.Parse( ls_kvp.Value , lo_SsnOut ) )
													{
														TranQueue.Add( lo_SsnOut );
														//..
														if ( progressHndlr.IsActive && progressHndlr.GoingToHit )
															{
																DTO_BDC_Progress x = progressHndlr.Create();
																progressHndlr.Report( x );
															}
													}
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
