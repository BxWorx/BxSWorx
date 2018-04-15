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
				private	static readonly	Lazy< IPX_Controller >	_Instance		= new		Lazy< IPX_Controller >( ()=>	new IPX_Controller() , cz_LM );
				public	static					IPX_Controller					Instance	{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPX_Controller()
					{
						this._WSParser	= new	Lazy< ExcelBDC_Parser >( ()=>	new	ExcelBDC_Parser() );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy< ExcelBDC_Parser >	_WSParser;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IExcelBDC_WS				CreateBDCSessionWS			()	=> new ExcelBDC_WS			();
				public	IExcelBDC_Request		CreateBDCSessionRequest	()	=> new ExcelBDC_Request	();

				public	IExcelBDCSessionResult		CreateBDCSessionResult	()=> new ExcelBDCSessionResult	();





				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IExcelBDC_Request ParseWStoRequest( IExcelBDC_WS ws )
					{
						IExcelBDC_Request	lo_Req	= this.CreateBDCSessionRequest();
						this._WSParser.Value.ParseWStoRequest( ws , lo_Req );
						return	lo_Req;
					}

			#endregion

		}
}