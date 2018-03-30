using System;
using System.Threading.Tasks;
//.........................................................
using BxS_WorxNCO.BDCSession.DTO;
using BxS_WorxNCO.Helpers.ObjectPool;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.Main
{
	public class BDC_SessionSAPMsgs : PooledObject
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_SessionSAPMsgs()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public async Task<int> ProcessAsync()
					{
						return	await	Task.Run( ()=> 10 ).ConfigureAwait(false);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

			#endregion
		}
}
