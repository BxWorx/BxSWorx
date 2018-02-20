using System.Threading.Tasks;
//.........................................................
using BxS_SAPIPC.BDCData;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPBDC.Parser
{
	public partial class BDC_Processor
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
				public async Task< IPC_BDCSession > Process( string[,] data )
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
						if ( await this.ParseForTokens( lo_BDCSession , data ).ConfigureAwait(false) )
							{
								
							}
						else
							{	return	null; }
						//.............................................
						return	lo_IPCSession;
					}
			#endregion

			//===========================================================================================
			#region "Methods: Private"
			#endregion

		}
}
