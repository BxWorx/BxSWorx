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
				public BDCCTU_Parameters(	char	DisplayMode		= BDCConstants.lz_CTU_N	,
																	char	UpdateMode		= BDCConstants.lz_CTU_S	,
																	char	CATTMode			= BDCConstants.lz_CTU__	,
																	char	DefaultSize		= BDCConstants.lz_CTU_Y	,
																	char	NoCommit   		= BDCConstants.lz_CTU__	,
																	char	BatchInputFor	= BDCConstants.lz_CTU__	,
																	char	BatchInputAft	= BDCConstants.lz_CTU__		)
					{
						this.DisplayMode	=	this.CheckDspMde	(DisplayMode)		;
						this.UpdateMode		=	this.CheckUpdMde	(UpdateMode)		;
						this.CATTMode			=	this.CheckCatMde	(CATTMode)			;
						this.DefaultSize	=	this.CheckYesNo		(DefaultSize)		;
						this.NoCommit			=	this.CheckYesNo		(NoCommit)			;
						this.DisplayMode	=	this.CheckYesNo		(BatchInputFor)	;
						this.DisplayMode	=	this.CheckYesNo		(BatchInputAft)	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	char	_DspMde	;
				private	char	_UpdMde	;
				private	char	_CatMde	;
				private	char	_DefSze	;
				private	char	_NoComm	;
				private	char	_NoBInp	;
				private	char	_NoBEnd	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	char		DisplayMode		{		get { return	this._DspMde; }
																					set	{ this._DspMde	= this.CheckDspMde	(value); }	}
				//.................................................
				public	char		UpdateMode		{		get { return	this._UpdMde; }
																					set	{ this._UpdMde	= this.CheckUpdMde	(value); }	}
				//.................................................
				public	char		CATTMode			{		get { return	this._CatMde; }
																					set	{ this._CatMde	= this.CheckCatMde	(value); }	}
				//.................................................
				public	char		DefaultSize		{		get { return	this._DefSze; }
																					set	{ this._DefSze	= this.CheckYesNo		(value); }	}
				//.................................................
				public	char		NoCommit			{		get { return	this._NoComm; }
																					set	{ this._NoComm	= this.CheckYesNo		(value); }	}
				//.................................................
				public	char		NoBatchInpFor		{		get { return	this._NoBInp; }
																					set	{ this._NoBInp	= this.CheckYesNo		(value); }	}
				//.................................................
				public	char		NoBatchInpAft		{		get { return	this._NoBEnd; }
																					set	{ this._NoBEnd	= this.CheckYesNo		(value); }	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckYesNo(char checkFor)
					{
						return	this.Check( BDCConstants.LZ_CTU_YesNo	, checkFor, BDCConstants.lz_CTU__ );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckDspMde(char checkFor)
					{
						return	this.Check( BDCConstants.LZ_CTU_DisMde, checkFor, BDCConstants.lz_CTU_N );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckUpdMde(char checkFor)
					{
						return	this.Check( BDCConstants.LZ_CTU_UpdMde, checkFor, BDCConstants.lz_CTU_S );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckCatMde(char checkFor)
					{
						return	this.Check( BDCConstants.LZ_CTU_CatMde, checkFor, BDCConstants.lz_CTU__ );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char Check(string valid, char checkfor, char defvalue)
					{
						return	valid.Contains(checkfor)	? checkfor	: defvalue	;
					}

			#endregion

		}
}
