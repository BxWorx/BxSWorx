using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Worx.UI.Dashboard
{
	public static class ButtonFactory
		{
				private	static  readonly Lazy<Dictionary<string , Type>>
					_BtnTypes		= new	Lazy<Dictionary<string, Type>>();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static Dictionary<string , Type>	GetTypeCustomAttributeManifestOf<T>() where T:class
					{
						var lt	=	new Dictionary<string , Type>();
						//...
						foreach ( Type lo_Type in GetTypeTypesOf<T>() )
							{
								ButtonTypeAttribute z =	lo_Type.GetCustomAttribute<ButtonTypeAttribute>();
								lt.Add( z.BtnType , lo_Type );
							}
						return	lt;
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static List<string> GetAllEntities()
					{
						return	AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(x => x.GetTypes())
												.Where(x => typeof(IUC_BtnBase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
													.Select(x => x.Name).ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static List<Type> GetAll<T>() where T:class
					{
						return	AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(x => x.GetTypes())
												.Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
													.ToList();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static Dictionary<string , Type>	GetButtonTypes()
					{
						var x = new Dictionary<string , Type>();
						foreach ( Type lo_Type in GetAll<IUC_BtnBase>() )
							{
								ButtonTypeAttribute z =	lo_Type.GetCustomAttribute<ButtonTypeAttribute>();
								x.Add( z.BtnType , lo_Type );
							}
						return	x;
					}

		}
}
