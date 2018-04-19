using System;
using System.Collections.Generic;
//.........................................................
using BxS_Toolset.IODisk;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.DataContainer
{
	public class DTController<TCls, TKey> where TCls : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTController(IO								io						,
														ObjSerializer			serializer		,
														string						fullPathName	,
														Func<TKey, TCls>	newEntry			,
														List<Type>				knownTypes		,
														bool							autoLoad				= true	)
					{
						this._IO						= io						;
						this._Serializer		= serializer		;
						this._FullPathName	= fullPathName	;
						this._KnownTypes		= knownTypes		;
						//.............................................
						this.DataTable	= new DataTable<TCls, TKey>( newEntry );
						if (autoLoad)	this.Load();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	IO							_IO						;
				private readonly	ObjSerializer		_Serializer		;
				private readonly	string					_FullPathName	;
				private readonly	List<Type>			_KnownTypes		;

			#endregion

			//===========================================================================================
			#region "Properties"

				public DataTable<TCls, TKey>	DataTable { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Save(bool force = false)
					{
						bool lb_Ret	= true;
						//.............................................
						if (force || this.DataTable.IsDirty)
							{
								try
									{
										this._IO.WriteFile(	this._FullPathName													,
																				this._Serializer.Serialize(	this.DataTable	,
																																		this._KnownTypes	)	);
									}
								catch (Exception)
									{	lb_Ret	=	false; }
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Load()
					{
						bool lb_Ret	= false;
						//.............................................
						if (this._IO.FileExists(this._FullPathName))
							{
								try
									{
										DataTable<TCls, TKey> lo_DTab	= this._Serializer
																											.DeSerialize<DataTable<TCls, TKey>>
																												(	this._IO.ReadFile(this._FullPathName)	,
																													this._KnownTypes												);
										lo_DTab.TransferTo(this.DataTable);
										lb_Ret	= true;
									}
								catch (Exception)
									{	lb_Ret	=	false; }
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

		}
}