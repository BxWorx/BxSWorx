using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Destination.Main.Destination
{
	public class REPDestination : STDDestination , IREPDestination
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal REPDestination( Guid ID ) : base( ID )
					{
						this._Fncs	= new List<string>()					;
						this._Lock	= new SemaphoreSlim( 0 , 1 )	;
						//.............................................
						_IsDirty		= 0	;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IList<String>	_Fncs ;
				//.................................................
				private	static		int		_IsDirty	;
				private SemaphoreSlim		_Lock			;

			#endregion

			//===========================================================================================
			#region "Properties"

				private	SMC.RfcRepository	_SMCRepository	{ get { return	this.NCODestination.Repository	; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Configuration"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterRfcFunctionForMetadata( string fncName )
					{
						this._Lock.Wait();
						this._Fncs.Add( fncName );
						Interlocked.Exchange( ref _IsDirty , 1 );
						this._Lock.Release();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task< bool > FetchMetadataAsync( bool	optimiseMetadataFetch = true )
					{
						if ( _IsDirty.Equals(0) )		return	true;
						//.............................................
						this._Lock.Wait();
						if ( _IsDirty.Equals(0) )		return	true;
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
								this.NCODestination.Repository.UseRoundtripOptimization = optimiseMetadataFetch;
								await Task.Run(	()=>	lo_NCOLookupErrors	= this._SMCRepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls ) )
																															.ConfigureAwait(false);
								Interlocked.Exchange( ref	_IsDirty , 0 );
								this._Lock.Release();
								return	true;
							}
						catch
							{	return	false; }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Task< SMC.RfcFunctionMetadata >	FetchFunctionMetadata( string fncName )
					{
						return	Task< SMC.RfcFunctionMetadata >.Run( ()=> this._SMCRepository.GetFunctionMetadata( fncName ) );
					}

			#endregion

		}
}
