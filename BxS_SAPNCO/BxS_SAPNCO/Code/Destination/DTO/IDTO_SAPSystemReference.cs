using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	public interface IDTO_SAPSystemReference
		{
			#region "Properties"

				Guid		ID		{ get; set; }
				string	Name	{ get; set; }

			#endregion

		}
}