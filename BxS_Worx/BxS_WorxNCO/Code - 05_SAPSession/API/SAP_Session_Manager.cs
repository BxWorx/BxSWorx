using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxUtil.ObjectPool;

using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.TableReader;
using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxNCO.BDCSession.Parser;
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.SAPSession.Main;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.SAPSession.API
{
	public class SAP_Session_Manager : ISAP_Session_Manager
		{
			#region "Constructors"



				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAP_Session_Manager(	IRfcDestination	rfcDestination )
					{
						this.RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(SAP_Session_Manager).Namespace}:- RfcDest Factory null" );
						//.............................................
						this._IsReady				= false;
						//.............................................
						this._Factory				= new	Lazy< SAP_Session_Factory >	( ()=>	SAP_Session_Factory.Instance								, cz_LM );
						this._RfcFncCntlr		= new	Lazy< IRfcFncController		>	(	()=>	new	RfcFncController( this.RfcDestination )	,	cz_LM );
						//.............................................
						this._TR_Header		= new Lazy< TblRdr_Function >( ()=> _RfcFncCntlr.Value.CreateTblRdrFunction() );
						this._TR_Profile	= new Lazy< TblRdr_Function >( ()=>	_RfcFncCntlr.Value.CreateTblRdrFunction()	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	bool	_UseAltFnc	;
				private						bool	_IsReady		;
				//.................................................
				private	readonly	Lazy< SAP_Session_Factory	>		_Factory			;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr	;
				//.................................................
				private	readonly	Lazy< TblRdr_Function >		_TR_Header;
				private	readonly	Lazy< TblRdr_Function >		_TR_Profile;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IRfcDestination			RfcDestination	{ get; }
				internal	SMC.RfcDestination	SMCDestination	{ get	{	return	this.RfcDestination.SMCDestination; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Destination Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Create BDC session config DTO to configure session environment
				//
				public DTO_BDC_SessionConfig CreateSessionConfig()
					{
						return	new DTO_BDC_SessionConfig {
																									IsSequential			= true
																								, ConsumersMax			= 1
																								,	ConsumersNo				= 1
																								,	PauseTime					= 0
																								, ConsumerThreshold	= 0
																								, QueueAddTimeout		= 0
																								, ProgressInterval	= 10
																																						};
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList< ISAP_Session_Header > SAPSessionList(	String		userId      = "*"
																													,	String		sessionName	= "*"
																													,	DateTime  dateFrom    =	default(DateTime)
																													,	DateTime	dateTo      = default(DateTime)	)
					{

					



						return	new List< ISAP_Session_Header >();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISAP_Session_Profile SAPBDCSession(		string	sessionName
																									,	string	QID
																									, bool		onlyHeader	= false )
					{
						return	this._Factory.Value.CreateSAPProfile();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				private BDC_Session_SAPMsgConsumer	CreateBDCSAPMsgConsumer	()=>	new	BDC_Session_SAPMsgConsumer	( this._RfcFncCntlr.Value.CreateSAPMsgFunction	() );
				private BDC_Session_TranConsumer		CreateBDCTranConsumer		()=>	new	BDC_Session_TranConsumer		( this._RfcFncCntlr.Value.CreateBDCFunction			( this._UseAltFnc ) );

			#endregion

		}
}
