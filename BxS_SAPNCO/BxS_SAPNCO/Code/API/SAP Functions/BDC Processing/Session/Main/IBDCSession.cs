using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//.........................................................
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public interface IBDCSession
		{
			#region "Properties"

				DTO_SessionOptions		SessionOptions	{ get; }
				DTO_BDCSessionHeader	SessionHeader		{	get; }
				//.................................................
				int		TransactionCount		{ get; }
				int		RFCTransactionCount	{ get; }
				bool	IsStarted						{ get; }
				//.................................................
				ConcurrentDictionary< int, BDCSessionTran >		Transactions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	ConfigureUser(IDTOConfigSetupDestination config);
				//.................................................
				void	Process();
				//.................................................
				BDCSessionTran	CreateTran(Guid ID = default(Guid));

				void	AddTransaction(	IList<BDCSessionTran>	transactions	);
				void	AddTransaction(	BDCSessionTran				transaction		);
				//.................................................
				void	Reset();

			#endregion

		}
}
