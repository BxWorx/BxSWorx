using System;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPBDC.BDC;
using BxS_SAPBDC.Main;
using BxS_SAPIPX.Excel;
using BxS_SAPIPX.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public class BDC_Processor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor(	BDC_Processor_Cfg	BDCConfig )
					{
						this._BDCCnfg	= BDCConfig;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly BDC_Processor_Cfg	_BDCCnfg;

			#endregion

			//===========================================================================================
			#region "Properties"

				private IIPX_Controller						_IPX { get { return	this._BDCCnfg.IPXController					; } }
				//.................................................
				private BDC_Processor_Tokens			_Tkn { get { return	this._BDCCnfg.TokenProcessor				; } }
				private BDC_Processor_Columns			_Col { get { return	this._BDCCnfg.ColumnProcessor				; } }
				private BDC_Processor_Transaction	_Trn { get { return	this._BDCCnfg.TransactionProcessor	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task< BDC_Session > Process( DTO_BDCSessionRequest DTORequest )
					{
						BDC_Session			lo_BDCSession	= this._BDCCnfg.CreateBDCSession();
						//.............................................
						DTO_BDCSession	lo_DTOSession	= this._BDCCnfg.CreateDTOSession();

						if ( await this._Tkn.Process( lo_DTOSession , DTORequest.WSData ).ConfigureAwait(false) )
							{
								if ( this._Col.Process( lo_DTOSession , DTORequest.WSData ) )
									{
										int x = await	this._Trn.Process( lo_DTOSession, DTORequest.WSData ).ConfigureAwait(false);

										if (x.Equals(0))
											{

											}

									}
							}
						else
							{	return	null; }
						//.............................................
						return	lo_BDCSession;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

			#endregion

		}
}
