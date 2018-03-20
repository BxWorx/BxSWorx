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
						this._Tkn		= factory.Value.GetTokenParser				();
						this._Col		= factory.Value.GetColumnParser				();
						this._Grp		= factory.Value.GetGroupParser				();
						this._Trn		= factory.Value.GetTransactionParser	();
						this._Ssn		= factory.Value.GetSessionParser			();
						this._Des		= factory.Value.GetDestinationParser	();
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
				internal DTO_BDC_Session Process( IBDCSessionRequest bdcSessionRequest )
					{
						DTO_ParserRequest	lo_DTOSessReq	= this.Parse1Dto2D( bdcSessionRequest );
						//.............................................
						DTO_BDC_Session		lo_BDCSession		= this._Factory.Value.CreateBDCSession();
						DTO_ParserProfile	lo_DTOProfile		= this._Factory.Value.CreateDTOProfile();
						//.............................................
						this._Tkn.Value.Process( lo_DTOSessReq	,	lo_DTOProfile	);
						this._Col.Value.Process( lo_DTOSessReq	,	lo_DTOProfile	);
						this._Grp.Value.Process( lo_DTOSessReq	,	lo_DTOProfile	);

						this._Trn.Value.Process( lo_DTOSessReq	, lo_DTOProfile	, lo_BDCSession );

						this._Ssn.Value.Process( lo_DTOProfile			, lo_BDCSession );
						this._Des.Value.Process( bdcSessionRequest	, lo_BDCSession );
						//.............................................
						return	lo_BDCSession;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTO_ParserRequest Parse1Dto2D( IBDCSessionRequest DTO )
					{
						DTO_ParserRequest	lo_SessReq	= this._Factory.Value.CreateDTOSessReq();
						//.............................................
						int[]	lt_UB = new int[2];
						int[]	lt_LB = new int[2];

						lt_UB[0] =	DTO.RowUB;
						lt_UB[1] =	DTO.ColUB;
						lt_LB[0] =	DTO.RowLB;
						lt_LB[1] =	DTO.ColLB;

						lo_SessReq.WSData	= ( string[,] ) Array.CreateInstance( typeof( string ) , lt_UB, lt_LB );
						//.............................................
						int	ln_Row	= 0;
						int ln_Col	= 0;

						foreach ( KeyValuePair<string, string> ls_kvp in DTO.WSData1D )
							{
								string[] lt_Idx = ls_kvp.Key.Split(',');

								ln_Row	= int.Parse(lt_Idx[0]);
								ln_Col	= int.Parse(lt_Idx[1]);

								lo_SessReq.WSData[ln_Row,ln_Col]	= ls_kvp.Value;
							}
						//.............................................
						return	lo_SessReq;
					}

			#endregion

		}
}
