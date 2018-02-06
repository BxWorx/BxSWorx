//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public class DTO_SessionOptions
		{
			#region "Documentation"
			#endregion

			//===========================================================================================
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_SessionOptions(	int	NoOfConsumers		=	1	,
																		int TimeInterval		=	0	)
					{
						this.NoOfConsumers	=	NoOfConsumers	;
						this.TimeInterval	=	TimeInterval	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	NoOfConsumers		{	get; set; }
				public	int	TimeInterval		{	get; set; }

			#endregion

		}
}
