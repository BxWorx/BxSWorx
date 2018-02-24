using System;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPIPX.BDCData;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public class BDC_Processor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor(		BDC_Processor_Tokens						processor_Tokens
																,	BDC_Processor_Columns						processor_Columns
																,	Func< DTO_BDCHeaderRowRef		>		createRowRef
																,	Func<		DTO_BDCHeaderRowRef
																				, DTO_BDCSession			>		createSession )
					{
						this._Process_Tokens	= processor_Tokens	;
						this._Process_Columns	= processor_Columns	;
						//.............................................
						this._CreateRowRef		= createRowRef	;
						this._CreateSession		= createSession	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly BDC_Processor_Tokens		_Process_Tokens;
				private readonly BDC_Processor_Columns	_Process_Columns;
				//.................................................
				private readonly	Func<	DTO_BDCHeaderRowRef>										_CreateRowRef		;
				private readonly	Func<	DTO_BDCHeaderRowRef , DTO_BDCSession>		_CreateSession	;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task< IPC_BDCSession > Process( DTO_ExcelWorksheet wsDTO )
					{
						DTO_BDCSession	lo_BDCSession	= this._CreateSession( this._CreateRowRef() );
						IPC_BDCSession	lo_IPCSession	= IPC_Controller.CreateSessionDTO();
						//.............................................
						if ( await this._Process_Tokens.Process( lo_BDCSession , wsDTO.WSData ).ConfigureAwait(false) )
							{
								if ( this._Process_Columns.Process( lo_BDCSession , wsDTO.WSData ) )
									{
									}
							}
						else
							{	return	null; }
						//.............................................
						return	lo_IPCSession;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

			#endregion

		}
}
