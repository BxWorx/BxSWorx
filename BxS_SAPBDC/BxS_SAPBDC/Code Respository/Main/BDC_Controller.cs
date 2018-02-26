using System;
using System.Threading;
//.........................................................
using BxS_SAPIPX.Excel;
using BxS_SAPBDC.API	;
using BxS_SAPBDC.Parser;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Main
{
	public class BDC_Controller
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDC_Controller()
					{}

			#endregion

			//===========================================================================================
			#region "Declarations"

				

				private readonly Lazy< BDC_Processor_Tokens >					_Proc_Tkns
						=	new Lazy< BDC_Processor_Tokens >			(		()=> new BDC_Processor_Tokens( ()=> new DTO_TokenReference() )
																											,	LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor_Columns	>				_Proc_Cols
						=	new Lazy< BDC_Processor_Columns >			(		()=> new BDC_Processor_Columns(	()=> new DTO_BDCColumn() )
																											,	LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor_Transaction	>		_Proc_Tran
						=	new Lazy< BDC_Processor_Transaction >	(		()=> new BDC_Processor_Transaction()
																											,	LazyThreadSafetyMode.ExecutionAndPublication );
				//.................................................
				private readonly Lazy< BDC_Processor >								_Proc_Main
						=	new Lazy< BDC_Processor >							(		( )=> new BDC_Processor( ()=> _Proc_Tkns.Value ,
																																								 ()=> _Proc_Cols.Value ,
																																								 ()=> _Proc_Tran.Value	)
																											,	LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IPC_BDCSession	Process( DTO_BDCSessionRequest BDCRequest )
					{
						var lo_BDC	= new BDC_Session();



						return	lo_BDC;
					}

			#endregion

		}
}