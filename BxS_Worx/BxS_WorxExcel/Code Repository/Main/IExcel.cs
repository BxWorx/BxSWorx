using Microsoft.Office.Interop.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	public interface IExcel
		{
			#region "Properties"

				string	CurrentAddress { get; }

				Worksheet	AddWorksheet();

			#endregion

		}
}
