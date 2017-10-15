//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.API
{
	internal class DTOConnectionView : IDTOConnectionView
		{
			#region "Properties"

				public string ID						{ get; set; }
				public string GroupID				{ get; set; }
				public string SAPID					{ get; set; }
				public string	Description		{ get; set; }

			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTOConnectionView()
					{
						this.ID						= string.Empty;
						this.GroupID			= string.Empty;
						this.SAPID				= string.Empty;
						this.Description	= string.Empty;
					}

			#endregion

		}
	}
