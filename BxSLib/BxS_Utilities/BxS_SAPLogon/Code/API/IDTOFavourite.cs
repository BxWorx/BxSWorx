using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public interface IDTOFavourite
		{
			#region "Properties"

				Guid		UUID					{ get; set; }
				//.................................................
				int			SeqNo					{ get; set; }
				Guid		ServiceID			{ get; set; }
				string	Description		{ get; set; }
				string	Client				{ get; set; }
				string	SystemID			{ get; set; }

			#endregion

		}
}
