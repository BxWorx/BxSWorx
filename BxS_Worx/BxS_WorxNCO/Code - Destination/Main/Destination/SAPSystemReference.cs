﻿using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.API.Destination
{
	internal class SAPSystemReference : ISAPSystemReference
		{
			#region "Properties"

				public Guid		ID			{ get; set; }
				public string	SAPName	{ get; set; }

			#endregion

		}
}