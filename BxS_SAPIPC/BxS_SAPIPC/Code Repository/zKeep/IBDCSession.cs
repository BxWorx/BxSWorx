using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPC.BDCData
{
	public interface IBDCSession
		{
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				BDCTransaction	CreateTran( Guid ID = default(Guid) );

			#endregion

		}
}
