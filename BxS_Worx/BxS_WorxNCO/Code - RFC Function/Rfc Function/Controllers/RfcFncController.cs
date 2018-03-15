using System;
using System.Threading;
//.........................................................
using BxS_WorxNCO.Destination.API.Destination;
using BxS_WorxNCO.RfcFunction.BDCTran;
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

						this._SAPRfcFncConst	= new Lazy<SAPRfcFncConstants>
																			(	()=>		new SAPRfcFncConstants()
																							, LazyThreadSafetyMode.ExecutionAndPublication );
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCall_Function CreateBDCCallFunction()
					{
						if ( ! this._RfcFncMngr.Value.ProfileExists( this._SAPRfcFncConst.Value.BDCCallTran ) )
							{
								var	lo_Prof	= new BDCCall_Profile(	this._SAPRfcFncConst.Value.BDCCallTran
																									,	this.RfcDestination
																									,	()=>	new BDCCall_Header()
																									,	()=>	new	BDCCall_Lines	()	);

								this._RfcFncMngr.Value.RegisterProfile( lo_Prof );
							}
						//.............................................
						var lo_Fnc	= new BDCCall_Function(	this._RfcFncMngr.Value.GetProfile<BDCCall_Profile>( this._SAPRfcFncConst.Value.BDCCallTran ) );
						//.............................................
						return	lo_Fnc;
					}

			#endregion

		}
}
