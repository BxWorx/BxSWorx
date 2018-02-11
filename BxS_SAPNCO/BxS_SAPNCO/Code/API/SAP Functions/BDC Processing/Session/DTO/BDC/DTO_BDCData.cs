//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class DTO_BDCData
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
				public DTO_BDCData(	string	ProgramName	= BDCConstants.lz_E	,
															int			Dynpro			= 0									,
															bool		Begin				= false							,
															string	Field				= BDCConstants.lz_E	,
															string	Value				= BDCConstants.lz_E		)
					{
						this.ProgramName	= string.IsNullOrEmpty(ProgramName)	? BDCConstants.lz_E	: ProgramName													;
						this.FieldName		= string.IsNullOrEmpty(Field)				? BDCConstants.lz_E	: Field																;
						this.FieldValue		= string.IsNullOrEmpty(Value)				? BDCConstants.lz_E	: Value																;
						this.Dynpro				= Dynpro.Equals(0)									? BDCConstants.lz_D	: Dynpro.ToString(BDCConstants.lz_D)	;
						this.Begin				= Begin															? BDCConstants.lz_T	: BDCConstants.lz_F										;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ProgramName	{ get; }
				public	string	Dynpro			{ get; }
				public	string	Begin				{ get; }
				public	string	FieldName		{ get; }
				public	string	FieldValue	{ get; }

			#endregion

		}
}
