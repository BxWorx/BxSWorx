//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	public interface IDTOConnection
		{
			#region "Properties"

				string	AppServer				{ get; set; }
				string	ID							{ get; set; }
				int			InstanceNo			{ get; set; }
				bool		IsValid					{ get; set; }
				bool		LowSpeed				{ get; set; }
				string	MSDescription		{ get; set; }
				string	MSHost					{ get; set; }
				string	MSID						{ get; set; }
				string	MSName					{ get; set; }
				string	MSPort					{ get; set; }
				string	RouterPath			{ get; set; }
				string	ServiceName			{ get; set; }
				bool		SNC_Active			{ get; set; }
				string	SNC_PartnerName	{ get; set; }
				int			SNC_QOP					{ get; set; }
				bool		SNC_UsrPwd			{ get; set; }
				string	SystemID				{ get; set; }

			#endregion

		}
}