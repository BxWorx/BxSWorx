using System;
using System.Collections.Generic;
//using System.Reflection;
using System.Security;
//using System.Linq;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM	= SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_WorxNCO.Destination.Config;
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.Main;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	public class RfcDestination : IRfcDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcDestination( Guid ID )
					{
						this.SAPGUIID	= ID	;
						this.MyID			= Guid.NewGuid();
						//.............................................
						this._RfcConfig		=	new SMC.RfcConfigParameters();
						this._Lock				= new object();
						this._Fncs				= new List<string>();
						this._OptMetadata	= false;
						//.............................................
						this._NCODestination	= new Lazy< SMC.RfcCustomDestination >
																					(		()=>	SDM.GetDestination( this._RfcConfig ).CreateCustomDestination()
																						, cz_LM	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy< SMC.RfcCustomDestination > _NCODestination;

				private readonly object										_Lock				;
				private readonly SMC.RfcConfigParameters	_RfcConfig	;
				private readonly IList<String>						_Fncs;
				//.................................................
				private bool	_OptMetadata;
				private bool	_MetadataIsDirty;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsConnected	{ get { return this.Ping(); } }
				//.................................................
				public Guid		MyID			{ get; }
				public Guid		SAPGUIID	{ get; }

				public SMC.RfcCustomDestination		NCODestination	{ get { return	this._NCODestination.Value						; } }
				public SMC.RfcRepository					NCORepository		{	get	{	return	this._NCODestination.Value.Repository	; } }
				//.................................................
				public string Client			{ set { this._RfcConfig	[ SMC.RfcConfigParameters.Client			]	= value; } }
				public string Language		{ set { this._RfcConfig	[ SMC.RfcConfigParameters.Language		]	= value; } }
				public string User				{ set { this._RfcConfig	[	SMC.RfcConfigParameters.User				]	= value; } }
				public string Password		{ set { this._RfcConfig	[	SMC.RfcConfigParameters.Password		]	= value; } }
				public string UseSAPGui		{ set { this._RfcConfig	[	SMC.RfcConfigParameters.UseSAPGui		]	= value; } }

				public bool		ShowSAPGui	{ set { this._RfcConfig	[	SMC.RfcConfigParameters.UseSAPGui		]
																						= value ?		SMC.RfcConfigParameters.RfcUseSAPGui.Use
																											: SMC.RfcConfigParameters.RfcUseSAPGui.Hidden ; } }

				public SecureString SecurePassword	{ set { this._RfcConfig.SecurePassword	= value; } }
				//.................................................
				public bool	LogonCheck	{ set { this._RfcConfig	[	SMC.RfcConfigParameters.LogonCheck	]	= value ? "1" : "0" ; } }
				public bool OptimiseMetadataFetch	{ set { this._OptMetadata	= value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcConfigParameters	CreateNCOConfig					()=>	new	SMC.RfcConfigParameters	();
				public IConfigSetupDestination	CreateDestinationConfig	()=>	new ConfigSetupDestination	();
				public IConfigSetupGlobal				CreateGlobalConfig			()=>	new ConfigSetupGlobal				();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( SMC.RfcConfigParameters config )
					{
						this.UpdateConfig( config );
						//.............................................
						this.SecurePassword = config.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigSetupGlobal config )
					{
						this.UpdateConfig( config.Settings );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigSetupDestination config )
					{
						this.UpdateConfig( config.Settings );
						//.............................................
						this.SecurePassword = config.SecurePassword;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcStructure CreateRfcStructure( string strName )
					{
						return	this.NCORepository.GetStructureMetadata( strName ).CreateStructure();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcTable CreateRfcTable( string strName )
					{
						return	this.NCORepository.GetStructureMetadata( strName ).CreateTable();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcFunction CreateRfcFunction( string fncName )
					{
						try
							{
								return	this.NCORepository.CreateFunction( fncName );
							}
						catch (Exception)
							{
								throw;
							}

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterRfcFunctionForMetadata( string fncName , bool loadMetaData = false )
					{
						this._Fncs.Add( fncName );
						this._MetadataIsDirty	= true;

						if ( loadMetaData )
							{
								this.FetchMetadata();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool LoadRfcFunctionProfileMetadata( IRfcFncProfile profile )
					{
						if ( ! profile.IsReady )
							{
								profile.ReadyProfile();
							}
						//.............................................
						return	profile.IsReady;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public bool LoadRfcFunction( IRfcFncBase rfcFunction )
				//	{
				//		bool	lb_Ret = true;
				//		//.............................................
				//		try
				//			{
				//				rfcFunction.NCORfcFunction	= this.CreateRfcFunction( rfcFunction.SAPFncName );
				//			}
				//		catch
				//			{ lb_Ret	= false; }
				//		//.............................................
				//		return	lb_Ret;
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Ping()
					{
						bool	lb_Ret = false;
						//.............................................
						try
							{
								this.NCODestination.Ping();
								lb_Ret	=	true;
							}
						catch
							{	}
						//.............................................
						return	lb_Ret;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public bool LoadFunctionIndexing<T>( T obj ) where T:class
				//	{
				//		try
				//			{
				//				string	lc_Nme	= string.Empty;
				//				int			ln_Idx	= 0;

				//				SMC.RfcFunctionMetadata		lo_Fnc	= null	;
				//				SAPAttribute							lo_CP;
				//				//.............................................
				//				lc_Nme	= this.ClassLevelAttribute<T>();
				//				lo_Fnc	= this.NCORepository.GetFunctionMetadata( lc_Nme );

				//				foreach ( PropertyInfo lo_PI in	obj.GetType().GetProperties() )
				//					{
				//						lo_CP		=	(SAPAttribute) Attribute.GetCustomAttribute( lo_PI , typeof( SAPAttribute ) );
				//						ln_Idx	= lo_Fnc.TryNameToIndex( lo_CP.Name );

				//						lo_PI.SetValue( obj , ln_Idx );
				//					}

				//				return	true;
				//			}
				//		catch
				//			{
				//				return	false;
				//			}
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public bool LoadStructureIndexing<T>( T obj ) where T:class
				//	{
				//		string	lc_Name	= string.Empty;
				//		int			ln_PIndx		= 0;

				//		SMC.RfcStructureMetadata	ls_Stru	= null;
				//		SAPAttribute							lo_CP;
				//		//.............................................
				//		try
				//			{
				//				lc_Name		= this.ClassLevelAttribute<T>();
				//				ls_Stru		= this.NCORepository.GetStructureMetadata( lc_Name );

				//				foreach ( PropertyInfo lo_PI in	obj.GetType().GetProperties() )
				//					{
				//						lo_CP			=	(SAPAttribute) Attribute.GetCustomAttribute( lo_PI , typeof( SAPAttribute ) );
				//						ln_PIndx	= ls_Stru.TryNameToIndex( lo_CP.Name );

				//						lo_PI.SetValue( obj , ln_PIndx );
				//					}

				//				return	true;
				//			}
				//		catch
				//			{
				//				return	false;
				//			}
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private string ClassLevelAttribute<T>() where T:class
				//	{
				//		return	typeof(T).GetCustomAttributes( typeof( SAPAttribute ) , true )
				//							.FirstOrDefault() is SAPAttribute SAPAttr ? SAPAttr.Name : string.Empty	;
				//	}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool FetchMetadata()
					{
						if ( this._MetadataIsDirty )
							{
								SMC.RfcLookupErrorList	lo_NCOLookupErrors;

								string[] lt_Str		= new string[] {};
								string[] lt_Tbl		= new string[] {};
								string[] lt_Cls		= new string[] {};
								string[] lt_Fnc		= new string[	this._Fncs.Count ];
								//...............................................
								try
									{
										this._Fncs.CopyTo( lt_Fnc , 0 );
										this.NCORepository.UseRoundtripOptimization = this._OptMetadata;
										lo_NCOLookupErrors		= this.NCORepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls );
										this._MetadataIsDirty	= false;
									}
								catch
									{	}
								finally
									{	}
							}
						//...............................................
						return	! this._MetadataIsDirty;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void UpdateConfig( Dictionary< string , string> settings )
					{
						foreach (KeyValuePair<string, string> ls_kvp in settings)
							{
								this._RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
					}

			#endregion

		}
}
