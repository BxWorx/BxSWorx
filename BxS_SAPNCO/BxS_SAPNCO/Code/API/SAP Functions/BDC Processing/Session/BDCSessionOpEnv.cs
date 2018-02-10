using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC.Session
{
	public class BDCSessionOpEnv
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCSessionOpEnv(	DestinationRfc	destination	,
																	BDC2RfcParser		parser				)
					{
						this.Destination	= destination	;
						this.Parser				= parser			;
						//.............................................
						this._IsStarted	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	bool	_IsStarted;


			#endregion

			//===========================================================================================
			#region "Properties"

				internal	DestinationRfc	Destination	{	get; }
				internal	BDC2RfcParser		Parser			{	get; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				internal void Start()
					{
						if (this._IsStarted)	return;
						//.............................................

						this._IsStarted	= true;
					}

			#endregion

		}
}
