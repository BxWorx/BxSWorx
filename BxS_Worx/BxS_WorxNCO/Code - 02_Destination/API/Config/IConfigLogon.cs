using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface IConfigLogon
		{
			#region "Properties"

				bool		ForRepository	{ get; set; }
				//.................................................
				string	Language			{ get; set; }
				string	Client				{ get; set; }
				string	User					{ get; set; }
				string	Password			{ get; set; }
				//.................................................
				SecureString	SecurePassword	{ get; set; }

			#endregion

		}
}