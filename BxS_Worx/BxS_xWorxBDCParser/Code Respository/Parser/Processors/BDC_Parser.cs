using System;
//.........................................................
using BxS_SAPBDC.BDC;
using BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public class BDC_Parser
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Parser( Lazy< BDC_Parser_Factory >	factory )
					{
						this._Factory	= factory;
						//.............................................
						this._Tkn	= factory.Value.GetTokenParser()				;
						this._Col	= factory.Value.GetColumnParser()				;
						this._Grp	= factory.Value.GetGroupParser()				;
						this._Trn	= factory.Value.GetTransactionParser()	;
						this._Ssn	= factory.Value.GetSessionParser()			;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< BDC_Parser_Factory >			_Factory;
				//.................................................
				private	readonly	Lazy< BDC_Parser_Tokens >				_Tkn;
				private	readonly	Lazy< BDC_Parser_Columns >			_Col;
				private	readonly	Lazy< BDC_Parser_Groups >				_Grp;
				private	readonly	Lazy< BDC_Parser_Transaction >	_Trn;
				private	readonly	Lazy< BDC_Parser_Session >			_Ssn;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDC_Session Process( DTO_BDCSessionRequest DTORequest )
					{
						BDC_Session				lo_BDCSession		= this._Factory.Value.CreateBDCSession();
						DTO_ParserProfile	lo_DTOProfile		= this._Factory.Value.CreateDTOProfile();
						//.............................................
						this._Tkn.Value.Process( DTORequest	,	lo_DTOProfile	);
						this._Col.Value.Process( DTORequest	,	lo_DTOProfile	);
						this._Grp.Value.Process( DTORequest	,	lo_DTOProfile	);
						this._Trn.Value.Process( DTORequest	, lo_DTOProfile	, lo_BDCSession );
						this._Ssn.Value.Process( DTORequest	, lo_DTOProfile	, lo_BDCSession );
						//.............................................
						return	lo_BDCSession;
					}

			#endregion

		}
}
