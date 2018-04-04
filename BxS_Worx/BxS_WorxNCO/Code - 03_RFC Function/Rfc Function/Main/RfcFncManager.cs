using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcFncManager : IRfcFncManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncManager( IRfcDestination rfcDestination )
					{
						this._RfcDestination	= rfcDestination	??	throw		new	ArgumentException( $"{typeof(RfcFncManager).Namespace}:- RfcDestination null" );
						//.............................................
						this._RfcFncProfiles	= new ConcurrentDictionary< string , IRfcFncProfile >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	IRfcDestination																		_RfcDestination;
				private readonly	ConcurrentDictionary< string , IRfcFncProfile >		_RfcFncProfiles;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	SMC.RfcRepository	SMCRepository	{ get { return this._RfcDestination.SMCRepository; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterProfile( IRfcFncProfile rfcFncProfile )
					{
						if ( this._RfcFncProfiles.TryAdd( rfcFncProfile.FunctionName , rfcFncProfile ) )
							{
								this._RfcDestination.RegisterRfcFunctionForMetadata( rfcFncProfile.FunctionName );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ProfileExists( string rfcFncName )
					{
						return	this._RfcFncProfiles.ContainsKey( rfcFncName );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ProfileType GetProfile<ProfileType>( string rfcFncName )
					{
						this._RfcFncProfiles.TryGetValue(	rfcFncName , out IRfcFncProfile lo_Prof );
						return	(ProfileType) lo_Prof;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task UpdateProfilesAsync( bool	optimiseMetadataFetch = true )
					{
						await	this._RfcDestination.FetchMetadataAsync( optimiseMetadataFetch ).ConfigureAwait(false);
						//.............................................
						foreach ( KeyValuePair< string , IRfcFncProfile > lo_Prof in this._RfcFncProfiles )
							{
								try
									{
										if ( ! lo_Prof.Value.IsReady )
											{
												lo_Prof.Value.Metadata	= this._RfcDestination.FetchFunctionMetadata( lo_Prof.Value.FunctionName );
												lo_Prof.Value.IsReady		= true;
											}
									}
								catch ( Exception ex )
									{
										throw	new Exception( "Profile Metadata Load error" , ex );
									}
							}
						}

			#endregion

		}
}
