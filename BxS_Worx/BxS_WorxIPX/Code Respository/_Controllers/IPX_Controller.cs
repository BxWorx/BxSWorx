using System;
//.........................................................
using BxS_WorxIPX.BDC;

using static	BxS_WorxIPX.Main.IPX_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class IPX_Controller : IIPX_Controller
		{
			#region "Constructors: Singleton"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPX_Controller()
					{	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	static readonly	Lazy< IPX_Controller >	_Instance	= new		Lazy< IPX_Controller >( ()=>	new IPX_Controller() , cz_LM );
				public	static					IPX_Controller					Instance	{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IExcelBDCSession_Parser		CreateBDCSessionParser	()=> new ExcelBDCSession_Parser	();
				public	IExcelBDCSessionWS				CreateBDCSessionWS			()=> new ExcelBDCSessionWS			();
				public	IExcelBDCSessionRequest		CreateBDCSessionRequest	()=> new ExcelBDCSessionRequest	();
				public	IExcelBDCSessionResult		CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();

			#endregion

		}
}