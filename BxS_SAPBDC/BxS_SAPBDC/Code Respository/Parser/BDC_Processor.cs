using System;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPBDC.BDC;
using BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public class BDC_Processor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor(	Lazy< BDC_Processor_Factory >	factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< BDC_Processor_Factory >		_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task< BDC_Session > Process( DTO_BDCSessionRequest DTORequest )
					{
						BDC_Processor_Tokens			lo_Tkn	= this._Factory.Value.GetTokenProcessor().Value				;
						BDC_Processor_Columns			lo_Col	= this._Factory.Value.GetColumnProcessor().Value			;
						BDC_Processor_Groups			lo_Grp	= this._Factory.Value.GetGroupProcessor().Value				;
						BDC_Processor_Transaction	lo_Trn	= this._Factory.Value.GetTransactionProcessor().Value	;
						//.............................................
						DTO_BDCSession	lo_DTOSession	= this._Factory.Value.CreateDTOSession();

						if ( await lo_Tkn.Process( lo_DTOSession , DTORequest.WSData ).ConfigureAwait(false) )
							{
								if ( lo_Col.Process( lo_DTOSession , DTORequest.WSData ) )
									{
										if ( !lo_Grp.Process( lo_DTOSession, DTORequest.WSData ).Equals(0) )
											{
												return	await	lo_Trn.Process( lo_DTOSession, DTORequest.WSData ).ConfigureAwait(false);
											}
									}
							}
						//.............................................
						return	null;
					}

			#endregion

		}
}
