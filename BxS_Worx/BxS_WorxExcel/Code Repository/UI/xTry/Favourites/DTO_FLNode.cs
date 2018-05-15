using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.DTO
{
	internal struct DTO_FLNode
		{
			#region "Properties"

				public	string	SAPID		{ get; set; }
				public	string	Name		{ get; set; }
				public	string	Client	{ get; set; }
				public	string	User		{ get; set; }
				public	SecureString	Pwrd		{ get; set; }
				//...
				public	bool		IsSSO		{ get; set; }

			#endregion

		}
	}
