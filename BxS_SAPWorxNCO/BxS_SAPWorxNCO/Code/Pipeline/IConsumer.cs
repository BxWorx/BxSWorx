using System.Collections.Concurrent;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Pipeline
{
	public interface IConsumer<T>	where T:class
		{
			#region "Properties"

				int TotalProcessed	{ get; }
				//.................................................
				ConcurrentQueue<T>	Successful	{ get; }
				ConcurrentQueue<T>	Faulty			{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				void Start();

				bool Execute(T workItem);

			#endregion

		}
}
