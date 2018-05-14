using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.NCO
{
	public interface IDTO_SessionRequest
		{
			#region "Properties"

				string		User	{ get; set; }
				string		Name	{ get; set; }
				DateTime	From	{ get; set; }
				DateTime	To		{ get; set; }
				bool			FromX	{ get; set; }
				bool			ToX		{ get; set; }

			#endregion

		}
}