using System;
using System.Threading;
//.........................................................
using BxS_WorxIPX.Helpers;
using BxS_WorxIPX.BDC;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Main
{
	public sealed class IPXController : IIPXController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public static IPXController Instance
					{
						get { return _Instance.Value; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IPXController()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	static readonly	Lazy< IPXController >	_Instance
					= new Lazy< IPXController >(	()=>		new IPXController()
																					, LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				public	IO					CreateIO					()=> new IO()					;
				public	Serializer	CreateSerializer	()=> new Serializer()	;
				//.................................................
				public	IBDCSessionRequest	CreateBDCSessionRequest	()=> new BDCSessionRequest()	;
				public	IBDCSessionResult		CreateBDCSessionResult	()=> new BDCSessionResult()		;

			#endregion

		}
}