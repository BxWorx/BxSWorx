using System;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	internal class SAPSystemReference : ISAPSystemReference
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPSystemReference(	Guid		id		= default(Guid)
																		, string	name	= default(string)
																		, bool		isSSO = false						)
					{
						this.ID				= id		;
						this.SAPName	= name	;
						this.IsSSO		= isSSO	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public Guid		ID			{ get; set; }
				public string	SAPName	{ get; set; }
				public bool		IsSSO		{ get; set; }

			#endregion

		}
}
