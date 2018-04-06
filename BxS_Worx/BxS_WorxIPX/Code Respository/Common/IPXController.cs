using System;
using System.Threading;
//.........................................................
using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.Helpers.ObjectPool;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class IPXController : IIPXController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IPXController Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPXController()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< IPXController >	_Instance

					= new Lazy< IPXController >(	()=>		new IPXController()
																							, LazyThreadSafetyMode.ExecutionAndPublication );

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