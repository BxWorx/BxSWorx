using System;
//.........................................................
using BxS_SAPNCO.BDCProcess;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API
{
	public partial class NCOController
		{
			#region "Methods: Exposed: BDC Processing"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBDCSession CreateBDCSession(string destinationID)
					{
						Guid lg_ID	= this._Cntlr_Dest.Value.Repository.GetAddIDFor( destinationID );
						//.............................................
						return	this.CreateBDCSession(lg_ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IBDCSession CreateBDCSession(Guid destinationID)
					{
						DestinationRfc lo_DestRfc = this._Cntlr_Dest.Value.CreateDestinationRFC(destinationID);
						//.............................................
						return	this._Cntlr_BDC.Value.CreateBDCSession( lo_DestRfc , _SAPFncConst.Value.BDCCallTran );
					}

			#endregion

		}
}
