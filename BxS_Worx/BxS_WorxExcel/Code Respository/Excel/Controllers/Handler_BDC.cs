using System;
using System.Threading;
//.........................................................
using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Main
{
	internal class Handler_BDC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Handler_BDC()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal readonly Lazy< IIPXController >

					_IPXCntlr		= new Lazy< IIPXController >	( ()=>	IPXController.Instance
																													, LazyThreadSafetyMode
																															.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteDataXML( IExcelBDCSessionRequest DTO )
					{
						this.Parse2Dto1D( DTO );
						string lc_XML		=	this._IPXCntlr.Value.Serialize( DTO );
						this._IPXCntlr.Value.WriteFile( $@"C:\ProgramData\BxS_Worx\{DTO.WSID}.xml" , lc_XML );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionRequest CreateBDCSessionRequest()
					{
						return	this._IPXCntlr.Value.CreateBDCSessionRequest();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionResult CreateBDCSessionResult()
					{
						return	this._IPXCntlr.Value.CreateBDCSessionResult();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void Parse2Dto1D( DTO_BDCSessionRequest DTO, bool resetSource = true )
					{
						DTO.RowLB	= DTO.WSCells.GetLowerBound(0);
						DTO.RowUB	= DTO.WSCells.GetUpperBound(0);
						DTO.ColLB	= DTO.WSCells.GetLowerBound(1);
						DTO.ColUB	= DTO.WSCells.GetUpperBound(1);

						DTO.WSData1D.Clear();
						//.............................................
						for (int r = DTO.RowLB; r <= DTO.RowUB; r++)
							{
								for (int c = DTO.ColLB; c <= DTO.ColUB; c++)
									{
										if ( DTO.WSCells[r,c] != null )
											{
												string lc_Key	= $"{r.ToString()},{c.ToString()}";

												DTO.WSData1D.Add( lc_Key , DTO.WSCells[r,c].ToString() );
											}
									}
							}
						//.............................................
						if ( resetSource )	DTO.WSCells = null;
					}

			#endregion

		}
}
