using System;
using System.Threading;
using System.Collections.Generic;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
using BxS_WorxNCO.RfcFunction.Main;
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
				private readonly	Lazy< IRfcFncController			 >	_Cntlr_Fnc;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of only requested SAP systems
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
						IRfcDestination		lo_D	= this._Cntlr_Dst.Value.GetDestination( destinationID );
						IRfcFncController lo_F	= new RfcFncController( lo_D );

						return	new BDCSession( destinationID );
					}

			#endregion

		}
}
