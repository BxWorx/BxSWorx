using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public interface IDTORefEntry
		{
			#region "Properties"

				Guid		ID		{ get; set; }
				string	Name	{ get; set; }

			#endregion

		}
}