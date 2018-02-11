﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//.........................................................
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public interface IBDCSession
		{
			#region "Properties"

				DTO_SessionOptions	SessionOptions	{ get; }
				DTO_SessionHeader		SessionHeader		{	get; }
				//.................................................
				int		TransactionCount		{ get; }
				int		RFCTransactionCount	{ get; }
				bool	IsStarted						{ get; }
				//.................................................
				ConcurrentDictionary< int, DTO_SessionTran >		Transactions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void	ConfigureUser( IDTOConfigSetupDestination config );
				//.................................................
				void	Process();
				//.................................................
				DTO_SessionTran	CreateTran( Guid ID = default(Guid) );

				void	AddTransaction(	IList<DTO_SessionTran>	transactions );
				void	AddTransaction(	DTO_SessionTran					transaction	 );
				//.................................................
				void	Reset();

			#endregion

		}
}
