using System.Threading.Tasks;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Helpers
{
	public interface IPoolObject
		{
			int	Position	{ get; set; }

			bool	Reset();
		}
}
