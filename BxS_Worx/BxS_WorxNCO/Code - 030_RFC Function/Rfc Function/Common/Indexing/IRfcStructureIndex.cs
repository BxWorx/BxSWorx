using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal interface IRfcStructureIndex
		{
			#region "Properties"

				string	Name	{ get; set; }
				//.................................................
				SMC.RfcStructureMetadata	Metadata	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				SMC.IRfcStructure	CreateStructure();
				SMC.IRfcTable			CreateTable();

			#endregion

		}
}
