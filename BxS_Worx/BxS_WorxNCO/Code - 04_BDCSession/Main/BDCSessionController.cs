using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxIPX.API.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	internal class BDCSessionController : IBDCSessionController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionController()
					{
						this._LazyMode = LazyThreadSafetyMode.ExecutionAndPublication;
						//.............................................
						this._Cntlr_Dst		= new Lazy<IDestinationController>(	()=>		new DestinationController()
																																				, this._LazyMode							);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	LazyThreadSafetyMode _LazyMode;
				//.................................................
				private readonly	Lazy< IDestinationController >	_Cntlr_Dst;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of SAP systems
				//
				public IList< ISAPSystemReference > GetSAPSystems()
					{
						return	this._Cntlr_Dst.Value.GetSAPSystems();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				public	IBDCSession	CreateBDCSession( Guid destinationID )
					{
						IRfcDestination				lo_D			= this._Cntlr_Dst.Value.GetDestination( destinationID );
						IRfcFncController			lo_F			= new RfcFncController( lo_D );

						DTO_BDC_SessionConfig	lo_Config	= new DTO_BDC_SessionConfig {		IsSequential			= true
																																					, ConsumersMax			= 1
																																					,	ConsumersNo				= 1
																																					,	PauseTime					= 0
																																					, ConsumerThreshold	= 0
																																					, ProgressInterval	= 10
																																					, QueueAddTimeout		= 0		};
						//.............................................
						return	new BDCSession( lo_F , lo_Config );
					}

			#endregion

		}
}
