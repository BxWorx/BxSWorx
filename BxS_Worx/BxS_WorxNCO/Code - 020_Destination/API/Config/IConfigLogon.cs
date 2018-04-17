using System.Security;
//.........................................................
using BxS_WorxNCO.Destination.Config;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IConfigLogon : IConfigBase
		{
			#region "Properties"

				bool	ForRepository		{ get; }
				//.................................................
				string	Language			{ set; }
				string	Client				{ set; }
				string	User					{ set; }
				string	Password			{ set; }
				//.................................................
				SecureString	SecurePassword	{ get; set; }

			#endregion

		}
}