//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class DTO_CTUParams
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_CTUParams()
					{
						this.DisplayMode		=	BDCConstants.lz_CTU_N	;
						this.UpdateMode			=	BDCConstants.lz_CTU_A	;
						this.CATTMode				=	BDCConstants.lz_CTU_F	;
						this.DefaultSize		=	BDCConstants.lz_CTU_T	;
						this.NoCommit				=	BDCConstants.lz_CTU_F	;
						this.NoBatchInpFor	=	BDCConstants.lz_CTU_F	;
						this.NoBatchInpAft	=	BDCConstants.lz_CTU_F	;
				}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	char	DisplayMode			{	get; set; }
				public	char	UpdateMode			{	get; set; }
				public	char	CATTMode				{	get; set; }
				public	char	DefaultSize			{	get; set; }
				public	char	NoCommit				{	get; set; }
				public	char	NoBatchInpFor		{	get; set; }
				public	char	NoBatchInpAft		{	get; set; }

			#endregion

		}
}
