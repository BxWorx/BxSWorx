using System.Collections.Concurrent;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.BDCSession.DTO
{
	public class DTO_BDC_Session
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Session( DTO_BDC_Header header )
					{
						this.Header	= header;
						//.............................................
						this.Trans	= new	ConcurrentQueue< DTO_BDC_Transaction >();
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	bool	UseSessionConfig	{ get; set; }
				//.................................................
				internal	DTO_BDC_SessionConfig		SessionConfig				{ get; set; }
				internal	IConfigDestination			DestinationConfig		{ get; set; }
				//.................................................
				internal	DTO_BDC_Header													Header	{ get; }
				internal	ConcurrentQueue< DTO_BDC_Transaction >	Trans		{ get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_BDC_Transaction	CreateTransDTO( int No = 0 )=>	new DTO_BDC_Transaction( No );

			#endregion

		}
}
