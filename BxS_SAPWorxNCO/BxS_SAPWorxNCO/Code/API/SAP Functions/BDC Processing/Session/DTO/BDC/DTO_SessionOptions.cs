//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class DTO_SessionOptions
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionOptions(	int		NoOfConsumers			=	1			,
																		int		TimeInterval			=	0			,
																		int		progressInterval	= 10		,
																		int		queueTimeout			= 10		,
																		bool	sequential				= false		)
					{
						this.NoOfConsumers		=	NoOfConsumers			;
						this.PauseTime				=	TimeInterval			;
						this.ProgressInterval	=	progressInterval	;
						this.QueueAddTimeout	=	queueTimeout			;
						this.Sequential				=	sequential				;
					}

			#endregion

			//===========================================================================================
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
