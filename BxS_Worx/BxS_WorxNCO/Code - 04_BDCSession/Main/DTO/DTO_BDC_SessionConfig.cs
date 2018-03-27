//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.DTO
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

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void Configure( DTO_BDC_SessionConfig dto )
					{
						this.IsSequential				= dto.IsSequential			;
						//.................................................
						this.ConsumersNo				= dto.ConsumersNo				;
						this.ConsumersMax				= dto.ConsumersMax			;
						this.ConsumerThreshold	= dto.ConsumerThreshold	;
						//.................................................
						this.PauseTime					= dto.PauseTime					;
						this.ProgressInterval		= dto.ProgressInterval	;
						this.QueueAddTimeout		= dto.QueueAddTimeout		;
					}

			#endregion

		}
}
