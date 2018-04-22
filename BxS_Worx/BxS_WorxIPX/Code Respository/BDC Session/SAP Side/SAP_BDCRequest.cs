using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//.........................................................
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.SAPBDCSession
{
	[DataContract()]
	[KnownType( typeof(SAP_Logon)				)]
	[KnownType( typeof(SAP_BDCSession)	)]

	public class SAP_BDCRequest : ISAP_BDCRequest
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAP_BDCRequest()
					{
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