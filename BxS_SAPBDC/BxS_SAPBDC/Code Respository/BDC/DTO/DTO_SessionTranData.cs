//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.BDC
{
	public class DTO_SessionTranData
		{
			#region "Documentation"

				//	BDCDATA:	Batch input: New table field structure
				//
				//	PROGRAM		1	Types	BDC_PROG		CHAR	 40		BDC module pool
				//	DYNPRO		1 Types	BDC_DYNR		NUMC	  4		BDC Dynpro Number
				//	DYNBEGIN	1 Types	BDC_START		CHAR	  1		BDC Dynpro Start
				//	FNAM			1 Types	FNAM_____4	CHAR	132		Field name
				//	FVAL			1 Types	BDC_FVAL		CHAR	132		BDC field value

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTranData(		string	ProgramName	= BDC_Constants.cz_Val_Empty
																		,	int			Dynpro			= 0
																		,	bool		Begin				= false
																		,	string	Field				= BDC_Constants.cz_Val_Empty
																		,	string	Value				= BDC_Constants.cz_Val_Empty	)
					{
						this.ProgramName	= string.IsNullOrEmpty(ProgramName)	? BDC_Constants.cz_Val_Empty	: ProgramName																	;
						this.FieldName		= string.IsNullOrEmpty(Field)				? BDC_Constants.cz_Val_Empty	: Field																				;
						this.FieldValue		= string.IsNullOrEmpty(Value)				? BDC_Constants.cz_Val_Empty	: Value																				;
						this.Dynpro				= Dynpro.Equals(0)									? BDC_Constants.cz_Val_Empty	: Dynpro.ToString(BDC_Constants.cz_Val_Scrn0)	;
						this.Begin				= Begin															? BDC_Constants.cz_Val_True		: BDC_Constants.cz_Val_False									;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ProgramName	{ get; set; }
				public	string	Dynpro			{ get; set; }
				public	string	Begin				{ get; set; }
				public	string	FieldName		{ get; set; }
				public	string	FieldValue	{ get; set; }

			#endregion

		}
}
