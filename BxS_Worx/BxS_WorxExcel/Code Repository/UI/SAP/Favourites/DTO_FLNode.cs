using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.DTO
{
	internal struct DTO_FLNode
		{
			#region "Properties"

				//...
				public	string	SAPID		{ get; set; }
				internal	string	Client	{ get; set; }
				internal	string	User		{ get; set; }
				internal	SecureString	Pwrd		{ get; set; }
				//...
				internal	bool		IsSSO		{ get; set; }

			#endregion

		}
	}
