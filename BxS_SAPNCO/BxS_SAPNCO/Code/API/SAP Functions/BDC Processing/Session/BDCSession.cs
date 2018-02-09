using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//.........................................................
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public class BDCSession	: IBDCSession
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCSession(	BDCSessionOpEnv			opEnv		,
														DTO_SessionOptions	options		)
					{
						this._OpEnv		= opEnv;
						this.Options	= options;
						//.............................................
						this.BDCTransactions	= new	ConcurrentQueue<IBDCTranData>	();
						this._BDCRfcTran			= new	ConcurrentQueue<DTO_RFCData>	();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	BDCSessionOpEnv	_OpEnv;

				private DTO_RFCSessionHeader						_RfcHeader	;
				private	ConcurrentQueue<DTO_RFCData>		_RfcTran		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	DestinationRfc	Destination		{	get;	}
				//.................................................
				public	int									Count						{ get { return	this.BDCTransactions.Count; } }
				public	DTO_SessionOptions	Options					{ get; set; }
				//.................................................
				public  DTO_BDCSessionHeader	SessionHeader	{ get; set; }
				//.................................................
				public	ConcurrentQueue<IBDCTranData>		BDCTransactions		{ get;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Process()
					{
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddBDCTransaction(IList<IBDCTranData> bdcTransactions)
					{
						foreach (IBDCTranData item in bdcTransactions)
							{
								this.BDCTransactions.Enqueue(item);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	AddBDCTransaction(IBDCTranData bdcTransaction)
					{
						this.BDCTransactions.Enqueue(bdcTransaction);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void	Reset()
					{
						while(!this.BDCTransactions.IsEmpty)
							{
								this.BDCTransactions.TryDequeue(out IBDCTranData lo);
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				private void Parse()
					{
						
						this._OpEnv.Parser.PutCTUOptions(this.SessionHeader.CTUOptions, this._RfcHeader.CTUOpts);

					}

			#endregion

		}
}
