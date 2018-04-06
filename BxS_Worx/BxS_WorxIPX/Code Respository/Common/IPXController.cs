using System;
//.........................................................
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.Helpers.ObjectPool;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class IPXController : IIPXController
		{
			#region "Constructors: Singleton"

				private IPXController()	{	}
				private	static readonly	Lazy< IPXController >	_Instance	= new		Lazy< IPXController >( ()=>	new IPXController() , cz_LM );
				public	static IPXController Instance	{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IExcelBDCSession_Parser		CreateBDCSessionParser	()=> new ExcelBDCSession_Parser	();
				public	IExcelBDCSessionWS				CreateBDCSessionWS			()=> new ExcelBDCSessionWS			();
				public	IExcelBDCSessionRequest		CreateBDCSessionRequest	()=> new ExcelBDCSessionRequest	();
				public	IExcelBDCSessionResult		CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();

				//public	IO					CreateIO					()=> new IO()					;
				//public	Serializer	CreateSerializer	()=> new Serializer()	;
				////.................................................
				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public PriorityQueue<T> CreatePriorityQueue<T>() where T: class		=>	new PriorityQueue<T>();

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public	ObjectPool<T> CreateObjectPool<T>(	int			MinSize				= 1
				//																					,	int			MaxSize				= 5
				//																					, bool		diagnostics		= false
				//																					,	Func<T>	func					= null	)	where T: PooledObject
				//	{
				//		return	new ObjectPool<T>( MinSize , MaxSize , diagnostics , func );
				//	}

			#endregion

		}
}