using System						;
using System.Threading	;
//.........................................................
using BxS_SAPNCO.Common				;
using BxS_SAPNCO.Destination	;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal partial class NCOController_BDC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal NCOController_BDC()
					{	}

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private readonly	Lazy<BDCOpFnc>	_OpFnc	= new	Lazy<BDCOpFnc>
				//																								(	() => CreateBDCOpFnc()
				//																									, LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Properties"

				//internal	BDCOpFnc	OpFnc	{ get {	return	this._OpFnc.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal IBDCSession CreateBDCSession(	DestinationRfc	destRFC
				//																			,	string					FncName	)
				//	{
				//		return	null;
				//		//IBDCProfile	lo_Prof		= this.GetAddBDCProfile	( destRFC	, FncName );
				//		//BDCOpEnv		lo_OpEnv	= this.CreateBDCOpEnv		( destRFC , lo_Prof	);
				//		////.............................................
				//		//return	new BDCSession(	this._OpFnc	, lo_OpEnv );
				//	}

			#endregion

		}
}
