//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPC.BDCData
{
	public class DTO_SessionTranSPA
		{
			#region "Documentation"

				//	RFC_SPAGPA:	SPA/GPA structure for RFC
				//
				//	PARID		1 Types	MEMORYID	CHAR	20	0	Set/Get parameter ID
				//	PARVAL	1 Types						CHAR	255	0	Set/Get Parameter Value (Char 255)

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTranSPA( string memoryID, string memoryValue)
					{
						this.MemoryID			=	memoryID		;
						this.MemoryValue	=	memoryValue	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	MemoryID		{	get; set; }
				public	string	MemoryValue	{	get; set; }

			#endregion

		}
}
