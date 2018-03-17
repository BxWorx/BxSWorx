//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_SPA
		{
			#region "Documentation"

				//	RFC_SPAGPA:	SPA/GPA structure for RFC
				//
				//	PARID		1 Types	MEMORYID	CHAR	20	0	Set/Get parameter ID
				//	PARVAL	1 Types						CHAR	255	0	Set/Get Parameter Value (Char 255)

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	MemoryID		{	get; set; }
				public	string	MemoryValue	{	get; set; }

			#endregion

		}
}
