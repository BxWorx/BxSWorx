using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Main;
using BxS_WorxIPX.API.BDC;
using BxS_WorxNCO.RfcFunction.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.API
{
	internal class BDCSession : IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession( Guid destinationID )
					{
						this._DestID	= destinationID;
						//.............................................
						this._LazyMode	= LazyThreadSafetyMode.ExecutionAndPublication;
						this._IsReady		= false;
						//.............................................
						this._Cntlr_Dst		= new Lazy<IDestinationController>(	()=>		new DestinationController()
																																				, this._LazyMode							);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Guid	_DestID;
				//.................................................
				private readonly	LazyThreadSafetyMode						_LazyMode;
				private readonly	Lazy< IDestinationController	>	_Cntlr_Dst;
				private readonly	Lazy< IRfcFncController				>	_Cntlr_Fnc;
				//.................................................
				private IRfcDestination		_Dest;
				private bool							_IsReady;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// List of  SAP systems
				//
				public IList< ISAPSystemReference > GetSAPSystems()
					{
						return	this._Cntlr_Dst.Value.GetSAPSystems();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Configure the BDC session operating environment
				//
				public void ConfigureOperation( DTO_BDC_SessionConfig dto )
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Process supplied BDC session
				//
				public async Task<bool> Process_SessionAsync( DTO_BDC_Session dto )
					{
						bool	lb_Ret = false;
						lb_Ret = 	await Task.Run( ()=> false ).ConfigureAwait(false);
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Cancel processing of session
				//
				public void CancelProcessing()
					{
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool SetupDestination()
					{
						if ( this._IsReady )	return	this._IsReady;
						//.............................................
						this._Dest	= this._Cntlr_Dst.Value.GetDestination( this._DestID );
						this._Cntlr_Fnc	= 
						


						this._Dest.RegisterRfcFunctionForMetadata( SAPRfcFncConstants )


						//.............................................
						return	this._IsReady;
					}

			#endregion

		}
}
