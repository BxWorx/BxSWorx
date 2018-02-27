using System;
using System.Threading;
//.........................................................
using BxS_SAPBDC.BDC;
using BxS_SAPIPX.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Processor_Factory
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal static BDC_Processor_Factory Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private BDC_Processor_Factory()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< BDC_Processor_Factory >		_Instance		= new Lazy< BDC_Processor_Factory >
					(		()=>	new BDC_Processor_Factory()
						, LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor_Tokens >				_Proc_Tkns	=	new Lazy< BDC_Processor_Tokens >
					(		()=>	new BDC_Processor_Tokens( _Instance )
						,	LazyThreadSafetyMode.ExecutionAndPublication );

				private readonly Lazy< BDC_Processor_Columns >			_Proc_Cols	=	new Lazy< BDC_Processor_Columns >
					(		()=> new BDC_Processor_Columns(	_Instance )
						,	LazyThreadSafetyMode.ExecutionAndPublication );

				private readonly Lazy< BDC_Processor_Groups >				_Proc_Grps	=	new Lazy< BDC_Processor_Groups >
					(		()=> new BDC_Processor_Groups(	_Instance )
						,	LazyThreadSafetyMode.ExecutionAndPublication );

				private readonly Lazy< BDC_Processor_Transaction >	_Proc_Tran	=	new Lazy< BDC_Processor_Transaction >
					(		()=>	new BDC_Processor_Transaction( _Instance )
						,	LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal IIPX_Controller	IPXController		{ get { return IPX_Controller.Instance;	} }

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
				//.................................................
				internal	Lazy< BDC_Processor_Tokens >				GetTokenProcessor				()=> this._Proc_Tkns	;
				internal	Lazy< BDC_Processor_Columns >				GetColumnProcessor			()=> this._Proc_Cols	;
				internal	Lazy< BDC_Processor_Groups >				GetGroupProcessor				()=> this._Proc_Grps	;
				internal	Lazy< BDC_Processor_Transaction >		GetTransactionProcessor	()=> this._Proc_Tran	;

			#endregion

		}
}