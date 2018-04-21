using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;
using BxS_WorxNCO.RfcFunction.TableReader;
using BxS_WorxNCO.RfcFunction.DDIC;
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
						this._Factory				= new	Lazy< SAP_Session_Factory >	( ()=>	SAP_Session_Factory.Instance										, cz_LM );
						this._RfcFncCntlr		= new	Lazy< IRfcFncController		>	(	()=>	new	RfcFncController( this.RfcDestination )			,	cz_LM );
						//.............................................
						this._SAPHdrHndlr		= new Lazy< SAP_Session_HandlerHeader	>	( ()=>	new SAP_Session_HandlerHeader	()					, cz_LM );
						this._SAPDatHndlr		= new Lazy< SAP_Session_HandlerData		>	( ()=>	new SAP_Session_HandlerData		()					, cz_LM );

						this._TR_Header			= new Lazy< TblRdr_Function >		( ()=>	this._RfcFncCntlr.Value.CreateTblRdrFunction()		,	cz_LM );
						this._TR_Profile		= new Lazy< TblRdr_Function >		( ()=>	this._RfcFncCntlr.Value.CreateTblRdrFunction()		,	cz_LM );
						this._DDICInfo			= new Lazy< DDICInfo_Function	>	( ()=>	this._RfcFncCntlr.Value.CreateDDICInfoFunction()	, cz_LM );
						//.............................................
						this._IsReady				= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	bool	_IsReady		;
				//.................................................
				private	readonly	Lazy< SAP_Session_Factory	>		_Factory			;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr	;
				//.................................................
				private	readonly	Lazy< SAP_Session_HandlerHeader >		_SAPHdrHndlr;
				private	readonly	Lazy< SAP_Session_HandlerData		>		_SAPDatHndlr;

				private	readonly	Lazy< TblRdr_Function >		_TR_Header;
				private	readonly	Lazy< TblRdr_Function >		_TR_Profile;

				private	readonly	Lazy< DDICInfo_Function >	_DDICInfo;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IRfcDestination			RfcDestination	{ get; }
				private		SMC.RfcDestination	SMCDestination	{ get	{	return	this.RfcDestination.SMCDestination; } }

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
				public async Task<bool>	ReadySessionAsync( bool optimise = true )
					{
						if ( ! this._IsReady )
							{
								this._RfcFncCntlr.Value.RegisterTblRdr();
								this._RfcFncCntlr.Value.RegisterDDICIno();
								//.........................................
								try
									{
										await this.RfcDestination.FetchMetadataAsync( optimise )	.ConfigureAwait(false);
										await	this._RfcFncCntlr.Value.UpdateProfilesAsync()			.ConfigureAwait(false);
										this._IsReady		=	true;
									}
								catch (Exception ex)
									{
										throw new Exception( "SAP Session ready fail" , ex );
									}
							}
						//.................................................
						return	this._IsReady;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IList< ISAP_Session_Header > SAPSessionList(	String		userId      = "*"
																													,	String		sessionName	= "*"
																													,	DateTime  dateFrom    =	default(DateTime)
																													,	DateTime	dateTo      = default(DateTime)	)
					{
						IList< ISAP_Session_Header >	lt_List		=	new List< ISAP_Session_Header >();
						TblRdr_Data										lo_TRData	= this._TR_Header.Value.CreateTblRdrData();
						//.............................................
						this._SAPHdrHndlr.Value.LoadTblRdr			( lo_TRData );
						this._SAPHdrHndlr.Value.Compile_Filter	( lo_TRData , userId , sessionName , dateFrom , dateTo )	;

						this._TR_Profile.Value.Process( lo_TRData , this.SMCDestination )	;

						this._SAPHdrHndlr.Value.ProcessSAPSessionData( lo_TRData , ref lt_List );
						//.............................................
						foreach (ISAP_Session_Header lo in lt_List)
							{
								lo.SAPTCode	= this.GetSAPSessionData( lo.SessionName , lo.QID , true ).SAPTCode;
							}
						//.............................................
						return	lt_List;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISAP_Session_Profile GetSAPSessionData(	string	sessionName
																											,	string	QID
																											, bool		onlyHeader	= false
																											,	bool		inclDDIC		= true	)
					{
						ISAP_Session_Profile	lo_Profile	=	this._Factory		.Value.CreateSAPProfile();
						TblRdr_Data						lo_TRData		= this._TR_Profile.Value.CreateTblRdrData();
						//.............................................
						lo_Profile.SessionName	= sessionName	;
						//.............................................
						this._SAPDatHndlr.Value.LoadTblRdr		( lo_TRData )				;
						this._SAPDatHndlr.Value.Compile_Filter( lo_TRData , QID )	;

						this._TR_Profile.Value.Process( lo_TRData , this.SMCDestination )	;

						this._SAPDatHndlr.Value.ProcessSAPSessionDataHeader( lo_TRData , lo_Profile );
						//.............................................
						if ( ! onlyHeader )
							{
								this._SAPDatHndlr.Value.ProcessSAPSessionData( lo_TRData , lo_Profile );

								if ( inclDDIC )
									{
										this._SAPDatHndlr.Value.ProcessSAPSessionDDICInfo	( lo_Profile );
										this._DDICInfo.Value.Process( lo_Profile.DDICInfo , this.SMCDestination );
									}
							}
						//.............................................
						return	lo_Profile;
					}

			#endregion

		}
}
