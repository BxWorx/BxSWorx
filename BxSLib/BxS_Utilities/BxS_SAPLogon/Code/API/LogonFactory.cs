using System;
using System.Collections.Generic;
using System.Threading;
//.........................................................
using BxS_SAPConn.API;
using BxS_Toolset;
using BxS_Toolset.DataContainer;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public class LogonFactory
		{
			#region "Constructors"

				private readonly ConnFactory	_ConnFac	= new ConnFactory();

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy<ToolSet>	_TS		= new Lazy<ToolSet>(	() => new ToolSet()	,
																																			LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IFavourites CreateFavourite(	string	fullPathName						,
																						int			MaximumEntries	= 3			,
																						bool		AutoLoad				= true	,
																						bool		AutoSave				= true		)
					{
						return	new Favourites(	this.CreateDTCntlr(	fullPathName, AutoLoad)	,
																		MaximumEntries															,
																		AutoLoad																		,
																		AutoSave																			);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private DTController<IDTOFavourite, Guid> CreateDTCntlr(string fullPathName, bool autoLoad)
					{
						var lt_Types	= new List<Type>	{	typeof(DTOFavourite)						,
																							this._ConnFac.GetDTOConnType()		};

						return	this._TS.Value.CreateDTController<IDTOFavourite, Guid>	(	fullPathName																	,
																																							(Guid ID) => new DTOFavourite()	{ UUID = ID } ,
																																							lt_Types																			,
																																							autoLoad																				);
					}

			#endregion
		}
}
