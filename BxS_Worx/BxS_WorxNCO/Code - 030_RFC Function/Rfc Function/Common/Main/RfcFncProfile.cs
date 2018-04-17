using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal abstract class RfcFncProfile : IRfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncProfile(	string	functionName )
					{
						this.FunctionName			= functionName		??	throw		new	ArgumentException( $"{typeof(RfcFncProfile).Namespace}:- Function Name null"	);
						//.............................................
						this.IsReady	= false;
						//.............................................
						this._Lock		= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	object	_Lock;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	FunctionName	{	get; }
				public	bool		IsReady				{ get; set; }
				//.................................................
				public	SMC.RfcFunctionMetadata		Metadata	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcFunction	CreateFunction()	=>	this.Metadata.CreateFunction();

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual void ReadyProfile()
					{
						this.IsReady	= true;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Protected/Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadFunctionIndex( IRfcFunctionIndex index )
					{
						index.Metadata	= this.Metadata;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool LoadStructureIndex( IRfcStructureIndex index )
					{
						try
							{
								SMC.RfcElementMetadata	lo_ElemMeta		=	this.Metadata[ index.Name ];
								//.........................................
								switch ( lo_ElemMeta.DataType )
									{
										case	SMC.RfcDataType.STRUCTURE	:	index.Metadata	=	lo_ElemMeta.ValueMetadataAsStructureMetadata			;	return	true	;
										case	SMC.RfcDataType.TABLE			:	index.Metadata	=	lo_ElemMeta.ValueMetadataAsTableMetadata.LineType	;	return	true	;
													default										:																																				return	false	;
									}
							}
						catch
							{	return	false; }
					}

			#endregion

		}
}
