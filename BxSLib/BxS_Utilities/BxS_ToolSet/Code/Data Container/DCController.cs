using System;
using System.Collections.Generic;
//.........................................................
using BxS_Toolset.IODisk;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.DataContainer
{
	public class DCController<TCls, TKey> where TCls : class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DCController(IO								io						,
														ObjSerializer			serializer		,
														string						fullPathName	,
														Func<TKey, TCls>	newEntry			,
														List<Type>				knownTypes			)
					{
						this._IO						= io						;
						this._Serializer		= serializer		;
						this._FullPathName	= fullPathName	;
						this._KnownTypes		= knownTypes		;
						//.............................................
						this.DataTable	= new DCTable<TCls, TKey>( newEntry );
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

				public DCTable<TCls, TKey>	DataTable { get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Save()
					{
						bool lb_Ret	= true;
						//.............................................
						if (this.DataTable.IsDirty)
							{
								try
									{
										this._IO.WriteFile(	this._FullPathName															,
																				this._Serializer.Serialize(	this.DataTable	,
																																		this._KnownTypes	)		);
									}
								catch (Exception)
									{	lb_Ret	=	false; }
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Load()
					{
						if (this._IO.FileExists(this._FullPathName))
							{
								DCTable<TCls, TKey> lo_DTO	= this._Serializer
																								.DeSerialize<DCTable<TCls, TKey>>
																									(this._IO.ReadFile(this._FullPathName));
								lo_DTO.TransferTo(this.DataTable);
							}
					}

			#endregion

		}
}