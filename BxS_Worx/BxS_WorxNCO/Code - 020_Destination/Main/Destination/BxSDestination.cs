using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.Destination.Config;

using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	public class BxSDestination : IBxSDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BxSDestination( Guid ID )
					{
						this.SAPGUIID	= ID	;
						this.MyID			= Guid.NewGuid();
						//.............................................
						this._SMCDestination	= new Lazy<SMC.RfcDestination>	( ()=>	this.SDM.GetDestination( this._RfcConfig )	, cz_LM )	;
						//.............................................
						this._RfcConfig		=	this.SDM.CreateNCOConfig();

						this._Fncs	= new List<string>()					;
						this._MLck	= new SemaphoreSlim( 0 , 1 )	;
						this._ILck	= new SemaphoreSlim( 0 , 1 )	;
						//.............................................
						this._IsMetaDirty		= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<SMC.RfcDestination>	_SMCDestination	;
				//.................................................
				private	readonly	SMC.RfcConfigParameters		_RfcConfig	;
				//.................................................
				private	readonly	IList<String>		_Fncs ;
				private readonly	SemaphoreSlim		_MLck	;
				private readonly	SemaphoreSlim		_ILck	;
				//.................................................
				private	int	_IsMetaDirty	;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid	MyID			{ get; }
				public	Guid	SAPGUIID	{ get; }
				//.................................................
				public	SMC.RfcDestination	SMCDestination	{ get { return	this._SMCDestination.Value			; } }
				public	SMC.RfcRepository		SMCRepository		{ get { return	this.SMCDestination.Repository	; } }
				//.................................................
				public	bool	IsConnected		{ get { return this.Ping(); } }

				//.................................................
				private	SAPDM	SDM		{ get { return	SAPDM.Instance; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	SMC.RfcConfigParameters		CreateNCOConfig						()=>	Destination_Factory.CreateNCOConfig();
				//...
				public	IConfigLogon							CreateLogonConfig					( bool ForRepository = false )=>	Destination_Factory.CreateLogonConfig ( ForRepository );
				//...
				public	IConfigRepository					CreateRepositoryConfig		()=>	Destination_Factory.CreateRepositoryConfig();
				public	IConfigDestination				CreateDestinationConfig		()=>	Destination_Factory.CreateDestinationConfig();

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( SMC.RfcConfigParameters config )
					{
						if ( this._SMCDestination.IsValueCreated )	return;
						//...
						foreach (KeyValuePair<string, string> ls_kvp in config)
							{
								this._RfcConfig[ls_kvp.Key]	= ls_kvp.Value;
							}
						//...
						this._RfcConfig.SecureRepositoryPassword	= config.SecureRepositoryPassword	;
						this._RfcConfig.SecurePassword						= config.SecurePassword						;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigBase config )
					{
						if ( this._SMCDestination.IsValueCreated )	return;
						//...
						this.TransferConfig( config );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigLogon config )
					{
						if ( this._SMCDestination.IsValueCreated )	return;
						//...
						this.TransferConfig( (IConfigBase)config );
						//.............................................
						if ( config.SecurePassword != null )
							{
								if ( config.ForRepository )
									{	this._RfcConfig.SecureRepositoryPassword	= config.SecurePassword;	}
								else
									{	this._RfcConfig.SecurePassword						= config.SecurePassword;	}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Ping()
					{
						bool	lb_Ret = false;
						//.............................................
						try
							{
								this.SMCDestination.Ping();
								lb_Ret	=	true;
							}
						catch ( Exception ex )
							{	throw	new Exception( "Ping Failed", ex );	}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Metadata"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterRfcFunctionForMetadata( string fncName )
					{
						this._MLck.Wait();
						this._Fncs.Add( fncName );
						Interlocked.Exchange( ref this._IsMetaDirty , 1 );
						this._MLck.Release();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task FetchMetadataAsync( bool	optimiseMetadataFetch = true )
					{
						if ( this._IsMetaDirty.Equals(0) )		return;
						//.............................................
						this._MLck.Wait();

						if ( this._IsMetaDirty.Equals(0) )
							{
								this._MLck.Release();
								return;
							}
						//.............................................
						SMC.RfcLookupErrorList	lo_NCOLookupErrors;

						string[] lt_Str		= new string[] {};
						string[] lt_Tbl		= new string[] {};
						string[] lt_Cls		= new string[] {};
						string[] lt_Fnc		= new string[	this._Fncs.Count ];
						//...............................................
						try
							{
								this._Fncs.CopyTo( lt_Fnc , 0 );
								this.SMCDestination.Repository.UseRoundtripOptimization = optimiseMetadataFetch;
								await Task.Run(	()=>	lo_NCOLookupErrors	= this.SMCRepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls ) )
																															.ConfigureAwait(false);
								Interlocked.Exchange( ref this._IsMetaDirty , 0 );
								this._MLck.Release();
							}
						catch ( Exception ex )
							{	throw	new Exception("Metadata ASYNC fail", ex ); }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcFunctionMetadata FetchFunctionMetadata( string fncName )	=>	this.SMCRepository.GetFunctionMetadata( fncName );

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void TransferConfig( IConfigBase config )
					{
						foreach (	KeyValuePair<string, string> ls_kvp in config.Settings )
							{
								this._RfcConfig[ ls_kvp.Key ]	= ls_kvp.Value ;
							}
					}

			#endregion

		}
}
