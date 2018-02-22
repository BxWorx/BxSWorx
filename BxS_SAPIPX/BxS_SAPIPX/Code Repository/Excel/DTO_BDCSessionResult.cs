using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Excel
{
	[DataContract()]
	public class DTO_BDCSessionResult
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDCSessionResult()
					{
						this.BDCMessages	= new	Dictionary< object , string >();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				[DataMember]	public	string	SAPID		{ get; set; }
				[DataMember]	public	string	Client	{ get; set; }
				[DataMember]	public	string	User		{ get; set; }
				[DataMember]	public	string	Lang		{ get; set; }

				[DataMember]	public	string	WBID		{ get; set; }
				[DataMember]	public	string	WSID		{ get; set;	}
				[DataMember]	public	string	WSNo		{ get; set;	}
				//.................................................
				[DataMember]	public	Dictionary< object , string >		BDCMessages	{ get; set; }

			#endregion

		}
}
