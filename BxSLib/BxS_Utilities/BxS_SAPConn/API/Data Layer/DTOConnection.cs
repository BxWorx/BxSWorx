using System;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPConn.API
{
	[DataContract]

	internal class DTOConnection : IDTOConnection
		{
			#region "Properties"

				[DataMember]	public Guid		ID							{ get; set; }
				//.................................................
				[DataMember]	public string	AppServer				{ get; set; }
				[DataMember]	public int		InstanceNo			{ get; set; }
				[DataMember]	public string	RouterPath			{ get; set; }
				[DataMember]	public string	ServiceName			{ get; set; }
				[DataMember]	public string	SystemID				{ get; set; }
				[DataMember]	public bool		LowSpeed				{ get; set; }
				//.................................................
				[DataMember]	public bool		SNC_Active			{ get; set; }
				[DataMember]	public string	SNC_PartnerName	{ get; set; }
				[DataMember]	public int		SNC_QOP					{ get; set; }
				[DataMember]	public bool		SNC_UsrPwd			{ get; set; }
				//.................................................
				[DataMember]	public Guid		MSID						{ get; set; }
				[DataMember]	public string MSName					{ get; set; }
				[DataMember]	public string MSHost					{ get; set; }
				[DataMember]	public string MSPort					{ get; set; }
				[DataMember]	public string MSDescription		{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTOConnection()
					{
						this.ID								= Guid.Empty;
						//..............................................
						this.AppServer				= string.Empty;
						this.InstanceNo				= default(int);
						this.RouterPath				= string.Empty;
						this.ServiceName			= string.Empty;
						this.SystemID					= string.Empty;
						this.LowSpeed					= false;
						//..............................................
						this.SNC_Active				= false;
						this.SNC_PartnerName	= string.Empty;
						this.SNC_QOP					= default(int);
						this.SNC_UsrPwd				= false;
						//..............................................
						this.MSID							=	Guid.Empty;
						this.MSName						= string.Empty;
						this.MSHost						= string.Empty;
						this.MSPort						= string.Empty;
						this.MSDescription		= string.Empty;
					}

			#endregion
		}
}
