using System;
using System.Collections.Generic;
using System.Text;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public class DTOConnection
		{
			#region "Properties"

				public string	ID							{ get; set; }
				public string	Name						{ get; set; }
				public string	AppServer				{ get; set; }
				public int		InstanceNo			{ get; set; }
				public string	SystemID				{ get; set; }
				public string	RouterPath			{ get; set; }
				public bool		SNC_Active			{ get; set; }
				public string	SNC_PartnerName	{ get; set; }
				public bool		SNC_UsrPwd			{ get; set; }
				public int		SNC_QOP					{ get; set; }
				public bool		LowSpeed				{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTOConnection()
					{
						this.ID								= string.Empty;
						this.Name							= string.Empty;
						this.AppServer				= string.Empty;
						this.SystemID					= string.Empty;
						this.RouterPath				= string.Empty;
						this.SNC_PartnerName	= string.Empty;

						this.SNC_Active				= false;
						this.SNC_UsrPwd				= false;
						this.LowSpeed					= false;

						this.InstanceNo				= default(int);
						this.SNC_QOP					= default(int);
					}

			#endregion
		}
}
