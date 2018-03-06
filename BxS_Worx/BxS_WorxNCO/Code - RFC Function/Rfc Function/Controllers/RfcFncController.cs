using System;
using System.Collections.Concurrent;
using System.Threading;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API.Destination;
//using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal class RfcFncController : IRfcFncController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncController( IRfcDestination rfcDestination )
					{
						this.RfcDestination		= rfcDestination;
						//.............................................
						this._RfcFncMngr			=	new	Lazy<IRfcFncManager>
																			(	() =>		new	RfcFncManager( this.RfcDestination )
																							, LazyThreadSafetyMode.ExecutionAndPublication );
						//.............................................
						this._SAPRfcFncConst	= new Lazy<SAPRfcFncConstants>
																			(	()=>		new SAPRfcFncConstants()
																							, LazyThreadSafetyMode.ExecutionAndPublication	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy<IRfcFncManager>				_RfcFncMngr			;
				private readonly Lazy<SAPRfcFncConstants>		_SAPRfcFncConst	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IRfcDestination	RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods"

//			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
//			public	BDCCallTranProcessor CreateBDCCallFunction()
//				{
//					return null;

////					this._RfcFncMngr.Value.RegisterFunction();
//					//return	new BDCCallTranProcessor( this._SAPRfcFncConst.Value.BDCCallTran );
//				}

			#endregion

		}
}
