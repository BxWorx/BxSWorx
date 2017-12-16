using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.ObjectPool
{
	public interface IPoolObject
		{
		 Task<bool>	ResetAsync();
		}
}
