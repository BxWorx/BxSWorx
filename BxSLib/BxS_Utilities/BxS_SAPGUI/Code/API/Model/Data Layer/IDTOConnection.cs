using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public interface IDTOConnection
		{
			#region "Properties"

				Guid		ID							{ get; set; }
				//.................................................
				string	AppServer				{ get; set; }
				int			InstanceNo			{ get; set; }
				string	RouterPath			{ get; set; }
				string	ServiceName			{ get; set; }
				string	SystemID				{ get; set; }
				bool		LowSpeed				{ get; set; }
				//.................................................
				bool		SNC_Active			{ get; set; }
				string	SNC_PartnerName	{ get; set; }
				int			SNC_QOP					{ get; set; }
				bool		SNC_UsrPwd			{ get; set; }
				//.................................................
				Guid		MSID						{ get; set; }
				string	MSDescription		{ get; set; }
				string	MSHost					{ get; set; }
				string	MSName					{ get; set; }
				string	MSPort					{ get; set; }

			#endregion

		}
}