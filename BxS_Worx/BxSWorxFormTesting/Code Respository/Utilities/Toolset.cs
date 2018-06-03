using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.Dashboard.Utilities
{
	public static class Toolset
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static List<string> GetChangedProperties<T>(T objA, T objB) where T:class
					{
						if ( objA != null && objB != null )
							{
								if ( ! object.Equals( objA , objB ) )
									{
										PropertyInfo[]	allProperties		= objA.GetType().GetProperties(	BindingFlags.Public | BindingFlags.Instance );
										//...
										return	allProperties
															.Where	( p => !object.Equals( p.GetValue(objA) , p.GetValue(objB) ) )
															.Select	( p => p.Name )
															.ToList()	;
									}
							}
						//...
						return	new List<string>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static List<string> ClassNamesImplementingIFaceOf<T>() where T:class
					{
						return	TypesImplementingIFaceOf<T>()
											.Select		(	x => x.Name	)
											.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static List<Type> TypesImplementingIFaceOf<T>() where T:class
					{
						return	GetAsmTypes()
											.Where		(	x =>				typeof(T).IsAssignableFrom( x )
																				&&	!	x.IsInterface
																				&&	!	x.IsAbstract )
											.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static IEnumerable<Type> GetAsmTypes()
					{
						return	AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany	(	x => x.GetTypes()	);
					}

			#endregion

		}
}
