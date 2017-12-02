using System;
//.........................................................
using BxS_Toolset.Serialize;
using BxS_Toolset.IODisk;
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public class Factory
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IFavourites CreateFavourite(string fullPathName)
					{
						return	new Favourites(this.CreateFavouriteDC(fullPathName));
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DCController<IDTOFavourite, Guid> CreateFavouriteDC(string fullPathName)
					{
						var	lo_IO		= new IO();
						var	lo_Ser	= new ObjSerializer();
						//.............................................
						return	new DCController<IDTOFavourite, Guid>	(lo_IO, lo_Ser, fullPathName, (Guid ID) => new DTOFavourite()	{ UUID = ID } );
					}

			#endregion
		}
}
