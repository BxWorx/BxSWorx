using System;
using System.Threading;
//.........................................................
using BxS_Toolset;
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public class Factory
		{
			#region "Declarations"

				private readonly Lazy<ToolSet>	_TS		= new Lazy<ToolSet>(	() => new ToolSet()	,
																																			LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IFavourites CreateFavourite(	string	fullPathName						,
																						int			MaximumEntries	= 3			,
																						bool		Autoload				= true	,
																						bool		AutoSave				= true		)
					{
						return	new Favourites(this.CreateFavouriteDC(fullPathName), MaximumEntries, Autoload, AutoSave);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DCController<IDTOFavourite, Guid> CreateFavouriteDC(string fullPathName)
					{
						return	this._TS.Value.CreateDCController<IDTOFavourite, Guid>	(	fullPathName	,
																																							(Guid ID) => new DTOFavourite()	{ UUID = ID } );
					}

			#endregion
		}
}
