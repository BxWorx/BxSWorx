using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using BxS_SAPNCO.Pipeline;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class BDCSession : IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSession(	BDC_OpFnc						sessionOpFnc
														, DTO_SessionOptions	sessionOptions
														, DTO_SessionHeader		sessionHeader
														, BDCCallTranProfile	profile
														,	ConsumerOpEnv<	DTO_SessionTran
																						, DTO_ProgressInfo >		consumerOpEnv	)
					{
						//. locals ....................................
						this._OpFnc				= sessionOpFnc	;
						this._CallProfile	= profile				;
						this._ConOpEnv		= consumerOpEnv	;

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

				private	int	_Indexer;

				private readonly	object	_Lock;
				//.................................................
				private readonly	BDC_OpFnc																							_OpFnc				;
				private readonly	BDCCallTranProfile																		_CallProfile	;
				private readonly	ConsumerOpEnv< DTO_SessionTran , DTO_ProgressInfo >		_ConOpEnv			;
				//.................................................
				private		Pipeline< DTO_SessionTran , DTO_ProgressInfo >								_Pipeline ;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	ConcurrentDictionary< int, DTO_SessionTran >	Transactions		{ get; }
				public	DTO_SessionOptions														SessionOptions	{ get; }
				public	DTO_SessionHeader															SessionHeader		{ get; }
				//.................................................
				public	int	TransactionCount					{ get { return	this.Transactions.Count											; } }
				public	int	CompletedSuccessfulCount	{ get { return	this._Pipeline.JobsCompletedSuccesfulCount	; } }
				public	int	CompletedFaultyCount			{ get { return	this._Pipeline.JobsCompletedFaultyCount			; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<int> ProcessAsync()
					{
						if (this.SessionOptions.NoOfConsumers.Equals(0))	return	0;
						if (!this._CallProfile.Ready())			return	0;
						//.............................................
						this._Pipeline	= this._OpFnc.CreateBDCPipeline( this._ConOpEnv.CT );
						this.CreateAddConsumers();
						//.............................................
						foreach (KeyValuePair<int, DTO_SessionTran> ls_kvp in this.Transactions)
							{
								this._ConOpEnv.Queue.TryAdd(ls_kvp.Value);
							}
						this._ConOpEnv.Queue.CompleteAdding();
						//.............................................
						return	await this._Pipeline.StartAsync().ConfigureAwait(false);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionTran CreateTran( Guid ID = default(Guid) )
					{
						return	this._OpFnc.CreateSessionTran(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction( IList<DTO_SessionTran> transactions )
					{
						foreach (DTO_SessionTran lo_Tran in transactions)
							{
								this.AddTransaction(lo_Tran);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction( DTO_SessionTran transaction )
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
						for (int i = 0; i < this.SessionOptions.NoOfConsumers; i++)
							{
								BDCCallTranProcessor lo_Tran	=	this._OpFnc.CreateBDCCallTran(this._CallProfile);

								if (lo_Tran.Configure())
									{
										BDCCallTranConsumer<DTO_SessionTran, DTO_ProgressInfo> lo_Con
											=	this._OpFnc.CreateBDCCallConsumer(	this._ConOpEnv
																													, this.SessionHeader
																													, this._CallProfile
																													, lo_Tran							);

										this._Pipeline.AddConsumer(lo_Con);
									}
							}
					}

			#endregion

		}
}
