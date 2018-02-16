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
			//===========================================================================================
			#region "Methods: Private"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//internal IBDCProfile GetAddBDCProfile(	DestinationRfc	destRFC
				//																			,	string					FncName	)
				//	{
				//		IBDCProfile	lo_Profile	= null;
				//		//.............................................
				//		destRFC.TryGetProfile( FncName , out object lo_ProfileObj	);

				//		if (lo_ProfileObj == null)
				//			{
				//				lo_Profile	= this.CreateBDCFncProfile(	destRFC	,	FncName );
				//				destRFC.RegisterProfile(lo_Profile);
				//				destRFC.TryGetProfile( FncName , out lo_ProfileObj );
				//			}
				//		//.............................................
				//		return	lo_Profile;
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private BDCOpEnv CreateBDCOpEnv(	DestinationRfc	destRFC
				//																,	IBDCProfile			profile	)
				//	{
				//		return	new BDCOpEnv(	destRFC, profile, this._OpFnc.Value	);
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private IBDCProfile CreateBDCFncProfile(	DestinationRfc	destRFC
				//																				,	string					FncName	)
				//	{
				//		return	new BDCFncProfile( destRFC , FncName );
				//	}

			#endregion

		}
}
