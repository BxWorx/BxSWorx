using System;
using System.Collections.Generic;
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
				public void	LoadMapper( IRfcFncIndexMapper map )
					{
						this.ExtractIndexing( map );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcFunction	CreateFunction()
					{
						return	this.Metadata.CreateFunction();
					}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual void ReadyProfile()
					{	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void ExtractIndexing( IRfcFncIndexMapper map )
					{
						// Empty Reference means this mapping is for function parameter indexing
						//
						if ( string.IsNullOrEmpty( map.ReferenceParameterName ) )
							{
								foreach ( KeyValuePair< string , int > ls_kvp in map.SAPIndex )
									{
										map.SAPIndex[ ls_kvp.Key ]	=	this.Metadata.TryNameToIndex( ls_kvp.Key );
									}
							}
						else
							{
								SMC.RfcStructureMetadata	lo_StruMeta	;
								SMC.RfcElementMetadata		lo_ElemMeta		=	this.Metadata[ map.ReferenceParameterName ];
								//.........................................
								switch ( lo_ElemMeta.DataType )
									{
										case	SMC.RfcDataType.STRUCTURE	:	lo_StruMeta		=	lo_ElemMeta.ValueMetadataAsStructureMetadata			;	break	;
										case	SMC.RfcDataType.TABLE			:	lo_StruMeta		=	lo_ElemMeta.ValueMetadataAsTableMetadata.LineType	;	break	;
										default													: lo_StruMeta		= null																							;	break	;
									}
								//.........................................
								if ( lo_StruMeta != null )
									{
										foreach ( KeyValuePair< string , int > ls_kvp in map.SAPIndex )
											{
												map.SAPIndex[ ls_kvp.Key ]	= lo_StruMeta.TryNameToIndex( ls_kvp.Key );
											}
									}
							}
					}

			#endregion

		}
}
