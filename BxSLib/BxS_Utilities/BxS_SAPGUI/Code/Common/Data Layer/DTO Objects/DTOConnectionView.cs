using System;
//.........................................................
using BxS_SAPGUI.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.COM.DL
{
	public class DTOConnectionView : IDTOConnectionView
		{
			#region "Properties"

				public string	HierID				{ get; set; }
				public string	HierID_Parent	{ get; set; }
				public string	Description		{ get; set; }
				public Guid		SAPID					{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOConnectionView(string id, string description, string parent	= default(string), Guid sapid = default(Guid) )
					{
						this.HierID					= id					;
						this.HierID_Parent	= parent			;
						this.Description		= description	;
						this.SAPID					= sapid				;
					}

			#endregion

		}
	}
