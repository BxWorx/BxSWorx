using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_Lines
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_Lines()
					{
						this.ProcessedStatus	= false	;
						this.SuccesStatus			= false	;
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				public  int   Reference				{ get; set; }
				public	bool	ProcessedStatus	{ get; set;	}
				public	bool	SuccesStatus		{ get; set;	}
				//.................................................
				public	SMC.IRfcTable		BDCData	{ get; set; }
				public	SMC.IRfcTable		SPAData	{ get; set; }
				public	SMC.IRfcTable		MSGData	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Reset()
					{
						this.ProcessedStatus	= false;
						this.SuccesStatus			= false;
						//.............................................
						this.BDCData.Clear();
						this.SPAData.Clear();
						this.MSGData.Clear();
					}

			#endregion

		}
}
