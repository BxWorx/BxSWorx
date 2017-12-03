using System;
//.........................................................
using BxS_SAPConn.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public interface IDTOFavourite
		{
			#region "Properties"

				Guid		UUID					{ get; set; }
				//.................................................
				int			SeqNo					{ get; set; }
				string	Client				{ get; set; }
				string	User					{ get; set; }
				string	Password			{ get; set; }
				//.................................................
				IDTOConnection	SAPConn	{ get; set; }

			#endregion

		}
}
