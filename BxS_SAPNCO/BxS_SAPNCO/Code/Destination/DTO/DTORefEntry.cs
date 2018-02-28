using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Destination
{
	internal class DTORefEntry : IDTO_SAPSystemReference
		{
			#region "Properties"

				public Guid		ID		{ get; set; }
				public string	Name	{ get; set; }

			#endregion

		}
}
