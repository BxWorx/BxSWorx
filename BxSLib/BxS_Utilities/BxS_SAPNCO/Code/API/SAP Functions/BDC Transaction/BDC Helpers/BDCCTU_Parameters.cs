using System;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
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
				//						N	CATT without	individual screen control
				//						A	CATT with			individual screen control

				//	CTU_DEFSZE/CTU_RAFC/CTU_NOBIM/CTU_NOBEN:	b	No
				//																						X	Yes

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCCTU_Parameters(	char	DisplayMode			= BDCConstants.lz_CTU_N	,
																	char	UpdateMode			= BDCConstants.lz_CTU_A	,
																	char	CATTMode				= BDCConstants.lz_CTU_F	,
																	char	DefaultSize			= BDCConstants.lz_CTU_T	,
																	char	NoCommit   			= BDCConstants.lz_CTU_T	,
																	char	NoBatchInputFor	= BDCConstants.lz_CTU_T	,
																	char	NoBatchInputAft	= BDCConstants.lz_CTU_T		)
					{
						this.DisplayMode		=	DisplayMode			;
						this.UpdateMode			=	UpdateMode			;
						this.CATTMode				=	CATTMode				;
						this.DefaultSize		=	DefaultSize			;
						this.NoCommit				=	NoCommit				;
						this.NoBatchInpFor	=	NoBatchInputFor	;
						this.NoBatchInpAft	=	NoBatchInputAft	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	const string	lz_CTU_DisMde	= "AENP";
				private	const string	lz_CTU_UpdMde	= "LSA";
				private	const string	lz_CTU_CatMde	= " NA";
				private	const string	lz_CTU_YesNo	= " X";
				//.................................................
				private	char	_DspMde	;
				private	char	_UpdMde	;
				private	char	_CatMde	;
				private	char	_DefSze	;
				private	char	_NoComm	;
				private	char	_NoBInp	;
				private	char	_NoBEnd	;
				//.................................................
				[Flags]
				public enum Validate
					{
						Non = 0x00,
						Dsp = 0x01,
						Upd = 0x02,
						Cat	= 0x04,
						Sze = 0x08,
						Com = 0x10,
						BIF	= 0x20,
						BIA = 0x40
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	char		DisplayMode			{	get { return	this._DspMde; }
																					set	{ this._DspMde	= this.CheckDspMde	(value); }	}
				//.................................................
				public	char		UpdateMode			{	get { return	this._UpdMde; }
																					set	{ this._UpdMde	= this.CheckUpdMde	(value); }	}
				//.................................................
				public	char		CATTMode				{	get { return	this._CatMde; }
																					set	{ this._CatMde	= this.CheckCatMde	(value); }	}
				//.................................................
				public	char		DefaultSize			{	get { return	this._DefSze; }
																					set	{ this._DefSze	= this.CheckYesNo		(value); }	}
				//.................................................
				public	char		NoCommit				{	get { return	this._NoComm; }
																					set	{ this._NoComm	= this.CheckYesNo		(value); }	}
				//.................................................
				public	char		NoBatchInpFor		{	get { return	this._NoBInp; }
																					set	{ this._NoBInp	= this.CheckYesNo		(value); }	}
				//.................................................
				public	char		NoBatchInpAft		{	get { return	this._NoBEnd; }
																					set	{ this._NoBEnd	= this.CheckYesNo		(value); }	}

				//.................................................
				//.................................................

				public	char	DisplayMode_All			{ get { return	BDCConstants.lz_CTU_A; } }
				public	char	DisplayMode_Errors	{ get { return	BDCConstants.lz_CTU_E; } }
				public	char	DisplayMode_BGrnd		{ get { return	BDCConstants.lz_CTU_N; } }
				public	char	DisplayMode_BGDeb		{ get { return	BDCConstants.lz_CTU_P; } }
				//.................................................
				public	char	UpdateMode_Local		{ get { return	BDCConstants.lz_CTU_L; } }
				public	char	UpdateMode_Sync			{ get { return	BDCConstants.lz_CTU_S; } }
				public	char	UpdateMode_ASync		{ get { return	BDCConstants.lz_CTU_A; } }
				//.................................................
				public	char	CATTMode_None 			{ get { return	BDCConstants.lz_CTU_F; } }
				public	char	CATTMode_Cntrl			{ get { return	BDCConstants.lz_CTU_N; } }
				public	char	CATTMode_NoCntrl		{ get { return	BDCConstants.lz_CTU_A; } }
				//.................................................
				public	char	Setas_No						{ get { return	BDCConstants.lz_CTU_F; } }
				public	char	Setas_Yes						{ get { return	BDCConstants.lz_CTU_T; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Validate IsValid(DTO_CTUParams DTO, bool autoCorrect = true)
					{
						if (autoCorrect)
							{
								DTO.DisplayMode		= CheckDspMde(DTO.DisplayMode)			;
								DTO.UpdateMode		=	CheckUpdMde(DTO.UpdateMode)				;
								DTO.CATTMode			=	CheckCatMde(DTO.CATTMode)					;
								DTO.DefaultSize		= CheckYesNo(DTO.DefaultSize, true)	;
								DTO.NoCommit			=	CheckYesNo(DTO.NoCommit)					;
								DTO.NoBatchInpFor	=	CheckYesNo(DTO.NoBatchInpFor)			;
								DTO.NoBatchInpAft	=	CheckYesNo(DTO.NoBatchInpFor)			;
								return	0;
							}
						//.............................................
						Validate ln_Ret	= Validate.Non;

						if (DTO.DisplayMode		!= CheckDspMde(DTO.DisplayMode)		) ln_Ret |= Validate.Dsp;
						if (DTO.UpdateMode		!= CheckDspMde(DTO.UpdateMode)		)	ln_Ret |= Validate.Upd;
						if (DTO.CATTMode			!= CheckDspMde(DTO.CATTMode)			)	ln_Ret |= Validate.Cat;
						if (DTO.DefaultSize		!= CheckDspMde(DTO.DefaultSize)		)	ln_Ret |= Validate.Sze;
						if (DTO.NoCommit			!= CheckDspMde(DTO.NoCommit)			)	ln_Ret |= Validate.Com;
						if (DTO.NoBatchInpFor	!= CheckDspMde(DTO.NoBatchInpFor)	)	ln_Ret |= Validate.BIF;
						if (DTO.NoBatchInpAft	!= CheckDspMde(DTO.NoBatchInpAft)	)	ln_Ret |= Validate.BIA;
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void TransferImage(DTO_CTUParams DTO)
					{
						DTO.DisplayMode		=	this._DspMde;
						DTO.UpdateMode		=	this._UpdMde;
						DTO.CATTMode			=	this._CatMde;
						DTO.DefaultSize		=	this._DefSze;
						DTO.NoCommit			=	this._NoComm;
						DTO.NoBatchInpFor	=	this._NoBInp;
						DTO.NoBatchInpAft	=	this._NoBEnd;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckYesNo(char checkFor, bool defaultYes = false)
					{
						return	this.Check( lz_CTU_YesNo	, checkFor, defaultYes ? BDCConstants.lz_CTU_F : BDCConstants.lz_CTU_T );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckDspMde(char checkFor)
					{
						return	this.Check( lz_CTU_DisMde, checkFor, BDCConstants.lz_CTU_N );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckUpdMde(char checkFor)
					{
						return	this.Check( lz_CTU_UpdMde, checkFor, BDCConstants.lz_CTU_S );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char CheckCatMde(char checkFor)
					{
						return	this.Check( lz_CTU_CatMde, checkFor, BDCConstants.lz_CTU_F );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private char Check(string valid, char checkfor, char defvalue)
					{
						char lc_Code	= char.ToUpper(checkfor);
						return	valid.Contains(lc_Code)	? lc_Code	: defvalue	;
					}

			#endregion

		}
}
