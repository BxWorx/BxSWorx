using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.DL
{
	public interface IDTOConnection
		{
			#region "Properties"

				Guid		ID		{ get; set; }
				string	Name	{ get; set; }

			#endregion

		}
}