//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions
{
	public class BDCEntry
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
				public BDCEntry(	string	ProgramName	= BDCConstants.lz_E	,
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCEntry(	string	ProgramName	= BDCConstants.lz_E	,
													string	Dynpro			= BDCConstants.lz_D	,
													string	Begin				= BDCConstants.lz_F	,
													string	Field				= BDCConstants.lz_E	,
													string	Value				= BDCConstants.lz_E		)
					{
						this.ProgramName	= ProgramName	;
						this.FieldName		= Field				;
						this.FieldValue		= Value				;
						this.Dynpro				= Dynpro			;
						this.Begin				= Begin				;
						//.............................................
						if (			!this.Begin.Equals(BDCConstants.lz_F)
									&&	!this.Begin.Equals(BDCConstants.lz_T)
									&&	!this.Begin.Length.Equals(0))
							{
								this.Begin = BDCConstants.lz_T;
							}
				}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	ProgramName	{ get; private set; }
				public	string	Dynpro			{ get; private set; }
				public	string	Begin				{ get; private set; }
				public	string	FieldName		{ get; private set; }
				public	string	FieldValue	{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public void Reset()
					{
						this.ProgramName	= string.Empty;
						this.Dynpro				= string.Empty;
						this.Begin				= string.Empty;
						this.FieldName		= string.Empty;
						this.FieldValue		= string.Empty;
					}

			#endregion

		}
}
