using System;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.API;

using BxS_WorxNCO.Destination.API			;

using BxS_WorxNCO.BDCSession.DTO			;
using BxS_WorxNCO.BDCSession.Parser		;

using BxS_WorxNCO.RfcFunction.Main		;
using BxS_WorxNCO.RfcFunction.BDCTran	;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal sealed class BDC_Session_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_Factory(		IRfcDestination	rfcDestination
																			, bool						useTranVersion = false )
					{
						this.RfcDestination		= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_Session_Factory).Namespace}:- RfcDest Factory null" );
						this._UseTrnVers			= useTranVersion	;
						//.............................................
						this._ParserFactory		= new Lazy< BDC_Parser_Factory	>		(	()=>	BDC_Parser_Factory.Instance									, cz_LM	);
						this._RfcFncCntlr			= new	Lazy< IRfcFncController		>		(	()=>	new	RfcFncController( this.RfcDestination )	,	cz_LM );
						//.............................................
						this._IsReady	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< BDC_Parser_Factory	>		_ParserFactory	;
				private	readonly	Lazy< IRfcFncController		>		_RfcFncCntlr		;
				//.................................................
				private readonly bool _UseTrnVers;
				private bool _IsReady;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IRfcDestination			RfcDestination	{ get; }
				internal	SMC.RfcDestination	SMCDestination	{ get	{	return	this.RfcDestination.SMCDestination; } }

				private		IUTL_Controller			UTL_Cntlr				{ get { return	NCO_Controller.Instance.UTL_Cntlr	; } }

			#endregion

			//===========================================================================================
			#region "Methods: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	DTO_BDC_SessionConfig								CreateBDCSessionConfig	()										=>	new	DTO_BDC_SessionConfig();
				internal	ProgressHandler<DTO_BDC_Progress>		CreateProgressHandler		( int interval = 10 )	=> this.UTL_Cntlr.CreateProgressHandler<DTO_BDC_Progress>( ()=> new DTO_BDC_Progress() , interval );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<bool> ReadyEnvironmentAsync( bool optimise = true )
					{
						if ( ! this._IsReady )
							{
								this._RfcFncCntlr.Value.RegisterBDCProfile		( this._UseTrnVers );
								this._RfcFncCntlr.Value.RegisterSAPMsgProfile	();
								//.........................................
								try
									{
										await this.RfcDestination.FetchMetadataAsync( optimise )	.ConfigureAwait(false);
										await	this._RfcFncCntlr.Value.ActivateProfilesAsync()			.ConfigureAwait(false);
										this._IsReady		=	true;
									}
								catch (Exception ex)
									{
										throw new Exception( "Session factory ready fail" , ex );
									}
							}
						//.................................................
						return	this._IsReady;
					}

			#endregion

			//===========================================================================================
			#region "Methods: SAP Messages"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	ObjectPool< BDC_Session_SAPMsgProcessor >	CreateSAPMsgsPool()	=>	this.UTL_Cntlr.CreateObjectPool< BDC_Session_SAPMsgProcessor >( this.CreateSAPMsgsProcessor );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session_SAPMsgProcessor > CreateSAPMsgsPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_Session_SAPMsgProcessor >( this.CreateSAPMsgsProcessor , defaults );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_SAPMsgProcessor CreateSAPMsgsProcessor()
					{
						return	new	BDC_Session_SAPMsgProcessor( this.CreateBDCSessionConfig() );
					}

			#endregion

			//===========================================================================================
			#region "Methods: BDC Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	ObjectPool< BDC_Session_TranProcessor >	CreateBDCSessionPool()	=>	this.UTL_Cntlr.CreateObjectPool< BDC_Session_TranProcessor	>( this.CreateBDCSession );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session_TranProcessor > CreateBDCSessionPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_Session_TranProcessor >( this.CreateBDCSession , defaults );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session_TranProcessor CreateBDCSession()
					{
						return	new	BDC_Session_TranProcessor( this.CreateBDCSessionConfig() );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU		();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed );
					}

			#endregion

			//===========================================================================================
			#region "Methods: BDC SAP Message Consumer"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		BDC_Session_SAPMsgConsumer								CreateBDCSAPMsgConsumer			()=>	new	BDC_Session_SAPMsgConsumer	( this._RfcFncCntlr.Value.CreateSAPMsgFunction() );
				internal	ObjectPool< BDC_Session_SAPMsgConsumer >	CreateBDCSAPMsgConsumerPool	()=>	this.UTL_Cntlr.CreateObjectPool< BDC_Session_SAPMsgConsumer >	( this.CreateBDCSAPMsgConsumer );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session_SAPMsgConsumer > CreateBDCSAPMsgConsumerPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_Session_SAPMsgConsumer >( this.CreateBDCSAPMsgConsumer , defaults );
					}

			#endregion

			//===========================================================================================
			#region "Methods: BDC Transaction Consumer"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Session_TranConsumer	CreateBDCTranConsumer	()	=>	new	BDC_Session_TranConsumer( this._RfcFncCntlr.Value.CreateBDCFunction( this._UseTrnVers ) );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPool< BDC_Session_TranConsumer > CreateBDCTransConsumerPool()
					{
								return	this.UTL_Cntlr.CreateObjectPool< BDC_Session_TranConsumer >	( this.CreateBDCTranConsumer );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session_TranConsumer > CreateBDCTransConsumerPoolConfig( bool defaults = true )
					{
								return	ObjectPoolFactory.CreateConfig< BDC_Session_TranConsumer >( this.CreateBDCTranConsumer , defaults );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Parser"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		BDC_Parser								CreateParser			()=>	new	BDC_Parser	( this._ParserFactory );
				internal	ObjectPool< BDC_Parser >	CreateParserPool	()=>	this.UTL_Cntlr.CreateObjectPool< BDC_Parser >	( this.CreateParser		);

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Parser > CreateParserPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_Parser >( this.CreateParser , defaults );
					}

			#endregion

		}
}