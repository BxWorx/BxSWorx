//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class BDCProgressInfo
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BDCProgressInfo()
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
