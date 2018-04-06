using System;
using System.Threading;
//.........................................................
using BxS_WorxUtil.Helpers;
using BxS_WorxUtil.Helpers.ObjectPool;
using BxS_WorxUtil.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class Util_Controller : IUtil_Controller
		{
			#region "Constructors: Singleton"

				private	static readonly	Lazy< Util_Controller >	_Instance	= new		Lazy< Util_Controller >( ()=>		new Util_Controller()
																																				, LazyThreadSafetyMode.ExecutionAndPublication );

				private Util_Controller()	{	}

				public static Util_Controller Instance	{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IO					CreateIO					()=> new IO()					;
				public	Serializer	CreateSerializer	()=> new Serializer()	;
				//.................................................
				public	IExcelBDCSession_Parser		CreateBDCSessionParser	()=> new ExcelBDCSession_Parser	();
				public	IExcelBDCSessionWS				CreateBDCSessionWS			()=> new ExcelBDCSessionWS			();
				public	IExcelBDCSessionRequest		CreateBDCSessionRequest	()=> new ExcelBDCSessionRequest	();
				public	IExcelBDCSessionResult		CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public PriorityQueue<T> CreatePriorityQueue<T>() where T: class
					{
						return	new PriorityQueue<T>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	ObjectPool<T> CreateObjectPool<T>(	int			MinSize				= 1
																									,	int			MaxSize				= 5
																									, bool		diagnostics		= false
																									,	Func<T>	func					= null	)	where T: PooledObject
					{
						return	new ObjectPool<T>( MinSize , MaxSize , diagnostics , func );
					}

			#endregion

		}
}