using System;
using System.Threading;
//.........................................................
using BxS_SAPIPX.Main;
using BxS_SAPBDC.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public class BDC_Processor_Cfg
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static BDC_Processor_Cfg Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Processor_Cfg()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< BDC_Processor_Cfg >		_Instance		= new Lazy< BDC_Processor_Cfg >
					(		()=>	new BDC_Processor_Cfg()
						, LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor_Tokens >				_Proc_Tkns	=	new Lazy< BDC_Processor_Tokens >
					(		()=>	new BDC_Processor_Tokens( _Instance )
						,	LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor_Columns >			_Proc_Cols	=	new Lazy< BDC_Processor_Columns >
					(		()=> new BDC_Processor_Columns(	_Instance.Value )
						,	LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor_Transaction >	_Proc_Tran	=	new Lazy< BDC_Processor_Transaction >
					(		()=>	new BDC_Processor_Transaction( _Instance.Value )
						,	LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal IIPX_Controller	IPXController		{ get { return IPX_Controller.Instance;	} }
				//.................................................
				//internal	BDC_Processor_Tokens				TokenProcessor				{ get { return	this._Proc_Tkns.Value	; } }
				internal	BDC_Processor_Columns				ColumnProcessor				{ get { return	this._Proc_Cols.Value	; } }
				internal	BDC_Processor_Transaction		TransactionProcessor	{ get { return	this._Proc_Tran.Value	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				internal	BDC_Session		CreateBDCSession	()=>	new BDC_Session()	;
				//.................................................
				internal	DTO_BDCSession			CreateDTOSession	()=>	new DTO_BDCSession( new DTO_BDCHeaderRowRef() )	;
				internal	DTO_TokenReference	CreateDTOToken		()=>	new DTO_TokenReference()												;
				internal	DTO_BDCColumn				CreateDTOColumn		()=>	new DTO_BDCColumn()															;
				internal	DTO_BDCXMLConfig		CreateDTOXMLCfg		()=>	new DTO_BDCXMLConfig()													;
				//.................................................
				internal	DTO_SessionTranData		CreateDTOTranData	()=>	new DTO_SessionTranData()	;

			#endregion

		}
}