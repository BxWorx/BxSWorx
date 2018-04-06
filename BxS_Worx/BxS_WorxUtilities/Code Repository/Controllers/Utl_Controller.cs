using System;
using System.Threading;
//.........................................................
using BxS_WorxUtil.General;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUTL.Main
{
	public sealed class Utl_Controller : IUtl_Controller
		{
			#region "Constructors: Singleton"

				private Utl_Controller()	{	}
				//.................................................
				private	static readonly	Lazy< Utl_Controller >	_Instance	= new		Lazy< Utl_Controller >( ()=>		new Utl_Controller()
																																				, LazyThreadSafetyMode.ExecutionAndPublication );

				public static Utl_Controller Instance	{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IO					CreateIO					()=> new IO()					;
				public	Serializer	CreateSerializer	()=> new Serializer()	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public PriorityQueue		<T> CreatePriorityQueue		<T>	()																							where T: class				=>	new PriorityQueue		<T>();
				public ObjectPool				<T>	CreateObjectPool			<T>	(	Func<T>	factory	= null )											where T: PooledObject	=>	new ObjectPool			<T>( factory );
				public ProgressHandler	<T>	CreateProgressHandler	<T>	(	Func<T>	factory ,	int reportInterval	= 10 )	where T: class				=>	new ProgressHandler	<T>( factory , reportInterval );

			#endregion

		}
}