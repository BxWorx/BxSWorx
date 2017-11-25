using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public class DTOConnectionView : IDTOConnectionView
		{
			#region "Properties"

				public string	HierID				{ get; }
				public string	HierID_Parent	{ get; }
				public string	Description		{ get; }
				public Guid		SAPID					{ get; }

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOConnectionView()
					{
						this.HierID					= string.Empty	;
						this.HierID_Parent	= string.Empty	;
						this.Description		= string.Empty	;
						this.SAPID					= Guid.Empty		;
					}

			#endregion

		}
	}
