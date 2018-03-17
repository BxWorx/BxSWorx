//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_SessionConfig
		{
			#region "Properties"

				public	int	NoOfConsumers			{	get; set; }
				public	int	PauseTime					{	get; set; }
				public	int ProgressInterval	{	get; set; }
				public	int	QueueAddTimeout		{	get; set; }
				//.................................................
				public	bool	Sequential	{	get; set; }

			#endregion

		}
}
