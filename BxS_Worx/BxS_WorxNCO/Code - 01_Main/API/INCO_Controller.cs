using BxS_WorxIPX.Main;
using BxS_WorxUtil.Main;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API
{
	public interface INCO_Controller
		{

			IIPX_Controller	IPX_Cntlr	{ get; }
			IUTL_Controller	Utl_Cntlr	{ get; }

			//#region "Properties"

			//	int LoadedSystemCount	{ get; }

			//#endregion

			////===========================================================================================
			//#region "Methods: Exposed"

			//	IList< string >								GetSAPINIList();
			//	IList< ISAPSystemReference >	GetSAPSystems();
			//	//.................................................
			//	IRfcDestination	GetDestination( Guid ID		);
			//	IRfcDestination	GetDestination( string ID );
			//	//.................................................
			//	void LoadGlobalConfig( IConfigGlobal config );
			//	//.................................................
			//	IConfigDestination		CreateDestinationConfig()	;
			//	IConfigGlobal				CreateGlobalConfig()			;
			//	//.................................................
			//	void Reset();

			//#endregion

		}
}