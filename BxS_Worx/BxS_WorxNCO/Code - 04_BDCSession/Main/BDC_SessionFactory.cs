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
	internal sealed class BDC_SessionFactory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_SessionFactory( IRfcDestination	rfcDestination )
					{
						this.RfcDestination		= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDC_SessionFactory).Namespace}:- RfcDest Factory null" );
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

				internal	ProgressHandler<DTO_BDC_Progress>	CreateProgressHandler( int interval = 10 )	=> this.UTL_Cntlr.CreateProgressHandler<DTO_BDC_Progress>( ()=> new DTO_BDC_Progress() , interval );


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal async Task<bool> ReadyEnvironmentAsync( bool optimise = true )
					{
						if ( ! this._IsReady )
							{
								this._RfcFncCntlr.Value.RegisterBDCCallProfile();
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
				private		BDC_SessionSAPMsgs								CreateSAPMsgsHandler	()=>	new	BDC_SessionSAPMsgs();
				internal	ObjectPool< BDC_SessionSAPMsgs >	CreateSAPMsgsPool			()=>	this.UTL_Cntlr.CreateObjectPool< BDC_SessionSAPMsgs >( this.CreateSAPMsgsHandler );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_SessionSAPMsgs > CreateSAPMsgsPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_SessionSAPMsgs >( this.CreateSAPMsgsHandler , defaults );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

			#endregion

			//===========================================================================================
			#region "Methods: BDC Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	DTO_BDC_SessionConfig				CreateBDCSessionConfig	()=>	new	DTO_BDC_SessionConfig();
				internal	ObjectPool< BDC_Session >		CreateBDCSessionPool		()=>	this.UTL_Cntlr.CreateObjectPool< BDC_Session	>( this.CreateBDCSession );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session > CreateBDCSessionPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_Session >( this.CreateBDCSession , defaults );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Session CreateBDCSession()
					{
						BDCCall_Header lo_H	= this._RfcFncCntlr.Value.GetAddBDCCallProfile().CreateBDCCallHeader();
						return	new	BDC_Session( lo_H , this.CreateBDCSessionConfig() );
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
			#region "Methods: BDC Consumer"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		BDC_SessionConsumer								CreateBDCConsumer			()=>	new	BDC_SessionConsumer	( this._RfcFncCntlr.Value.CreateBDCCallFunction() );
				internal	ObjectPool< BDC_SessionConsumer >	CreateBDCConsumerPool	()=>	this.UTL_Cntlr.CreateObjectPool< BDC_SessionConsumer >	( this.CreateBDCConsumer );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_SessionConsumer > CreateBDCConsumerPoolConfig( bool defaults = true )
					{
						return	ObjectPoolFactory.CreateConfig< BDC_SessionConsumer >( this.CreateBDCConsumer , defaults );
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