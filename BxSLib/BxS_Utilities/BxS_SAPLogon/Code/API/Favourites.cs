using System;
using System.Collections.Generic;
//.........................................................
using BxS_Toolset.DataContainer;
using BxS_Toolset.IO;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPLogon.API
{
	public class Favourites : IFavourites
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Favourites(	IO						io							,
															ObjSerializer serializer			,
															string				fullPathNameLNK		)
					{
						this._IO							= io;
						this._Serializer			= serializer;
						this._FullPathNameLNK	= fullPathNameLNK;
						//.............................................
						this._Favourites	= new DCTable<IDTOFavourite	, Guid>	( (Guid ID) => new DTOFavourite	()	{ UUID	= ID } );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private						bool					_IsDirty					;
				private readonly	IO						_IO								;
				private readonly	ObjSerializer	_Serializer				;
				private readonly	string				_FullPathNameLNK	;
				//.................................................
				private readonly	DCTable<IDTOFavourite, Guid>	_Favourites;

			#endregion

			//===========================================================================================
			#region "Properties"

				public IList<IDTOFavourite>	List	{ get { return	this._Favourites.ValueListFor(); } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IDTOFavourite Create(Guid ID = default(Guid))
					{
						return	this._Favourites.Create(ID);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void Add(IDTOFavourite DTO)
					{
						this._Favourites.AddUpdate(DTO.UUID, DTO);
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void SaveLink()
					{
						if (this._IsDirty)
							{
								this._IO.WriteFile(	this._FullPathNameLNK													,
																		this._Serializer.Serialize(this._Favourites)		);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void LoadLink()
					{
						if (this._IO.FileExists(this._FullPathNameLNK))
							{
								DCTable<IDTOFavourite, Guid> lo_DTO	= this._Serializer
																												.DeSerialize<DCTable<IDTOFavourite, Guid>>
																													(this._IO.ReadFile(this._FullPathNameLNK));
								lo_DTO.Reload(this._Favourites);
							}
					}

			#endregion

		}
}