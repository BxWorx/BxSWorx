﻿using System;
//.........................................................
using BxS_WorxUtil.General;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.Main
{
	public interface IUTL_Controller
		{
			#region "Properties"

				IO					IO					{ get; }
				Serializer	Serializer	{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				TopTenList<T>				CreateTopTenList<T>				( int size = 10 )																								where T: class				;
				PriorityQueue<T>		CreatePriorityQueue<T>		()																															where T: class				;
				ObjectPool<T>				CreateObjectPool<T>				(	Func<T>	createConsumer	= null )															where T: PooledObject	;
				ProgressHandler<T>	CreateProgressHandler<T>	(	Func<T>	factory					= null ,	int reportInterval	= 10 )	where T:class					;

			#endregion

		}
}