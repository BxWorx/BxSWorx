using System;
using System.Linq;
//.........................................................
using BxS_SAPNCO.BDCProcess;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.CTU
{
	public class CTUParametersHandler
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public CTUParametersHandler(	char	DisplayMode			= BDCConstants.lz_CTU_N	,
																			char	UpdateMode			= BDCConstants.lz_CTU_A	,
																			char	CATTMode				= BDCConstants.lz_CTU_F	,
																			char	DefaultSize			= BDCConstants.lz_CTU_T	,
																			char	NoCommit   			= BDCConstants.lz_CTU_F	,
																			char	NoBatchInputFor	= BDCConstants.lz_CTU_F	,
																			char	NoBatchInputAft	= BDCConstants.lz_CTU_F		)
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

				private	const string	lz_CTU_DisMde	= "AENP"	;
				private	const string	lz_CTU_UpdMde	= "LSA"		;
				private	const string	lz_CTU_CatMde	= " NA"		;
				private	const string	lz_CTU_YesNo	= " X"		;
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
				public enum Ce_Validate
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
				public Ce_Validate IsValid(DTO_CTUParms DTO, bool autoCorrect = true)
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
						Ce_Validate ln_Ret	= Ce_Validate.Non;

						if (DTO.DisplayMode		!= CheckDspMde(DTO.DisplayMode)		) ln_Ret |= Ce_Validate.Dsp;
						if (DTO.UpdateMode		!= CheckDspMde(DTO.UpdateMode)		)	ln_Ret |= Ce_Validate.Upd;
						if (DTO.CATTMode			!= CheckDspMde(DTO.CATTMode)			)	ln_Ret |= Ce_Validate.Cat;
						if (DTO.DefaultSize		!= CheckDspMde(DTO.DefaultSize)		)	ln_Ret |= Ce_Validate.Sze;
						if (DTO.NoCommit			!= CheckDspMde(DTO.NoCommit)			)	ln_Ret |= Ce_Validate.Com;
						if (DTO.NoBatchInpFor	!= CheckDspMde(DTO.NoBatchInpFor)	)	ln_Ret |= Ce_Validate.BIF;
						if (DTO.NoBatchInpAft	!= CheckDspMde(DTO.NoBatchInpAft)	)	ln_Ret |= Ce_Validate.BIA;
						//.............................................
						return	ln_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_CTUParms GetImage()
					{
						return	new DTO_CTUParms	{	DisplayMode		= this._DspMde,
																				UpdateMode		= this._UpdMde,
																				CATTMode			= this._CatMde,
																				DefaultSize		= this._DefSze,
																				NoCommit			= this._NoComm,
																				NoBatchInpFor = this._NoBInp,
																				NoBatchInpAft	= this._NoBEnd	};
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void TransferImage(DTO_CTUParms DTO)
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
						return	this.Check( lz_CTU_YesNo	, checkFor, defaultYes ?	BDCConstants.lz_CTU_T :
																																				BDCConstants.lz_CTU_F		);
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
