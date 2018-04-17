//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.DTO
{
	public class DTO_BDC_Data
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
			#region "Properties"

				public	string	ProgramName	{ get; set; }
				public	string	Dynpro			{ get; set; }
				public	string	Begin				{ get; set; }
				public	string	FieldName		{ get; set; }
				public	string	FieldValue	{ get; set; }

			#endregion

		}
}
