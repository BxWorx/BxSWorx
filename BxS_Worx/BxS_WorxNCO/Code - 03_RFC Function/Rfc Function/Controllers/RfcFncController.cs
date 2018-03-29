using System;
using System.Threading;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.SAPMsg;

using	static	BxS_WorxNCO.RfcFunction.Main.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcFncController : IRfcFncController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncController( IRfcDestination rfcDestination )
					{
						this.RfcDestination		= rfcDestination	??	throw		new ArgumentException( $"{typeof(BDCCall_Data).Namespace}:- BDCData null" );
						//.............................................
						this._LM							= LazyThreadSafetyMode.ExecutionAndPublication;
						this._RfcFncMngr			=	new	Lazy<IRfcFncManager>		(	()=>	new	RfcFncManager( this.RfcDestination )	, this._LM );
						//.............................................
						this._Lock	= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	LazyThreadSafetyMode				_LM							;
				private readonly	Lazy<IRfcFncManager>				_RfcFncMngr			;
				//.................................................
				private readonly	object	_Lock;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IRfcDestination		RfcDestination	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: BDC Call"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterBDCCallProfile( bool loadMetaData = false )
					{
						if ( ! this._RfcFncMngr.Value.ProfileExists( cz_BDCCallTran ) )
							{
								lock (this._Lock)
									{
										if ( ! this._RfcFncMngr.Value.ProfileExists( cz_BDCCallTran ) )
											{
												var	lo_Prof	= new BDCCall_Profile(	cz_BDCCallTran
																													,	this.RfcDestination
																													, BDCCall_Factory.Instance );

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
						return	this._RfcFncMngr.Value.GetProfile< BDCCall_Profile >( cz_BDCCallTran );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCall_Function CreateBDCCallFunction()
					{
						return	new BDCCall_Function(	this.GetAddBDCCallProfile() );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: SAP Message Compiler"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterSAPMsgProfile( bool loadMetaData = false )
					{
						if ( ! this._RfcFncMngr.Value.ProfileExists( cz_SAPMsgCompiler ) )
							{
								lock (this._Lock)
									{
										if ( ! this._RfcFncMngr.Value.ProfileExists( cz_SAPMsgCompiler ) )
											{
												var	lo_Prof	= new BDCCall_Profile(	cz_SAPMsgCompiler
																													,	this.RfcDestination
																													, BDCCall_Factory.Instance	);

												this._RfcFncMngr.Value.RegisterProfile( lo_Prof , loadMetaData );
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SAPMsg_Profile GetAddSAPMsgProfile()
					{
						this.RegisterSAPMsgProfile();
						//.............................................
						return	this._RfcFncMngr.Value.GetProfile< SAPMsg_Profile >( cz_SAPMsgCompiler );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SAPMsg_Function CreateSAPMsgFunction()
					{
						return	new SAPMsg_Function( this.GetAddSAPMsgProfile() );
					}

			#endregion

		}
}
