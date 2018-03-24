using System;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxNCO.Helpers;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.Helpers.ObjectPool;
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal class BDCSessionManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionManager(	IRfcDestination	rfcDestination )
					{
						this._LM	= LazyThreadSafetyMode.ExecutionAndPublication;
						//.............................................
						this._RfcDest			= rfcDestination	;
						//this._Factory			= factory					;
						//.............................................
						this._ReqQueue		=	new	Lazy< PriorityQueue< IExcelBDCSessionRequest > >
																		(	()=>	new PriorityQueue<IExcelBDCSessionRequest>() , this._LM	);

						this._SessionPool		= new	Lazy< ObjectPool< BDC_Session > >
																		(	()=> BDCSession_Factory.Instance.CreateSessionPool() , this._LM	);

						this._ParserPool		= new	Lazy< ObjectPool< BDC_Parser > >
																		(	()=> BDCSession_Factory.Instance.CreateParserPool() , this._LM	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	LazyThreadSafetyMode	_LM	;
				//.................................................
				private	readonly	IRfcDestination			_RfcDest	;
				//private	readonly	BDCSession_Factory	_Factory	;
				//.................................................
				private	readonly	Lazy< PriorityQueue< IExcelBDCSessionRequest > >	_ReqQueue;
				//.................................................
				private	readonly	Lazy< ObjectPool< BDC_Parser > >									_ParserPool;
				private	readonly	Lazy< ObjectPool< BDC_Session > >									_SessionPool;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				public async Task Process( IExcelBDCSessionRequest request )
					{
						this._ReqQueue.Value.Add( request , request.Priority );

						DTO_BDC_Session x	= BDCSession_Factory.Instance.CreateSessionDTO();

						//.............................................
						using (	BDC_Parser lo_Parser = this._ParserPool.Value.Acquire() )
							{
								lo_Parser.Parse( request , x );
							}
						//.............................................
						using ( BDC_Session lo_Session = this._SessionPool.Value.Acquire() )
							{
								int i = await	lo_Session.Process_SessionAsync( x ).ConfigureAwait(false);
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

			#endregion

		}
}
