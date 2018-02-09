using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public interface IBDCSession
		{
			#region "Properties"

				DTO_SessionOptions	Options	{ get; set; }
				int									Count		{ get; }
				//.................................................
				ConcurrentQueue<IBDCTranData>	BDCTransactions		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	Process();
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void	AddBDCTransaction	(IList<IBDCTranData>	bdcTransactions	);
				void	AddBDCTransaction	(IBDCTranData					bdcTransaction	);
				void	Reset();

			#endregion

		}
}
