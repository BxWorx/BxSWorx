﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCSession	: IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCSession(	BDCOpFnc	opFnc	,
														BDCOpEnv	opEnv		)
					{
						this._OpFnc	= opFnc	;
						this._OpEnv	= opEnv	;
						//.............................................
						this._Indexer		= 0												;
						this._Lock			= new object()						;
						this._RfcHeader	= opFnc.CreateRFCHeader()	;
						//.............................................
						this.SessionHeader	= opFnc.CreateSessionHeader()	;
						this.SessionOptions	= opFnc.CreateSessionOptions();
						//.............................................
						this.Transactions	= new	ConcurrentDictionary< int, DTO_SessionTran >();
						this._RfcTran			= new	ConcurrentQueue< DTO_RFCTran >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	int	_Indexer;
				//.................................................
				private readonly	BDCOpFnc			_OpFnc			;
				private readonly	BDCOpEnv			_OpEnv			;
				private readonly	DTO_RFCHeader	_RfcHeader	;

				private readonly	object	_Lock	;
				//.................................................
				private readonly	ConcurrentQueue<DTO_RFCTran>	_RfcTran;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	IsStarted							{ get { return	this._OpEnv.IsStarted		; } }
				public	int		TransactionCount			{ get { return	this.Transactions.Count	; } }
				public	int		RFCTransactionCount		{ get { return	this._RfcTran.Count			; } }
				//.................................................
				public	DTO_SessionOptions	SessionOptions	{ get; }
				public  DTO_SessionHeader		SessionHeader		{ get; }
				//.................................................
				public	ConcurrentDictionary< int, DTO_SessionTran >	Transactions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Process()
					{
						this._OpEnv.Start();
						if (!this.IsStarted)	return;
						//.............................................
						this.ParseOut();

						this.ParseIn();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ConfigureUser(IDTOConfigSetupDestination config)
					{
						this._OpEnv.Destination.LoadConfig(config);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTran CreateTran(Guid ID = default(Guid))
					{
						return	this._OpFnc.CreateSessionTran(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction(IList<DTO_SessionTran> transactions)
					{
						foreach (DTO_SessionTran lo_Tran in transactions)
							{
								this.AddTransaction(lo_Tran);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction(DTO_SessionTran transaction)
					{
						lock (this._Lock)
							{
								Interlocked.Increment(ref this._Indexer);
								if (!this.Transactions.TryAdd(this._Indexer, transaction))	Interlocked.Decrement(ref this._Indexer);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Reset()
					{
						this.ClearRFC();
						this.Transactions.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseIn()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseOut()
					{
						this.ClearRFC();
						//.............................................
						this._OpEnv.Profile.Configure( this._RfcHeader );
						this._OpEnv.Parser.PutCTUOptions(	this.SessionHeader.CTUParms	,
																							this._RfcHeader		.CTUParms		);
						//.............................................
						ICollection<int> lt_Keys	= this.Transactions.Keys;

						if (this.SessionOptions.Sequential)
							{
								foreach (int ln_Key in lt_Keys)
									{
										if (this.Transactions.TryGetValue(ln_Key, out DTO_SessionTran lo_BDCTran))
											{
												this.ParseTran(lo_BDCTran);
											}
									}
							}
						else
							{
								ParallelLoopResult x = Parallel.ForEach
									(	lt_Keys	,
										(ln_Key) =>	{	if (this.Transactions.TryGetValue(ln_Key, out DTO_SessionTran lo_BDCTran))
																		{
																			this.ParseTran(lo_BDCTran);
																		}
																}
									);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ParseTran(DTO_SessionTran lo_BDCTran)
					{
						DTO_RFCTran lo_RFCTran	= this._OpFnc.CreateRFCTran();
						this._OpEnv.Profile.Configure(lo_RFCTran);
						this._OpEnv.Parser.PutBDCData(lo_BDCTran.BDCData, lo_RFCTran.BDCData);
						this._RfcTran.Enqueue(lo_RFCTran);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void ClearRFC()
					{
						while(!this._RfcTran.IsEmpty)
							{
								this._RfcTran.TryDequeue( out DTO_RFCTran lo );
							}
					}

			#endregion

		}
}
