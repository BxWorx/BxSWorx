﻿using System.Collections.Generic;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.BDC
{
	public interface IRequest
		{
			#region "Properties"

				int	Count	{ get; }
				//...
				string	ID	{ get; set; }
				//...
				IUser						User			{ get; set; }
				ISAP_Logon			SAPLogon	{ get; set; }
				IRequest_Config	Config		{ get; set; }
				//...
				Dictionary<int , ISession>	Sessions	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void	Set_User		( IUser						user	 )	;
				void	Set_Logon		( ISAP_Logon			logon	 )	;
				void	Set_Config	( IRequest_Config	config )	;
				//...
				void	Add_Session	( ISession	session )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void	Sync	();		// Sync's the index with the loaded entries
				void	Clear	();

			#endregion
		}
}