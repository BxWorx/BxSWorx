using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API
{
	public interface ISAPSystemReference
		{
			#region "Properties"

				Guid		ID			{ get; set; }
				string	SAPName	{ get; set; }

			#endregion

		}
}
