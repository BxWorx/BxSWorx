using System;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPGUI.API
{
	public interface IFavourites
		{
			#region "Properties"

				IList<IDTOFavourite>	Favourites	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				IDTOFavourite CreateFavourite();
				//.................................................
				void AddFavorite(IDTOFavourite DTO);

			#endregion

		}
}