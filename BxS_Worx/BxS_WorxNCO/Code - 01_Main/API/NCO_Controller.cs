using System;
//.........................................................
using BxS_WorxNCO.Destination.API;

using BxS_WorxNCO.BDCSession.Main;
using BxS_WorxNCO.BDCSession.DTO;

using BxS_WorxIPX.Main;
using BxS_WorxUtil.Main;
using BxS_WorxUtil.Progress;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.API
{
	public sealed class NCO_Controller : INCO_Controller
		{
			#region "Constructors: Singleton"

				private NCO_Controller()	{	}
				//.................................................
				private	static readonly		Lazy< NCO_Controller >	_Instance		= new	Lazy< NCO_Controller >( ()=> new NCO_Controller() , cz_LM );
				public	static						NCO_Controller					Instance			{	get { return _Instance.Value; }	}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IIPX_Controller		IPX_Cntlr		{ get	{	return	IPX_Controller.Instance	; } }
				public	IUTL_Controller		UTL_Cntlr		{ get	{	return	UTL_Controller.Instance	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Session Handling"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	BDC_SessionManager										CreateBDCSessionManager					( IRfcDestination rfcDestination )	=>	new BDC_SessionManager( new BDC_SessionFactory( rfcDestination ) );
				public	ProgressHandler< DTO_BDC_Progress >		CreateBDCSessionProgressHandler	()																	=>	this.UTL_Cntlr.CreateProgressHandler< DTO_BDC_Progress >( this.CreateProgress );

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	DTO_BDC_Progress CreateProgress	()=>	new	DTO_BDC_Progress();

			#endregion

		}
}