using System;
//.........................................................
using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public interface IIPXController
		{
			#region "Methods: Exposed"

				IO								CreateIO();
				Serializer				CreateSerializer();

				PriorityQueue<T>	CreatePriorityQueue<T>	()								where T: class;
				//ObjectPool<T>			CreateObjectPool<T>			( Func<T> func )	where T: IPoolObject;
				//.................................................
				IExcelBDCSessionRequest		CreateBDCSessionRequest	();
				IExcelBDCSessionResult		CreateBDCSessionResult	();

			#endregion

		}
}