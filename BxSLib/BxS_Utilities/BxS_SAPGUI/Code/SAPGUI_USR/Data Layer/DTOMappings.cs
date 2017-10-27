using System;
using System.Collections.Generic;
using System.Reflection;
//.........................................................
using SAPGUI.API.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
	internal static class DTOMappings<T> where T : class
		{
			#region "Declarations"

				private static readonly Lazy<Dictionary<string, string>>	_Map
									=	new Lazy<Dictionary<string, string>>(	() => LoadServicesMap()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

				private	static readonly Lazy<PropertyInfo[]>	_Properties
							=	new Lazy<PropertyInfo[]>( () => typeof(DTOService).GetProperties()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal static Dictionary<string, string>	Map					{ get { return _Map.Value;	} }
				internal static PropertyInfo[]							Properties	{ get { return _Properties.Value;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static Dictionary<string, string> LoadServicesMap()
					{
						var lt_Map = new Dictionary<string, string>
							{
								{ "UUID", "UUID" }
							};

						return	lt_Map;
					}

			#endregion

		}
}
