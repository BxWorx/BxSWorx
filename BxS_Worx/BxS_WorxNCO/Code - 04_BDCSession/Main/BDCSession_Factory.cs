using System;
//.........................................................
using BxS_WorxNCO.Destination.API			;

using BxS_WorxNCO.BDCSession.DTO			;
using BxS_WorxNCO.BDCSession.Parser		;

using BxS_WorxNCO.RfcFunction.Main		;
using BxS_WorxNCO.RfcFunction.BDCTran	;

using BxS_WorxNCO.Helpers.ObjectPool	;
using BxS_WorxNCO.Helpers.Progress		;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal sealed class BDCSession_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession_Factory( IRfcDestination	rfcDestination )
					{
						this._RfcDest		= rfcDestination	??	throw		new	ArgumentException( $"{typeof(BDCSession_Factory).Namespace}:- RfcDest Factory null" );
						//.................................................
						this._ParserFactory		= new Lazy< BDC_Parser_Factory	>		(	()=>	BDC_Parser_Factory.Instance , cz_LM			);
						this._RfcFncCntlr			= new	Lazy< IRfcFncController		>		(	()=>	new	RfcFncController( this._RfcDest ) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< BDC_Parser_Factory >	_ParserFactory	;
				private	readonly	Lazy< IRfcFncController >		_RfcFncCntlr		;

				private	readonly	IRfcDestination		_RfcDest;

			#endregion

			//===========================================================================================
			#region "Methods: BDC Session"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	DTO_BDC_SessionConfig									CreateBDCSessionConfig					()=>	new	DTO_BDC_SessionConfig();
				internal	ObjectPool< BDC_Session >							CreateBDCSessionPool						()=>	new ObjectPool< BDC_Session	>( this.CreateBDCSession );
				internal	ProgressHandler< DTO_BDC_Progress >		CreateBDCSessionProgressHandler	()=>	new ProgressHandler< DTO_BDC_Progress >( this.CreateProgress );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Session > CreateBDCSessionPoolConfig( bool defaults = true )
					{
						ObjectPoolConfig< BDC_Session >	lo_Cfg	=	ObjectPoolFactory.CreateConfig< BDC_Session >();
						//.............................................
						if ( defaults )
							{
								lo_Cfg.Factory	= this.CreateBDCSession;
							}
						//.............................................
						return	lo_Cfg;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Session CreateBDCSession()
					{
						BDCCall_Header lo_H	= this._RfcFncCntlr.Value.GetAddBDCCallProfile().CreateBDCCallHeader();
						return	new	BDC_Session( lo_H , this.CreateBDCSessionConfig() );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed );
					}

			#endregion

			//===========================================================================================
			#region "Methods: BDC Consumer"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		BDCSessionConsumer								CreateBDCConsumer			()=>	new	BDCSessionConsumer								( this._RfcFncCntlr.Value.CreateBDCCallFunction() );
				internal	ObjectPool< BDCSessionConsumer >	CreateBDCConsumerPool	()=>	new ObjectPool< BDCSessionConsumer >	( this.CreateBDCConsumer );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDCSessionConsumer > CreateBDCConsumerPoolConfig( bool defaults = true )
					{
						ObjectPoolConfig< BDCSessionConsumer >	lo_Cfg	=	ObjectPoolFactory.CreateConfig< BDCSessionConsumer >();
						//.............................................
						if ( defaults )
							{
								lo_Cfg.Factory	= this.CreateBDCConsumer;
							}
						//.............................................
						return	lo_Cfg;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Parser"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private		BDC_Parser								CreateParser			()=>	new	BDC_Parser								( this._ParserFactory );
				internal	ObjectPool< BDC_Parser >	CreateParserPool	()=>	new ObjectPool< BDC_Parser >	( this.CreateParser		);

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ObjectPoolConfig< BDC_Parser > CreateParserPoolConfig( bool defaults = true )
					{
						ObjectPoolConfig< BDC_Parser >	lo_Cfg	=	ObjectPoolFactory.CreateConfig< BDC_Parser >();
						//.............................................
						if ( defaults )
							{
								lo_Cfg.Factory	= this.CreateParser;
							}
						//.............................................
						return	lo_Cfg;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Progress"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	DTO_BDC_Progress CreateProgress	()=>	new	DTO_BDC_Progress();

			#endregion

		}
}