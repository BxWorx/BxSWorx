using System;
using System.Collections.Generic;
using System.Security;
//.........................................................
using SMC	= SAP.Middleware.Connector;
using SDM	= SAP.Middleware.Connector.RfcDestinationManager;
//.........................................................
using BxS_WorxNCO.Destination.API;

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
																					( ()=>	SDM.GetDestination( this._RfcConfig ).CreateCustomDestination() , cz_LM	);
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
				public SMC.RfcConfigParameters	CreateNCOConfig					()=> Destination_Factory.CreateNCOConfig					()	;
				public IConfigDestination				CreateDestinationConfig	()=> Destination_Factory.CreateDestinationConfig	()	;
				public IConfigGlobal						CreateGlobalConfig			()=> Destination_Factory.CreateGlobalConfig				()	;

				public IConfigLogon							CreateLogonConfig				( bool ForRepository = false )=> Destination_Factory.CreateLogonConfig( ForRepository )	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( SMC.RfcConfigParameters config )
					{
						this.UpdateConfig( config );
						//.............................................
						this.SecurePassword = config.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigLogon config )
					{
						this.UpdateConfig( config.Settings );
						//.............................................
						this.SecurePassword = config.SecurePassword;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigGlobal config )
					{
						this.UpdateConfig( config.Settings );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigDestination config )
					{
						this.UpdateConfig( config.Settings );
						//.............................................
						this.SecurePassword = config.SecurePassword;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterRfcFunctionForMetadata( string fncName )
					{
						this._Fncs.Add( fncName );
						this._MetadataIsDirty	= true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void FetchMetadata()
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
					}

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

			#endregion

			//===========================================================================================
			#region "Methods: Private"


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
