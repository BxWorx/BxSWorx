using System;
using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.BDC;

using BxS_WorxNCO.BDCSession.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Parser
{
	internal class BDC_Parser : BDC_Parser_Base
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Parser( Lazy< BDC_Parser_Factory >	factory ) : base( factory )
					{
						this._Tkn		=	this._PFactory.Value.GetTokenParser				();
						this._Col		= this._PFactory.Value.GetColumnParser			();
						this._Grp		= this._PFactory.Value.GetGroupParser				();
						this._Trn		= this._PFactory.Value.GetTransactionParser	();
						this._Ssn		= this._PFactory.Value.GetSessionParser			();
						this._Des		= this._PFactory.Value.GetDestinationParser	();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< BDC_Parser_Tokens				>	_Tkn;
				private	readonly	Lazy< BDC_Parser_Columns			>	_Col;
				private	readonly	Lazy< BDC_Parser_Groups				>	_Grp;
				private	readonly	Lazy< BDC_Parser_Transaction	>	_Trn;
				private	readonly	Lazy< BDC_Parser_Session			>	_Ssn;
				private	readonly	Lazy< BDC_Parser_Destination	>	_Des;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool Parse(	IExcelBDCSessionRequest	excelBDCRequest
														, DTO_BDC_Session					dto_BDCSession		)
					{
						DTO_ParserRequest	lo_DTOSessReq	= this._PFactory.Value.CreateDTOSessReq();

						if ( ! this.Parse1Dto2D( excelBDCRequest , lo_DTOSessReq ) )
							{
								return	false;
							}
						//.............................................
						DTO_ParserProfile	lo_DTOProfile		= this._PFactory.Value.CreateDTOProfile();
						//.............................................
						this._Tkn.Value.Process( lo_DTOSessReq	,	lo_DTOProfile	);
						this._Col.Value.Process( lo_DTOSessReq	,	lo_DTOProfile	);
						this._Grp.Value.Process( lo_DTOSessReq	,	lo_DTOProfile	);

						this._Trn.Value.Process( lo_DTOSessReq	, lo_DTOProfile	, dto_BDCSession );
						//.............................................
						if ( ! excelBDCRequest.IgnoreSessionConfig )
							{
								this._Ssn.Value.Process( lo_DTOProfile , dto_BDCSession.Header );
							}
						//.............................................
						if ( ! excelBDCRequest.IgnoreDestinationConfig )
							{
								this._Des.Value.Process( excelBDCRequest , dto_BDCSession.DestConfig );
							}
						//.............................................
						return	true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool Parse1Dto2D(		IExcelBDCSessionRequest request
																	, DTO_ParserRequest				dto			)
					{
						bool	lb_Ret	= true;
						//.............................................
						if ( request.WSData1D.Count.Equals(0) )
							{
								lb_Ret	= false;
							}
						else
							{
								int[]	lt_UB	= new int[2];
								int[]	lt_LB = new int[2];

								lt_UB[0]	=	request.RowUB;
								lt_UB[1]	=	request.ColUB;
								lt_LB[0]	=	request.RowLB;
								lt_LB[1]	=	request.ColLB;

								dto.WSData	= ( string[,] ) Array.CreateInstance( typeof( string ) , lt_UB, lt_LB );
								//.............................................
								int	ln_Row	= 0;
								int ln_Col	= 0;

								foreach ( KeyValuePair<string, string> ls_kvp in request.WSData1D )
									{
										string[] lt_Idx = ls_kvp.Key.Split(',');

										ln_Row	= int.Parse(lt_Idx[0]);
										ln_Col	= int.Parse(lt_Idx[1]);

										dto.WSData[ln_Row,ln_Col]	= ls_kvp.Value;
									}
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}
