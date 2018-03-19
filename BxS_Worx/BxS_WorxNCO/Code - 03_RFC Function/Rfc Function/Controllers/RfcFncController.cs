using System;
using System.Threading;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.BDCTran;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcFncController : IRfcFncController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncController( IRfcDestination rfcDestination )
					{
						this.RfcDestination		= rfcDestination	?? throw new ArgumentException("IRfcDestination is null");
						//.............................................
						this._LazyMode				= LazyThreadSafetyMode.ExecutionAndPublication;

						this._RfcFncMngr			=	new	Lazy<IRfcFncManager>
																			(	() =>		new	RfcFncManager( this.RfcDestination ) , this._LazyMode );

						this._SAPRfcFncConst	= new Lazy<SAPRfcFncConstants>
																			(	()=>		new SAPRfcFncConstants() , this._LazyMode );
						//.............................................
						this._Lock	= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly LazyThreadSafetyMode				_LazyMode				;
				private readonly Lazy<IRfcFncManager>				_RfcFncMngr			;
				private readonly Lazy<SAPRfcFncConstants>		_SAPRfcFncConst	;

				private readonly	object	_Lock;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IRfcDestination	RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Call"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterBDCCallProfile( bool loadMetaData = false )
					{
						if ( ! this._RfcFncMngr.Value.ProfileExists( this._SAPRfcFncConst.Value.BDCCallTran ) )
							{
								lock (this._Lock)
									{
										if ( ! this._RfcFncMngr.Value.ProfileExists( this._SAPRfcFncConst.Value.BDCCallTran ) )
											{
												var	lo_Prof	= new BDCCall_Profile(	this._SAPRfcFncConst.Value.BDCCallTran
																													,	this.RfcDestination
																													,	()=>	new BDCCall_Header()
																													,	()=>	new	BDCCall_Lines	()							);

												this._RfcFncMngr.Value.RegisterProfile( lo_Prof , loadMetaData );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCall_Profile GetAddBDCCallProfile()
					{
						this.RegisterBDCCallProfile();
						//.............................................
						return	this._RfcFncMngr.Value.GetProfile< BDCCall_Profile >( this._SAPRfcFncConst.Value.BDCCallTran );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCall_Function CreateBDCCallFunction()
					{
						return	new BDCCall_Function(	this.GetAddBDCCallProfile() );
					}

			#endregion

		}
}
