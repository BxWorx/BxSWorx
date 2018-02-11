using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using BxS_SAPNCO.API.DL;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public class BDCSession	: IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCSession(	BDCOpEnv<DTO_SessionProgressInfo>	opEnv					,
														DTO_SessionOptions								options				,
														DTO_BDCSessionHeader							sessionHeader		)
					{
						this._OpEnv					= opEnv					;
						this.SessionOptions	= options				;
						this.SessionHeader	= sessionHeader	;
						//.............................................
						this._Indexer	= 0							;
						this._Lock		= new object()	;
						//.............................................
						this.Transactions	= new	ConcurrentDictionary<int, BDCSessionTran>	();
						this._RfcTran			= new	ConcurrentQueue<DTO_RFCSessionTran>				();

						this._RfcHeader		= this._OpEnv.CreateSessionRFCHeader						();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	int	_Indexer;
				//.................................................
				private readonly	BDCOpEnv<DTO_SessionProgressInfo>	_OpEnv;
				private readonly	object														_Lock	;
				//.................................................
				private readonly	DTO_RFCSessionHeader								_RfcHeader	;
				private readonly	ConcurrentQueue<DTO_RFCSessionTran>	_RfcTran		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	IsStarted							{ get { return	this._OpEnv.IsStarted		; } }
				public	int		TransactionCount			{ get { return	this.Transactions.Count	; } }
				public	int		RFCTransactionCount		{ get { return	this._RfcTran.Count			; } }
				//.................................................
				public	DTO_SessionOptions		SessionOptions	{ get; }
				public  DTO_BDCSessionHeader	SessionHeader		{ get; }
				//.................................................
				public	ConcurrentDictionary<int, BDCSessionTran>		Transactions	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Process()
					{
						this._OpEnv.Start();
						if (!this.IsStarted)	return;
						//.............................................
						this.Parse();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	ConfigureUser(IDTOConfigSetupDestination config)
					{
						this._OpEnv.Destination.LoadConfig(config);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCSessionTran	CreateTran(Guid ID = default(Guid))
					{
						return	this._OpEnv.CreateSessionBDCTran(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction(IList<BDCSessionTran> transactions)
					{
						foreach (BDCSessionTran lo_Tran in transactions)
							{
								this.AddTransaction(lo_Tran);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction(BDCSessionTran transaction)
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
				private void Parse()
					{
						this.ClearRFC();
						//.............................................
						this._OpEnv.Profile.Configure(this._RfcHeader);
						this._OpEnv.Parser.PutCTUOptions(	this.SessionHeader.CTUOptions	,
																							this._RfcHeader		.CTUOpts			);
						//.............................................
						ICollection<int> lt_Keys	= this.Transactions.Keys;

						if (this.SessionOptions.Sequential)
							{
								foreach (int ln_Key in lt_Keys)
									{
										if (this.Transactions.TryGetValue(ln_Key, out BDCSessionTran lo_BDCTran))
											{
												DTO_RFCSessionTran lo_RFCTran	= this._OpEnv.CreateSessionRFCTran();
												this._OpEnv.Profile.Configure(lo_RFCTran);
												this._OpEnv.Parser.PutBDCData(lo_BDCTran.BDCData, lo_RFCTran.BDCData);
												this._RfcTran.Enqueue(lo_RFCTran);
											}
									}
							}
						else
							{

							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void	ClearRFC()
					{
						while(!this._RfcTran.IsEmpty)
							{
								this._RfcTran.TryDequeue(out DTO_RFCSessionTran lo);
							}
					}

			#endregion

		}
}
