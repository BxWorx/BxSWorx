using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IUser
		{
			#region "Properties"

				Guid			GUID				{ get; set; }

				DateTime	Timestamp		{ get; set; }
				String		Name				{ get; set; }

			#endregion

		}
}
