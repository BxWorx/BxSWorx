using System;
using System.Threading;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Config;
using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Main
{
	internal class BDC_Factoryx
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDC_Factoryx Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Factoryx()
					{
						this._LazyMode	= LazyThreadSafetyMode.ExecutionAndPublication;
						//.............................................
						this._Proc_Tkns	=	new Lazy< BDC_Parser_Tokens				>	(	()=>	new BDC_Parser_Tokens				( _Instance ) , this._LazyMode );
						this._Proc_Cols	=	new Lazy< BDC_Parser_Columns			>	(	()=>	new BDC_Parser_Columns			(	_Instance ) , this._LazyMode );
						this._Proc_Grps	=	new Lazy< BDC_Parser_Groups				>	(	()=>	new BDC_Parser_Groups				(	_Instance ) , this._LazyMode );
						this._Proc_Tran	=	new Lazy< BDC_Parser_Transaction	>	(	()=>	new BDC_Parser_Transaction	( _Instance ) , this._LazyMode );
						this._Proc_Sesn	=	new Lazy< BDC_Parser_Session			>	(	()=>	new BDC_Parser_Session			( _Instance ) , this._LazyMode );
						this._Proc_Dest	=	new Lazy< BDC_Parser_Destination	>	(	()=>	new BDC_Parser_Destination	( _Instance ) , this._LazyMode );
				}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< BDC_Factory >	_Instance
					= new Lazy< BDC_Factory >(	()=>	new BDC_Factory()
																					, LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly	Lazy< BDC_Parser_Tokens				>	_Proc_Tkns;
				private readonly	Lazy< BDC_Parser_Columns			>	_Proc_Cols;
				private readonly	Lazy< BDC_Parser_Groups				>	_Proc_Grps;
				private readonly	Lazy< BDC_Parser_Transaction	>	_Proc_Tran;
				private readonly	Lazy< BDC_Parser_Session			>	_Proc_Sesn;
				private readonly	Lazy< BDC_Parser_Destination	>	_Proc_Dest;
				//.................................................
				private readonly	LazyThreadSafetyMode	_LazyMode;

			#endregion

			//===========================================================================================
			#region "Properties"

				//internal IIPX_Controller	IPXController		{ get { return IPX_Controller.Instance;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//.................................................
				//.................................................
				// Parser objects
				//.................................................
				internal	DTO_ParserRequest			CreateDTOSessReq	()=>	new DTO_ParserRequest	();
				internal	DTO_ParserColumn			CreateDTOColumn		()=>	new DTO_ParserColumn	();

				internal	DTO_ParserProfile			CreateDTOProfile	()=>	new DTO_ParserProfile( new DTO_ParserHeaderRowRef() )	;

				internal	DTO_ParserToken				CreateDTOToken		( string ID = ""						)=>	new DTO_ParserToken		( ID )					;
				internal	DTO_ParserXMLConfig		CreateDTOXMLCfg		( bool SetDefaults = false	)=>	new DTO_ParserXMLConfig( SetDefaults )	;

				//.................................................
				//.................................................
				// BDC Session objects
				//.................................................
				internal	BDC_Session							CreateBDCSession						()=>	new BDC_Session(	this.CreateDTOSessionOptions()
																																														,	this.CreateDTOSessionHeader()		)	;

				internal	DTO_BDC_Transaction		CreateBDCSessionTransaction	( int ID	= 0 )=>	new DTO_BDC_Transaction( ID )	;

				internal	DTO_SessionHeader			CreateDTOSessionHeader		()=>	new	DTO_SessionHeader		( this.IPXController.CreateCTUParms() )	;
				internal	DTO_SessionOptions		CreateDTOSessionOptions		()=>	new DTO_SessionOptions	()	;
				internal	DTO_BDC_Data				CreateDTOBDCData	()=>	new DTO_BDC_Data	()	;

				internal	IConfigSetupDestination		CreateDestConfig			()=> new ConfigSetupDestination();
				

				//.................................................
				//.................................................
				// Parser routines
				//.................................................
				internal	Lazy< BDC_Parser_Tokens				>	GetTokenParser				()=> this._Proc_Tkns	;
				internal	Lazy< BDC_Parser_Columns			>	GetColumnParser				()=> this._Proc_Cols	;
				internal	Lazy< BDC_Parser_Groups				>	GetGroupParser				()=> this._Proc_Grps	;
				internal	Lazy< BDC_Parser_Transaction	>	GetTransactionParser	()=> this._Proc_Tran	;
				internal	Lazy< BDC_Parser_Session			>	GetSessionParser			()=> this._Proc_Sesn	;
				internal	Lazy< BDC_Parser_Destination	>	GetDestinationParser	()=> this._Proc_Dest	;

			#endregion

		}
}