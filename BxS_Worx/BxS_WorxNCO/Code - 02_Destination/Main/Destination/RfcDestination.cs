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
	public class RfcDestination : IRfcDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcDestination( Guid ID )
					{
						this.SAPGUIID	= ID	;
						this.MyID			= Guid.NewGuid();
						//.............................................
						this._RfcConfig				=	new Lazy< SMC.RfcConfigParameters >	( ()=>	SAPSDM.Instance.CreateNCOConfig()												, cz_LM )	;
						this._SMCDestination	= new Lazy< SMC.RfcDestination >			( ()=>	SAPSDM.Instance.GetDestination( this._RfcConfig.Value ) , cz_LM )	;
						//.............................................
						this._Fncs	= new List<string>()					;
						this._Lock	= new SemaphoreSlim( 1 , 1 )	;
						//.............................................
						this._IsDirty		= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly Lazy< SMC.RfcDestination >				_SMCDestination	;
				private	readonly Lazy< SMC.RfcConfigParameters >	_RfcConfig			;
				//.................................................
				private	readonly	IList<String>		_Fncs ;
				private readonly	SemaphoreSlim		_Lock	;
				//.................................................
				private	int	_IsDirty	;

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

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( SMC.RfcConfigParameters config )
					{
						foreach (KeyValuePair<string, string> ls_kvp in config)
							{
								this._RfcConfig.Value[ls_kvp.Key]	= ls_kvp.Value;
							}
						//.............................................
						this._RfcConfig.Value.SecureRepositoryPassword	= config.SecureRepositoryPassword	;
						this._RfcConfig.Value.SecurePassword						= config.SecurePassword						;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigBase config )
					{
						foreach (	KeyValuePair<string, string> ls_kvp in config.Settings )
							{
								this._RfcConfig.Value[ ls_kvp.Key ]	= ls_kvp.Value ;
							}

					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadConfig( IConfigLogon config )
					{
						this.LoadConfig( (IConfigBase)config );
						//.............................................
						if ( config.SecurePassword != null )
							{
								if ( config.ForRepository )
									{	this._RfcConfig.Value.SecureRepositoryPassword	= config.SecurePassword;	}
								else
									{	this._RfcConfig.Value.SecurePassword						= config.SecurePassword;	}
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
						this._Lock.Wait();
						this._Fncs.Add( fncName );
						Interlocked.Exchange( ref this._IsDirty , 1 );
						this._Lock.Release();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task FetchMetadataAsync( bool	optimiseMetadataFetch = true )
					{
						if ( this._IsDirty.Equals(0) )		return;
						//.............................................
						this._Lock.Wait();
						if ( this._IsDirty.Equals(0) )		return;
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
								Interlocked.Exchange( ref this._IsDirty , 0 );
								this._Lock.Release();
							}
						catch ( Exception ex )
							{	throw	new Exception("Metadata ASYNC fail", ex ); }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SMC.RfcFunctionMetadata FetchFunctionMetadata( string fncName )	=>	this.SMCRepository.GetFunctionMetadata( fncName );

			#endregion

		}
}
