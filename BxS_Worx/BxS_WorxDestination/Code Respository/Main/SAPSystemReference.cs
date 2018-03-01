using System;
//.........................................................
using BxS_WorxIPX.API.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxDestination.Main
{
	internal class SAPSystemReference : ISAPSystemReference
		{
			#region "Properties"

				public Guid		ID			{ get; set; }
				public string	SAPName	{ get; set; }

			#endregion

		}
}
