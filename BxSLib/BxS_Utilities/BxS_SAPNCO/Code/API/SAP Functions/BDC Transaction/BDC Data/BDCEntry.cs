//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions
{
	public class BDCEntry
		{
			#region "Constructors"

				//	PROGRAM		1	Types	BDC_PROG		CHAR	 40		BDC module pool
				//	DYNPRO		1 Types	BDC_DYNR		NUMC	  4		BDC Dynpro Number
				//	DYNBEGIN	1 Types	BDC_START		CHAR	  1		BDC Dynpro Start
				//	FNAM			1 Types	FNAM_____4	CHAR	132		Field name
				//	FVAL			1 Types	BDC_FVAL		CHAR	132		BDC field value

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCEntry(	string	ProgramName	= lz_E	,
													int			Dynpro			= 0			,
													bool		Begin				= false	,
													string	Field				= lz_E	,
													string	Value				= lz_E		)
					{
						this.ProgramName	= string.IsNullOrEmpty(ProgramName)	? lz_E	: ProgramName						;
						this.FieldName		= string.IsNullOrEmpty(Field)				? lz_E	: Field									;
						this.FieldValue		= string.IsNullOrEmpty(Value)				? lz_E	: Value									;
						this.Dynpro				= Dynpro.Equals(0)									? lz_D	: Dynpro.ToString(lz_D)	;
						this.Begin				= Begin															? lz_T	: lz_F									;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCEntry(	string	ProgramName	= lz_E	,
													string	Dynpro			= lz_D	,
													string	Begin				= lz_F	,
													string	Field				= lz_E	,
													string	Value				= lz_E		)
					{
						this.ProgramName	= ProgramName	;
						this.FieldName		= Field				;
						this.FieldValue		= Value				;
						this.Dynpro				= Dynpro			;
						this.Begin				= Begin				;
						//.............................................
						if (			!this.Begin.Equals(lz_F)
									&&	!this.Begin.Equals(lz_T)
									&&	!this.Begin.Length.Equals(0))
							{
								this.Begin = lz_T;
							}
				}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private const string	lz_T	= "X"			;
				private const string	lz_F	= " "			;
				private const string	lz_E	= ""			;
				private const string	lz_D	= "0000"	;

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
