using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//.........................................................
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDCSAP
{
	[DataContract()]

	public class SAP_BDCRequest : ISAP_BDCRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAP_BDCRequest( ISAP_Logon	sapLogon )
					{
						this.SAPLogon		= sapLogon;
						this.Sessions		= new	Dictionary<Guid , ISAP_BDCSession>();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	ISAP_Logon	SAPLogon	{ get; set;	}
				//...
				[DataMember]	public	Dictionary<Guid , ISAP_BDCSession>		Sessions { get; set;	}

			#endregion

		}
}