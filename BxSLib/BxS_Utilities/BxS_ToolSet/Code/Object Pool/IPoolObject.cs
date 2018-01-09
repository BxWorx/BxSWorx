using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.ObjectPool
{
	public interface IPoolObject
		{
			int	Position	{ get; set; }

			Task<bool>	ResetAsync	();
		}
}
