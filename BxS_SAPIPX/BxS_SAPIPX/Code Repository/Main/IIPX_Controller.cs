using BxS_SAPIPX.BDCData;
using BxS_SAPIPX.Excel;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPIPX.Main
{
	public interface IIPX_Controller
		{
			#region "Methods: Exposed"

				DTO_BDCSessionRequest		CreateBDCSessionRequest();
				DTO_BDCSessionResult		CreateBDCSessionResult();
				DTO_CTUParms						CreateCTUParms();
				//.................................................
				string	Serialize<T>		(T classObject);
				T				DeSerialize<T>	(string xmlString);
				void		WriteFile				(string FullPathFileName, string DataString);
				string	ReadFile				(string FullPathFileName);
				//.................................................
				void Parse1Dto2D( DTO_BDCSessionRequest DTO	, bool resetSource = true );
				void Parse2Dto1D( DTO_BDCSessionRequest DTO	, bool resetSource = true );

			#endregion

		}
}
