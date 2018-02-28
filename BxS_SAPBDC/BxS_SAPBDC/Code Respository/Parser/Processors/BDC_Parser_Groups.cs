using System;
using System.Collections.Generic;
//.........................................................
using					BxS_SAPIPX.Excel;
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Parser_Groups
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Parser_Groups(	Lazy< BDC_Parser_Factory > factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly Lazy< BDC_Parser_Factory > 	_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void	Process(	DTO_BDCSessionRequest		dtoRequest
															,	DTO_ParserProfile				dtoProfile )
					{
						if (dtoRequest.WSData == null)	return;
						//.............................................
						bool	lb_New	= true	;
						int   ln_TNo	= 0			;

						List< int >	lt_GroupRows	= null;
						//.............................................
						for ( int r = dtoProfile.RowDataStart; r <= dtoProfile.RowUB; r++ )
							{
								if ( dtoRequest.WSData[r,dtoProfile.ColExec] != null	)
									{
										if ( !dtoRequest.WSData[r,dtoProfile.ColExec].Equals(string.Empty)	)
											{
												if (lb_New)
													{
														lb_New				= false;
														lt_GroupRows	= new	List<int>();
														ln_TNo	++;
													}

												lt_GroupRows.Add(r);

												if (		dtoProfile.ColPost.Equals(0)
														||	dtoRequest.WSData[r,dtoProfile.ColPost].Contains( cz_Instr_Exec )	)
													{
														dtoProfile.TranRows.Add( ln_TNo, lt_GroupRows );
														lb_New	= true;

														if ( dtoProfile.IsTest )
															{
																break;
															}
													}
											}
									}
							}
					}

			#endregion

		}
}
