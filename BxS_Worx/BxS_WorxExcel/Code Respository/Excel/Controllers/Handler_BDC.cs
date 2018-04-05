using System;
using System.Threading;
//.........................................................
using BxS_WorxIPX.Main;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Helpers;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPExcel.Main
{
	internal class Handler_BDC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Handler_BDC()
					{
						this._IPXSerialiser		=	new	Lazy< Serializer >			(	()=>	Globals.ThisAddIn._IPXCntlr.Value.CreateSerializer()	, cz_LM );
						this._IPXIO						=	new	Lazy< IO >							(	()=>	Globals.ThisAddIn._IPXCntlr.Value.CreateIO()					, cz_LM );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				internal	const LazyThreadSafetyMode	cz_LM		= LazyThreadSafetyMode.ExecutionAndPublication;

				internal readonly Lazy< Serializer >			_IPXSerialiser	;
				internal readonly Lazy< IO >							_IPXIO					;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void WriteDataXML( IExcelBDCSessionWS DTO )
					{
						string lc_XML		=	this._IPXSerialiser.Value.Serialize( DTO );
						this._IPXIO.Value.WriteFile( $@"C:\ProgramData\BxS_Worx\{DTO.WSID}.xml" , lc_XML );
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal DTO_BDCSessionRequest CreateBDCSessionRequest()
				//	{
				//		return	this._IPXCntlr.Value.CreateBDCSessionRequest();
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal DTO_BDCSessionResult CreateBDCSessionResult()
				//	{
				//		return	this._IPXCntlr.Value.CreateBDCSessionResult();
				//	}

			#endregion

			////===========================================================================================
			//#region "Methods: Private"

			//	//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			//	private void Parse2Dto1D( DTO_BDCSessionRequest DTO, bool resetSource = true )
			//		{
			//			DTO.RowLB	= DTO.WSCells.GetLowerBound(0);
			//			DTO.RowUB	= DTO.WSCells.GetUpperBound(0);
			//			DTO.ColLB	= DTO.WSCells.GetLowerBound(1);
			//			DTO.ColUB	= DTO.WSCells.GetUpperBound(1);

			//			DTO.WSData1D.Clear();
			//			//.............................................
			//			for (int r = DTO.RowLB; r <= DTO.RowUB; r++)
			//				{
			//					for (int c = DTO.ColLB; c <= DTO.ColUB; c++)
			//						{
			//							if ( DTO.WSCells[r,c] != null )
			//								{
			//									string lc_Key	= $"{r.ToString()},{c.ToString()}";

			//									DTO.WSData1D.Add( lc_Key , DTO.WSCells[r,c].ToString() );
			//								}
			//						}
			//				}
			//			//.............................................
			//			if ( resetSource )	DTO.WSCells = null;
			//		}

			//#endregion

		}
}
