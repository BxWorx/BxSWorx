using System;
using System.Reflection;
//.........................................................
using SAPGUI.API.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace SAPGUI.USR.DS
{
	internal static class DTOProperties
		{
			#region "Declarations"

				private	static readonly Lazy<PropertyInfo[]>	_SrvProp
							=	new Lazy<PropertyInfo[]>( () => typeof(DTOService).GetProperties()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

				private	static readonly Lazy<PropertyInfo[]>	_MsgProp
							=	new Lazy<PropertyInfo[]>( () => typeof(DTOMsgServer).GetProperties()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

				private	static readonly Lazy<PropertyInfo[]>	_WrkProp
							=	new Lazy<PropertyInfo[]>( () => typeof(DTOWorkspace).GetProperties()	,
																					System.Threading.LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				internal static PropertyInfo[]	Service		{ get { return _SrvProp.Value;	} }
				internal static PropertyInfo[]	MsgServer	{ get { return _MsgProp.Value;	} }
				internal static PropertyInfo[]	WorkSpace	{ get { return _WrkProp.Value;	} }

			#endregion

		}
}
