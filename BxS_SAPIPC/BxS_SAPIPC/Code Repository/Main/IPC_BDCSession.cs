using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPC.BDCData
{
	public class IPC_BDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IPC_BDCSession()
					{
						this._Lock			= new object()	;
						this._Indexer		= 0							;
						//.............................................
						this.Transactions		= new	ConcurrentDictionary< int, BDC_Transaction >()	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private						int			_Indexer;
				private readonly	object	_Lock		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	ConcurrentDictionary< int, BDC_Transaction >	Transactions		{ get; }
				public	DTO_SessionOptions														SessionOptions	{ get; }
				public	DTO_SessionHeader															SessionHeader		{ get; }
				//.................................................
				public	int	TransactionCount	{ get { return	this.Transactions.Count	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDC_Transaction CreateTransaction( Guid ID = default(Guid) )
					{
						return	new BDC_Transaction( ID );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction( IList< BDC_Transaction > transactions )
					{
						foreach (BDC_Transaction lo_Tran in transactions)
							{
								this.AddTransaction(lo_Tran);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddTransaction( BDC_Transaction transaction )
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

		}
}
