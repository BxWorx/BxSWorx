using System;
//.........................................................
using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.BDC;
using BxS_WorxIPX.Helpers.ObjectPool;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPXController
		{
			#region "Methods: Exposed"

				//IO								CreateIO();
				//Serializer				CreateSerializer();

				//PriorityQueue<T>	CreatePriorityQueue<T>	()								where T: class;

				//ObjectPool<T>			CreateObjectPool<T>			(		int			MinSize				= 1
				//																						,	int			MaxSize				= 5
				//																						, bool		diagnostics		= false
				//																						,	Func<T>	func					= null	)	where T: PooledObject;
				////.................................................
				IExcelBDCSession_Parser		CreateBDCSessionParser	();
				IExcelBDCSessionWS				CreateBDCSessionWS			();
				IExcelBDCSessionRequest		CreateBDCSessionRequest	();
				IExcelBDCSessionResult		CreateBDCSessionResult	();

			#endregion

		}
}