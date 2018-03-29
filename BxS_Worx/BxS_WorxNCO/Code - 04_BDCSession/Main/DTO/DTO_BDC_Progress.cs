//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.DTO
{
	public class DTO_BDC_Progress
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Progress( int total	= 100 )
					{
						this.TasksDne	= total;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				public int		TasksDne	{ get; set; }
				public int		TasksErr	{ get; set; }
				public int		TasksCan	{ get; set; }

				public string Msg				{ get; set; }

			#endregion

		}
}
