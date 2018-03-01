using System;
using System.Security;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public interface IBDCSession
		{
			#region "Properties"

				Guid		SAPGUIID		{ get; }
				//.................................................
				string	Client			{ set; }
				string	Language		{ set; }
				string	User				{ set; }
				string	Password		{ set; }
				//.................................................
				SecureString SecurePassword	{ set; }
				//.................................................
				bool	IsConnected		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"


			#endregion

		}
}