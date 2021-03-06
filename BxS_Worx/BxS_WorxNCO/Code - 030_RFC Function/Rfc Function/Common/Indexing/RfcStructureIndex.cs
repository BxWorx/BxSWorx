﻿using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcStructureIndex : IRfcStructureIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcStructureIndex()
					{
						this.IsLoaded	= false	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		SMC.RfcStructureMetadata	_Metadata;
				protected	bool	IsLoaded;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	Name	{ get; set; }
				//.................................................
				public	SMC.RfcStructureMetadata	Metadata	{ get	{	return	this._Metadata;	}
																											set	{	this._Metadata	= value	;
																														this.IsLoaded		= true	;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	SMC.IRfcStructure		CreateStructure	()=>	this.Metadata.CreateStructure();
				public	SMC.IRfcTable				CreateTable			()=>	this.Metadata.CreateTable();

			#endregion

		}
}
