using System;
using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public interface IBDCSession
		{
			#region "Properties"

				DTO_SessionOptions	Options	{ get; set; }
				int									Count		{ get; }
				//.................................................
				ConcurrentDictionary<	Guid, IBDCTranData >	BDCTransactions		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	Process();
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void	AddBDCTransaction(IBDCTranData BDCTransaction);
				void	Reset();

			#endregion

		}
}
