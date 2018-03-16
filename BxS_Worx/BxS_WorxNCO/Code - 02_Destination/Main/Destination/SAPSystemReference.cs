using System;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	internal class SAPSystemReference : ISAPSystemReference
		{
			#region "Properties"

				public Guid		ID			{ get; set; }
				public string	SAPName	{ get; set; }

			#endregion

		}
}
