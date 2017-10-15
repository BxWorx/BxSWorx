//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	internal class DTOConnection : IDTOConnection
		{
			#region "Properties"

				public string	ID							{ get; set; }
				public string	ServiceName			{ get; set; }
				public string	AppServer				{ get; set; }
				public int		InstanceNo			{ get; set; }
				public string	SystemID				{ get; set; }
				public string	RouterPath			{ get; set; }
				public bool		SNC_Active			{ get; set; }
				public string	SNC_PartnerName	{ get; set; }
				public bool		SNC_UsrPwd			{ get; set; }
				public int		SNC_QOP					{ get; set; }
				public bool		LowSpeed				{ get; set; }
				//.................................................
				public string MSID						{ get; set; }
				public string MSName					{ get; set; }
				public string MSHost					{ get; set; }
				public string MSPort					{ get; set; }
				public string MSDescription		{ get; set; }
				//.................................................
				public bool		IsValid					{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOConnection()
					{
						this.ID								= string.Empty;
						this.ServiceName			= string.Empty;
						this.AppServer				= string.Empty;
						this.SystemID					= string.Empty;
						this.RouterPath				= string.Empty;
						this.SNC_PartnerName	= string.Empty;

						this.MSID							= string.Empty;
						this.MSName						= string.Empty;
						this.MSHost						= string.Empty;
						this.MSPort						= string.Empty;
						this.MSDescription		= string.Empty;

						this.SNC_Active				= false;
						this.SNC_UsrPwd				= false;
						this.LowSpeed					= false;
						this.IsValid					= false;

						this.InstanceNo				= default(int);
						this.SNC_QOP					= default(int);
					}

			#endregion
		}
}
