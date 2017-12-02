using System;
using System.Threading;
//.........................................................
using BxS_Toolset.IODisk;
using BxS_Toolset.Serialize;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset
{
	public class IOToolSet
		{

			#region "Declarations"

				private readonly	Lazy<IO>						_IO		= new Lazy<IO>						( () => new IO()						,	LazyThreadSafetyMode.ExecutionAndPublication );
				private readonly	Lazy<ObjSerializer>	_Ser	= new Lazy<ObjSerializer>	( () => new ObjSerializer()	,	LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IO GetIO()
					{
						return	this._IO.Value;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjSerializer GetSerlizser()
					{
						return	this._Ser.Value;
					}

			#endregion

		}
}
