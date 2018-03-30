using System;
using System.Linq;
using System.Reflection;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal abstract class RfcFncProfile : IRfcFncProfile
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncProfile(		string					functionName
																,	IRfcDestination rfcDestination )
					{
						this.FunctionName			= functionName		??	throw		new	ArgumentException( $"{typeof(RfcFncProfile).Namespace}:- Function Name null"	);
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(RfcFncProfile).Namespace}:- Destination null"		);
						//.............................................
						this.IsReady	= false;
						//.............................................
						this._Lock		= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private		readonly	object						_Lock;
				protected	readonly	IRfcDestination		_RfcDestination;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	string	FunctionName	{	get; }
				public	bool		IsReady				{ get; set; }
				//.................................................
				public	SMC.RfcFunctionMetadata	Metadata	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcFunction	CreateFunction()
					{
						return	this.Metadata.CreateFunction();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool LoadFunctionIndexing<T>( T obj ) where T:class
					{
						try
							{
								SAPAttribute	lo_CP;
								//.........................................
								foreach ( PropertyInfo lo_PI in	obj.GetType().GetProperties() )
									{
										lo_CP		=	(SAPAttribute) Attribute.GetCustomAttribute( lo_PI , typeof( SAPAttribute ) );
										lo_PI.SetValue( obj , this.Metadata.TryNameToIndex( lo_CP.Name ) );
									}
								//.........................................
								return	true;
							}
						catch
							{
								return	false;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected bool LoadStructureIndexing<T>( T obj ) where T:class
					{
						try
							{
								SAPAttribute							lo_CP;
								int												ln_PIndx	= 0;
								string										lc_Name		= this.ClassLevelAttribute<T>();
								SMC.RfcStructureMetadata	ls_Stru		= this.Metadata[ this.Metadata.TryNameToIndex( lc_Name ) ].ValueMetadataAsStructureMetadata;
								//.........................................
								foreach ( PropertyInfo lo_PI in	obj.GetType().GetProperties() )
									{
										lo_CP			=	(SAPAttribute) Attribute.GetCustomAttribute( lo_PI , typeof( SAPAttribute ) );
										ln_PIndx	= ls_Stru.TryNameToIndex( lo_CP.Name );

										lo_PI.SetValue( obj , ln_PIndx );
									}
								//.........................................
								return	true;
							}
						catch
							{
								return	false;
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private string ClassLevelAttribute<T>() where T:class
					{
						return	typeof(T).GetCustomAttributes( typeof( SAPAttribute ) , true )
											.FirstOrDefault() is SAPAttribute SAPAttr ? SAPAttr.Name : string.Empty	;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual void ReadyProfile()
					{	}

			#endregion

		}
}
