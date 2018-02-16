using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPNCO.Destination;
using BxS_SAPNCO.Helpers;
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCSession : IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCSession(	BDCSessionOpFnc			sessionOpFnc
													, DTO_SessionOptions	sessionOptions
													, DTO_SessionHeader		sessionHeader
													,	BDCCallTranParser		parser

													,	ConsumerOpEnv< DTO_SessionTran , DTO_ProgressInfo >		consumerOpEnv		)
					{
						//. locals ....................................
						this._SessOpEnv	= sessionOpFnc	;
						this._Parser		= parser				;
						this._ConOpEnv	= consumerOpEnv	;
						//. properties ................................
						this.SessionOptions		= sessionOptions	;
						this.SessionHeader		= sessionHeader		;
						//. initialise ................................
						this.Transactions		= new	ConcurrentDictionary< int, DTO_SessionTran >()	;
						this._Lock					= new object()	;
						this._Indexer				= 0							;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	object	_Lock	;
				//.................................................
				private readonly	BDCSessionOpFnc			_SessOpEnv			;
				private	readonly	BDCCallTranParser		_Parser					;
				private readonly	ConsumerOpEnv< DTO_SessionTran , DTO_ProgressInfo >	_ConOpEnv	;
				//.................................................
				//.................................................
				private		Pipeline< DTO_SessionTran , DTO_ProgressInfo >							_Pipeline ;






				private readonly	BDCCallTranProfile	_CallProfile		;


				private	readonly	CancellationToken	_CT;
				private	readonly	int								_ConsumerCount;
				private	int	_Indexer;
				//.................................................

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	ConcurrentDictionary< int, DTO_SessionTran >		Transactions	{ get; }

				internal	DTO_SessionOptions		SessionOptions	{ get; }
				internal	DTO_SessionHeader			SessionHeader		{ get; }





				//public	bool	IsStarted							{ get { return	this._OpEnv.IsStarted		; } }
				public	int		TransactionCount			{ get { return	this.Transactions.Count	; } }
				//public	int		RFCTransactionCount		{ get { return	this._OpEnv.PLOpEnv.Queue.Count	; } }
				//.................................................

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> ProcessAsync()
					{
						if (this._ConsumerCount.Equals(0))	return	0;
						if (!this._CallProfile.Ready())			return	0;
						//.............................................
						this._Pipeline	= this._SessOpEnv.CreateBDCPipeline( this._CT );
						this.CreateAddConsumers();
						//.............................................
						this._ConOpEnv.Reset();
						foreach (KeyValuePair<int, DTO_SessionTran> ls_kvp in this.Transactions)
							{
								this._ConOpEnv.Queue.TryAdd(ls_kvp.Value);
							}
						//.............................................
						return	await this._Pipeline.StartAsync().ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTran CreateTran(Guid ID = default(Guid))
					{
						return	this._OpFnc.Value.SessionTran(ID);
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
								if (!this.Transactions.TryAdd(this._Indexer, transaction))
									Interlocked.Decrement(ref this._Indexer);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Reset()
					{
						this.Transactions.Clear();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void CreateAddConsumers()
					{
						for (int i = 0; i < this._ConsumerCount; i++)
							{
								BDCCallTranProcessor lo_Tran	=	this._SessOpEnv.CreateBDCCallTran(this._CallProfile);

								if (lo_Tran.Configure())
									{
										BDCCallTranConsumer<DTO_SessionTran, DTO_ProgressInfo> lo_Con
											=	this._SessOpEnv.CreateBDCCallConsumer(	this._ConOpEnv
																															, this.SessionHeader
																															, lo_Tran
																															, this._Parser				);

										this._Pipeline.AddConsumer(lo_Con);
									}
							}
					}

			#endregion

		}
}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public void	ConfigureUser(IDTOConfigSetupDestination config)
				//	{
				//		this._OpEnv.DestRFC.LoadConfig(config);
				//	}
