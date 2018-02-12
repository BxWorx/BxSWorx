//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.Helpers
{
	public interface IConsumerMaker<T>	where T : class
		{
			#region "Methods: Exposed"

				IConsumer<T>	CreateConsumer();

			#endregion

		}
}
