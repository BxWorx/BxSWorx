//.........................................................
using BxS_SAPIPC.BDCData;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	internal partial class BDC_Processor
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDC_Processor()
					{
					}

			#endregion

			//===========================================================================================
			#region "Declarations"
			#endregion

			//===========================================================================================
			#region "Properties"
			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IPC_BDCSession Process( string[,] data )
					{
						IPC_BDCSession	lo_IPCSession	= IPC_Controller.CreateSession();

						var	lo_BDCSession	= new DTO_BDCSession
							{
									RowLB		= data.GetLowerBound(0)
								,	RowUB		= data.GetUpperBound(0)	+ 1
								,	ColLB		= data.GetLowerBound(1)
								,	ColUB		= data.GetUpperBound(1)	+ 1
							};
						//.............................................
						this.LoadTokens(lo_BDCSession);

						//.............................................
						return	lo_IPCSession;
					}
			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
