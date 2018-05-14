using BxS_WorxExcel.Code_Repository.UI.xTry;
using BxS_WorxExcel.MVVM;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class VM_SAPBDC : ViewModelBase
		{
			#region "Declarations"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal	VM_SAPBDC( MD_SAPBDC	model )
					{
						this.MyModel	=	model;
					}

			#endregion


				internal void	GetchSAPSessionList()
					{

					}



				internal	VW_SAPBDC		MyView	{	get; set; }
				internal	MD_SAPBDC		MyModel	{ get; }
		}
}
