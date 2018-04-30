using System;
//.........................................................
using BxS_WorxNCO.API;

using BxS_WorxNCO.BDCSession.DTO			;
using BxS_WorxNCO.BDCSession.Parser		;

using BxS_WorxUtil.Main;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	internal sealed class BDC_Session_Factory
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Session_Factory()
					{
						//this._ParserFactory		= new Lazy< BDC_Parser_Factory	>		(	()=>	BDC_Parser_Factory.Instance	, cz_LM	);
					}
				//.................................................
				private	static readonly		Lazy< BDC_Session_Factory >	_Instance		= new	Lazy< BDC_Session_Factory >( ()=> new BDC_Session_Factory() , cz_LM );
				public	static						BDC_Session_Factory					Instance			{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private readonly	Lazy< BDC_Parser_Factory >	_ParserFactory	;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	IUTL_Controller		UTL_Cntlr		{ get { return	NCO_Controller.Instance.UTL_Cntlr	; } }

			#endregion

			//===========================================================================================
			#region "Methods: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_SessionConfig	CreateBDCSessionConfig()	=>	new	DTO_BDC_SessionConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ProgressHandler<DTO_BDC_Progress> CreateProgressHandler( int interval = 10 )
					{
						return	this.UTL_Cntlr.CreateProgressHandler( ()=> new DTO_BDC_Progress() , interval );
					}

			#endregion

			////===========================================================================================
			//#region "Methods: SAP Messages"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal	ObjectPool< BDC_Session_SAPMsgProcessor >	CreateSAPMsgsPool()	=>	this.UTL_Cntlr.CreateObjectPool( this.CreateSAPMsgsProcessor );

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal ObjectPoolConfig< BDC_Session_SAPMsgProcessor > CreateSAPMsgsPoolConfig( bool defaults = true )
			//		{
			//			return	ObjectPoolFactory.CreateConfig( this.CreateSAPMsgsProcessor , defaults );
			//		}

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal BDC_Session_SAPMsgProcessor CreateSAPMsgsProcessor()
			//		{
			//			return	new	BDC_Session_SAPMsgProcessor( this.CreateBDCSessionConfig() );
			//		}

			//#endregion

			//===========================================================================================
			#region "Methods: BDC Session"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal	ObjectPool< BDC_Session_TranProcessor >	CreateBDCSessionPool()	=>	this.UTL_Cntlr.CreateObjectPool( this.CreateBDCSession );

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal ObjectPoolConfig< BDC_Session_TranProcessor > CreateBDCSessionPoolConfig( bool defaults = true )
				//	{
				//		return	ObjectPoolFactory.CreateConfig( this.CreateBDCSession , defaults );
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal BDC_Session_TranProcessor CreateBDCSession()
				//	{
				//		return	new	BDC_Session_TranProcessor( this.CreateBDCSessionConfig() );
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Session CreateSessionDTO()
					{
						var lo_CTU	= new DTO_BDC_CTU		();
						var lo_Hed	= new DTO_BDC_Header( lo_CTU );
						//.............................................
						return	new DTO_BDC_Session( lo_Hed , this.CreateBDCTransaction );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	DTO_BDC_Transaction	CreateBDCTransaction( int No )	=>	new DTO_BDC_Transaction( No );

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal DTO_BDC_Request CreateRequestDTO()
				//	{
				//		return	new DTO_BDC_Request( this.CreateSessionDTO );
				//	}


			#endregion

			////===========================================================================================
			//#region "Methods: BDC SAP Message Consumer"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal	ObjectPool< BDC_Session_SAPMsgConsumer >	CreateBDCSAPMsgConsumerPool( Func< BDC_Session_SAPMsgConsumer > factory )
			//		{
			//			return	this.UTL_Cntlr.CreateObjectPool( factory );
			//		}

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	internal ObjectPoolConfig< BDC_Session_SAPMsgConsumer > CreateBDCSAPMsgConsumerPoolConfig(	Func< BDC_Session_SAPMsgConsumer >	factory
			//																																														, bool																defaults = true )
			//		{
			//			return	ObjectPoolFactory.CreateConfig( factory , defaults );
			//		}

			//#endregion

			//===========================================================================================
			#region "Methods: BDC Transaction Consumer"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal ObjectPool< BDC_Session_TranConsumer > CreateBDCTransConsumerPool( Func< BDC_Session_TranConsumer > factory )
				//	{
				//		return	this.UTL_Cntlr.CreateObjectPool( factory );
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal ObjectPoolConfig< BDC_Session_TranConsumer > CreateBDCTransConsumerPoolConfig(		Func< BDC_Session_TranConsumer >	factory
				//																																												,	bool															defaults = true )
				//	{
				//		return	ObjectPoolFactory.CreateConfig( factory , defaults );
				//	}

			#endregion

			//===========================================================================================
			#region "Methods: Parser"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private		BDC_Parser								CreateParser			()=>	new	BDC_Parser	( this._ParserFactory );
				//internal	ObjectPool< BDC_Parser >	CreateParserPool	()=>	this.UTL_Cntlr.CreateObjectPool( this.CreateParser		);

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal ObjectPoolConfig< BDC_Parser > CreateParserPoolConfig( bool defaults = true )
				//	{
				//		return	ObjectPoolFactory.CreateConfig( this.CreateParser , defaults );
				//	}

			#endregion

		}
}