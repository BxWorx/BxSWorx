using System;
//.........................................................
using BxS_WorxIPX.Main;
using BxS_WorxUtil.Main;

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

				internal	IIPX_Controller		IPX_Cntlr		{ get	{	return	IPX_Controller.Instance	; } }
				internal	IUTL_Controller		UTL_Cntlr		{ get	{	return	UTL_Controller.Instance	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"
			#endregion

		}
}