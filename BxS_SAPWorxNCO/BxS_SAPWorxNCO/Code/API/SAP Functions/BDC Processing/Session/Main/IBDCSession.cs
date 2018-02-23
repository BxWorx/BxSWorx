using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public interface IBDCSession
		{
			#region "Properties"

				DTO_SessionOptions	SessionOptions	{ get; }
				DTO_SessionHeader		SessionHeader		{	get; }
				//.................................................
				int	TransactionCount					{ get; }
				int	CompletedSuccessfulCount	{ get; }
				int	CompletedFaultyCount			{ get; }
				//.................................................
				ConcurrentDictionary< int, DTO_SessionTran >		Transactions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				Task<int> ProcessAsync();
				//.................................................
				DTO_SessionTran	CreateTran( Guid ID = default(Guid) );

				void	AddTransaction(	IList<DTO_SessionTran>	transactions );
				void	AddTransaction(	DTO_SessionTran					transaction	 );
				//.................................................
				void	Reset();

			#endregion

		}
}
