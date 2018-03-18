//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.API.BDC
{
	public class DTO_BDC_SessionConfig
		{
			#region "Properties"

				public	bool	IsSequential		{	get; set; }
				//.................................................
				public	int		ConsumersNo				{	get; set; }
				public	int		ConsumersMax			{	get; set; }
				public  int		ConsumerThreshold	{	get; set; }
				//.................................................
				public	int		PauseTime					{	get; set; }
				public	int		ProgressInterval	{	get; set; }
				public	int		QueueAddTimeout		{	get; set; }

			#endregion

		}
}
