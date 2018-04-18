//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.DDIC
{
	internal class DTO_DDICInfo_Field
		{
			#region "Documentation"

				//	*"----------------------------------------------------------------------
				//	DFIES                          Active
				//	DD Interface: Table Fields for DDIF_FIELDINFO_GET
				//	
				//	TABNAME					Types	TABNAME				CHAR	030			Table Name
				//	FIELDNAME				Types	FIELDNAME			CHAR	030			Field Name
				//	FIELDTEXT				Types	AS4TEXT				CHAR	060			Short Description of Repository Objects
				//	*"----------------------------------------------------------------------

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	TabName		{	get; set; }
				public	string	FldName		{	get; set; }
				public	string	FldText		{	get; set; }

			#endregion

		}
}
