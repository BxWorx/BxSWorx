//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
using System.Linq;

namespace BxS_SAPNCO.API.SAPFunctions
{
	public class BDCCTU_Parameters
		{
			#region "Documentation"

				//	CTU_PARAMS:	Parameter string for runtime of CALL TRANSACTION USING...
				//
				//	DISMODE		1 Types	CTU_MODE		CHAR	1	0	Processing mode for CALL TRANSACTION USING...
				//	UPDMODE		1 Types	CTU_UPDATE	CHAR	1	0	Update mode for CALL TRANSACTION USING...
				//	CATTMODE	1 Types	CTU_CATT		CHAR	1	0	CATT mode for CALL TRANSACTION USING...
				//	DEFSIZE		1 Types	CTU_DEFSZE	CHAR	1	0	Default screen size for CALL TRANSACTION USING...
				//	RACOMMIT	1 Types	CTU_RAFC		CHAR	1	0	CALL TRANSACTION USING... is not completed by COMMIT
				//	NOBINPT		1 Types	CTU_NOBIM		CHAR	1	0	SY-BINPT=SPACE for CALL TRANSACTION USING...
				//	NOBIEND		1 Types	CTU_NOBEN		CHAR	1	0	SY-BINPT=SPACE after data end for CALL TRANSACTION USING...

				//	CTU_MODE:	A	Display all screens
				//						E	Display Errors
				//						N	Background processing
				//						P	Background processing; debugging possible

				//	CTU_UPDATE:	L	Local
				//							S	Synchronous
				//							A	Asynchronous

				//	CTU_CATT:	b	No CATT
				//						N	CATT without individual screen control
				//						A	CATT with individual screen control

				//	CTU_DEFSZE/CTU_RAFC/CTU_NOBIM/CTU_NOBEN:	b	No
				//																						X	Yes

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCTU_Parameters(	char	DisplayMode	= BDCConstants.lz_CTU_N )
					{
						this.DisplayMode	=	this.CheckDispMde(DisplayMode);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	char	_DispMode;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	char		DisplayMode		{		get { return	this._DispMode; }		
																					set	{ this._DispMode = this.CheckDispMde(value); }	}
				//.................................................
				public	string	UpdateMode		{ get; set; }
				public	string	CattMode			{ get; set; }
				public	string	DefSize				{ get; set; }
				public	string	NoCommit			{ get; set; }
				public	string	BatchInputFor	{ get; set; }
				public	string	BatchInputAft	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckDispMde(char checkFor)
					{
						return	this.Check( BDCConstants.LZ_CTU_DisMde, checkFor, BDCConstants.lz_CTU_N);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char Check(string valid, char checkfor, char defvalue)
					{
						return	valid.Contains(checkfor)	? checkfor	: defvalue	;
					}

			#endregion

		}
}
