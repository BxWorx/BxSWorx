//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	public class DTO_ProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_ProgressInfo()
					{
						this.Processed	= 0;
						this.Successful	= 0;
						this.Faulty			= 0;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	Processed		{ get; set; }
				public	int	Successful	{ get; set; }
				public	int	Faulty			{ get; set; }

			#endregion

		}
}
