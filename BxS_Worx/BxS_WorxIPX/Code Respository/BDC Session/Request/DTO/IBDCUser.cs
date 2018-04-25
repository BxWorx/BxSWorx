using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IBDCUser
		{
			#region "Properties"

				Guid			GUID				{ get; set; }

				DateTime	Timestamp		{ get; set; }
				String		User				{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Transfer( IBDCUser user );

			#endregion
		}
}
