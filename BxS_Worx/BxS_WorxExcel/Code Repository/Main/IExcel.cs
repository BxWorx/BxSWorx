using System.Collections.Generic;
//.........................................................
using Microsoft.Office.Interop.Excel;
//.........................................................
using BxS_WorxExcel.DTO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.Main
{
	internal interface IExcel
		{
			#region "Properties"

				string	CurrentAddress { get; }

				Worksheet	GetActiveWorksheet();
				Worksheet	AddWorksheet();

				IList<DTO_WSNode>	GetManifest();
				DTO_WSNode				GetActiveWSNode();
				DTO_WSData				GetWSData( DTO_WSNode wsNode );

				void WriteConfig( string xml , string address = "$A$1" );

				void	ScreenUpdating( bool AsOn = true );

			#endregion

		}
}
