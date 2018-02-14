using BxS_SAPNCO.RfcFunction;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranProfile : RfcFncProfileBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProfile( string functionName )	: base( functionName )
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties:  Parameters Indicies"

				internal	int	ParIdx_TCode	{ get; private set;	}
				internal	int ParIdx_Skip1	{ get; private set;	}
				internal	int ParIdx_CTUOpt	{ get; private set;	}
				internal	int ParIdx_TabBDC	{ get; private set;	}
				internal	int	ParIdx_TabMSG	{ get; private set;	}
				internal	int ParIdx_TabSPA	{ get; private set;	}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void SetupProfile()
					{
						this.ParIdx_TCode		= this.Metadata.TryNameToIndex( "IF_TCODE"							);
						this.ParIdx_Skip1		= this.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	);
						this.ParIdx_TabBDC	= this.Metadata.TryNameToIndex( "IT_BDCDATA"						);
						this.ParIdx_CTUOpt	= this.Metadata.TryNameToIndex( "IS_OPTIONS"						);
						this.ParIdx_TabMSG	= this.Metadata.TryNameToIndex( "ET_MSG"								);
						this.ParIdx_TabSPA	= this.Metadata.TryNameToIndex( "CT_SETGET_PARAMETER"		);
					}

			#endregion

		}
}
