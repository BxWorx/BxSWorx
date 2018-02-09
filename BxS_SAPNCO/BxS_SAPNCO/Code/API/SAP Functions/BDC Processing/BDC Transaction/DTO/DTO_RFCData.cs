using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	public class DTO_RFCData
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTO_RFCData()
					{
						this.ProcessedStatus	= false	;
						this.SuccesStatus			= false	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	ProcessedStatus	{ get; set;	}
				public	bool	SuccesStatus		{ get; set;	}
				//.................................................
				public	string	SAPTCode	{ get;	set; }
				public	string	Skip1st		{ get;	set; }
				//.................................................
				public	SMC.IRfcStructure		CTUOpts	{ get;	set; }
				public	SMC.IRfcTable				BDCData	{ get;	set; }
				public	SMC.IRfcTable				SPAData	{ get;	set; }
				public	SMC.IRfcTable				MSGData	{ get;	set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this.BDCData.Clear();
						this.SPAData.Clear();
						this.MSGData.Clear();
					}

			#endregion

		}
}
