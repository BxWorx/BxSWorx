﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM	= SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_WorxNCO.Destination.API.Config;
using BxS_WorxNCO.Destination.API.Destination;
using BxS_WorxNCO.RfcFunction.Common;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	public class RfcDestination : IRfcDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcDestination(	Guid ID )
					{
						this.SAPGUIID		= ID	;
						//.............................................
						this._RfcConfig	=	new SMC.RfcConfigParameters();
						this._Lock			= new object();
						this._Fncs			= new List<string>();

						this._NCODestination	= new Lazy< SMC.RfcCustomDestination >
							(	()=>		SDM.GetDestination( this._RfcConfig ).CreateCustomDestination()
											, LazyThreadSafetyMode.ExecutionAndPublication										);
						//.............................................
						//this.IsProcured	= false;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Lazy< SMC.RfcCustomDestination > _NCODestination;

				private readonly object										_Lock				;
				private readonly SMC.RfcConfigParameters	_RfcConfig	;
				private readonly IList<String>						_Fncs;
				//.................................................
				private bool	_MetadataIsDirty;

			#endregion

			//===========================================================================================
			#region "Properties"

				public bool	IsConnected	{ get { return this.Ping(); } }
				//public bool	IsProcured	{ get; private set; }
				//.................................................
				public Guid												SAPGUIID				{ get; }

				public SMC.RfcCustomDestination		NCODestination	{ get { return	this._NCODestination.Value						; } }
				public SMC.RfcRepository					NCORepository		{	get	{	return	this._NCODestination.Value.Repository	; } }

				//public SMC.RfcRepository					NCORepository		{	get	{	try		{	return	this.Procure()	?	this.NCODestination.Repository : null;	}
				//																													catch	{	return	null;	}																										}
																													//}
				//.................................................
				public string Client			{ set { this._RfcConfig	[ SMC.RfcConfigParameters.Client			]	= value; } }
				public string Language		{ set { this._RfcConfig	[ SMC.RfcConfigParameters.Language		]	= value; } }
				public string User				{ set { this._RfcConfig	[	SMC.RfcConfigParameters.User				]	= value; } }
				public string Password		{ set { this._RfcConfig	[	SMC.RfcConfigParameters.Password		]	= value; } }
				public string UseSAPGui		{ set { this._RfcConfig	[	SMC.RfcConfigParameters.UseSAPGui		]	= value; } }

				public bool		LogonCheck	{ set { this._RfcConfig	[	SMC.RfcConfigParameters.LogonCheck	]	= value ? "1":"0" ; } }

				public SecureString SecurePassword	{ set { this._RfcConfig.SecurePassword	= value; } }

				public bool OptimiseMetadataFetch	{ get; set; }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

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
						return	this.NCODestination.Repository.GetStructureMetadata( strName ).CreateStructure();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcTable CreateRfcTable( string strName )
					{
						return	this.NCODestination.Repository.GetStructureMetadata( strName ).CreateTable();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.IRfcFunction CreateRfcFunction( string fncName )
					{
						return	this.NCODestination.Repository.CreateFunction( fncName );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterRfcFunctionForMetadata( string fncName , bool triggerFetch = false )
					{
						this._Fncs.Add( fncName );
						this._MetadataIsDirty	= true;

						if ( triggerFetch )
							{
								this.FetchMetadata();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool LoadRfcFunctionProfileMetadata( IRfcFncProfile lo_Prof )
					{
						if ( ! lo_Prof.IsReady )
							{
								this.UpdateProfileMetadata( lo_Prof );
							}
						//.............................................
						return	lo_Prof.IsReady;
					}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//public bool ProcureX()
				//	{
				//		if ( !this.IsProcured )
				//			{
				//				//.........................................
				//				lock (this._Lock)
				//					{
				//						if ( !this.IsProcured )
				//							{
				//								try
				//									{
				//										//this.NCODestination	= SDM.GetDestination( this._RfcConfig ).CreateCustomDestination();
				//										this.IsProcured			= !this.IsProcured;
				//									}
				//								catch	{	}
				//							}
				//					}
				//			}
				//		//.............................................
				//		return	this.IsProcured;
				//	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Ping()
					{
						bool	lb_Ret = false;
						//.............................................
						try
							{
								//if ( this.Procure() )
										this.NCODestination.Ping();
										lb_Ret	=	true;
							}
						catch
							{	}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool UpdateProfileMetadata( IRfcFncProfile lo_Prof )
					{
						string	lc_StrName	= string.Empty;
						int			ln_PIndx		= 0;

						SMC.RfcStructureMetadata	ls_StruMetadata	= null;
						//.............................................
						try
							{
								SMC.RfcFunctionMetadata lo_FncMetdata	= this.NCORepository.GetFunctionMetadata( lo_Prof.FunctionName );
								//.........................................
								// Collect indicies for function parameters, structure fields
								//
								foreach ( PropertyInfo lo_PI in	lo_Prof.GetType().GetProperties() )
									{
										var lo_CP	=	(SAPFncAttribute) Attribute.GetCustomAttribute( lo_PI , typeof( SAPFncAttribute ) );

										if ( lo_CP != null )
											{
												if ( lo_CP.Stru?.Equals(0) == false )
													{
														if ( ! lc_StrName.Equals( lo_CP.Stru ) )
															{
																lc_StrName			= lo_CP.Stru;
																ls_StruMetadata	= this.NCORepository.GetStructureMetadata( lc_StrName );
															}
														ln_PIndx	= ls_StruMetadata.TryNameToIndex( lo_CP.Name );
													}
												else
													{
														ln_PIndx	= lo_FncMetdata.TryNameToIndex( lo_CP.Name );
													}

												lo_PI.SetValue( lo_Prof , ln_PIndx );
											}
									}
								//.........................................
								lo_Prof.IsReady	= true;
							}
						catch	{	}
						//.............................................
						return	lo_Prof.IsReady;
					}

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
										this.NCORepository.UseRoundtripOptimization = this.OptimiseMetadataFetch;
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
