using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_SAPNCO.RfcFunction;
using BxS_SAPNCO.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.BDCProcess
{
	internal class BDCCallTranProfile : RfcFncProfileBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCallTranProfile(	DestinationRfc	destRfc
																		,	string					functionName )	: base( destRfc , functionName )
					{
						this.DestinationRfc.RegisterProfile(this);
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

				internal	SMC.IRfcStructure		GetCTUStr	{	get	{ return	this.Metadata[this.ParIdx_CTUOpt].ValueMetadataAsStructureMetadata.CreateStructure()	; } }
				internal	SMC.IRfcTable				GetBDCTbl	{	get	{ return	this.Metadata[this.ParIdx_TabBDC].ValueMetadataAsTableMetadata.CreateTable()					; } }
				internal	SMC.IRfcTable				GetSPATbl	{	get	{ return	this.Metadata[this.ParIdx_TabSPA].ValueMetadataAsTableMetadata.CreateTable()					; } }
				internal	SMC.IRfcTable				GetMSGTbl	{	get	{ return	this.Metadata[this.ParIdx_TabMSG].ValueMetadataAsTableMetadata.CreateTable()					; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override bool Setup()
					{
						try
							{
								this.ParIdx_TCode		= this.Metadata.TryNameToIndex( "IF_TCODE"							);
								this.ParIdx_Skip1		= this.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	);
								this.ParIdx_TabBDC	= this.Metadata.TryNameToIndex( "IT_BDCDATA"						);
								this.ParIdx_CTUOpt	= this.Metadata.TryNameToIndex( "IS_OPTIONS"						);
								this.ParIdx_TabMSG	= this.Metadata.TryNameToIndex( "ET_MSG"								);
								this.ParIdx_TabSPA	= this.Metadata.TryNameToIndex( "CT_SETGET_PARAMETER"		);

								return	true;
							}
						catch (System.Exception)
							{
								return	false;
							}
					}

			#endregion

		}
}
