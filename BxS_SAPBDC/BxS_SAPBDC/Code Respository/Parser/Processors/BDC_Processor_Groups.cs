using System;
using System.Collections.Generic;
//.........................................................
using					BxS_SAPBDC.BDC;
using static	BxS_SAPBDC.BDC.BDC_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal class BDC_Processor_Groups
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	BDC_Processor_Groups(	Lazy< BDC_Processor_Factory > factory )
					{
						this._Factory	= factory;
					}

			#endregion

			//===========================================================================================
			#region "Declaration"

				private	readonly Lazy< BDC_Processor_Factory > 	_Factory;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Columns"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal int Process( DTO_BDCProfile dto , string[,] data )
					{
						bool	lb_New	= true	;
						int   ln_TNo	= 0			;

						List< int >	lt_GroupRows	= null;
						//.............................................
						for ( int r = dto.RowDataStart; r < dto.RowUB; r++ )
							{
								if ( !data[r,dto.ColDataExec].Equals(string.Empty)	)
									{
										if (lb_New)
											{
												lt_GroupRows	= new	List<int>();
												ln_TNo	++;
												lb_New	= false;
											}

										lt_GroupRows.Add(r);

										if (		dto.ColDataExec.Equals(0)
												||	data[r,dto.ColDataPost].Contains( cz_Instr_Exec )	)
											{
												dto.TranRows.Add( ln_TNo, lt_GroupRows );
												lb_New	= true;

												if ( dto.IsTest )
													{
														break;
													}
											}
									}
							}
						//.............................................
						return	ln_TNo;
					}

			#endregion

		}
}
