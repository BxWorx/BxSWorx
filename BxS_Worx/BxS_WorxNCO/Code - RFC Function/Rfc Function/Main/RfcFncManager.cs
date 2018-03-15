using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API.Destination;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal class RfcFncManager : IRfcFncManager
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncManager( IRfcDestination rfcDestination )
					{
						this._RfcDestination	= rfcDestination	?? throw new ArgumentException("IRfcDestination is null");
						//.............................................
						this.UseRoundtrip			= true;
						//this._IsDirty					= false;
						this._RfcFncProfiles	= new ConcurrentDictionary< string , IRfcFncProfile >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				//private bool	_IsDirty;
				//.................................................
				private	readonly	IRfcDestination																		_RfcDestination;
				private readonly	ConcurrentDictionary< string , IRfcFncProfile >		_RfcFncProfiles;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	UseRoundtrip	{	get;	set; }
				//.................................................
				public	SMC.RfcRepository				NCORepository			{ get { return this._RfcDestination.NCORepository; } }

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterProfile( IRfcFncProfile rfcFncProfile , bool loadMetadata = false )
					{
						if ( this._RfcFncProfiles.TryAdd( rfcFncProfile.FunctionName , rfcFncProfile ) )
							{
								this._RfcDestination.RegisterRfcFunctionForMetadata( rfcFncProfile.FunctionName , loadMetadata );
							}
						////.............................................
						//if ( loadMetadata )
						//	{
						//		if ( this.FetchMetadata() )
						//			{
						//				this.UpdateProfiles();
						//			}
						//	}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool ProfileExists( string rfcFncName )
					{
						return	this._RfcFncProfiles.ContainsKey( rfcFncName );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ProfileType GetProfile<ProfileType>( string rfcFncName )
					{
						this._RfcFncProfiles.TryGetValue(	rfcFncName	, out IRfcFncProfile lo_Prof );
						return	(ProfileType) lo_Prof;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool UpdateProfiles()
					{
						bool lb_Ret	= true;
						//...............................................
						foreach ( KeyValuePair< string , IRfcFncProfile > lo_Prof in this._RfcFncProfiles )
							{
								if ( ! this._RfcDestination.LoadRfcFunctionProfileMetadata( lo_Prof.Value ) )
									{
										lb_Ret	= false;
									}
							}
						//...............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private bool UpdateProfileMetadata( IRfcFncProfile lo_Prof )
				//	{
				//		string	lc_StrName	= string.Empty;
				//		int			ln_PIndx		= 0;

				//		SMC.RfcStructureMetadata	ls_StruMetadata	= null;
				//		//.............................................
				//		if ( lo_Prof.IsReady )	return	true;
				//		//.............................................
				//		try
				//			{
				//				SMC.RfcFunctionMetadata lo_FncMetdata	= this._RfcDestination.NCORepository.GetFunctionMetadata( lo_Prof.FunctionName );
				//				//.........................................
				//				// Collect indicies for function parameters, structure fields
				//				//
				//				foreach ( PropertyInfo lo_PI in	lo_Prof.GetType().GetProperties() )
				//					{
				//						var lo_CP	=	(SAPFncAttribute) Attribute.GetCustomAttribute( lo_PI , typeof( SAPFncAttribute ) );

				//						if ( lo_CP != null )
				//							{
				//								if ( lo_CP.Stru?.Equals(0) == false )
				//									{
				//										if ( !lc_StrName.Equals( lo_CP.Stru ) )
				//											{
				//												lc_StrName	= lo_CP.Stru;
				//												ls_StruMetadata	= this._RfcDestination.NCORepository.GetStructureMetadata( lc_StrName );
				//											}
				//										ln_PIndx	= ls_StruMetadata.TryNameToIndex( lo_CP.Name );
				//									}
				//								else
				//									{
				//										ln_PIndx	= lo_FncMetdata.TryNameToIndex( lo_CP.Name );
				//									}

				//								lo_PI.SetValue( lo_Prof , ln_PIndx );
				//							}
				//					}
				//				//.........................................
				//				lo_Prof.IsReady	= true;
				//			}
				//		catch	{	}
				//		//.............................................
				//		return	lo_Prof.IsReady;
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private bool FetchMetadata()
				//	{
				//		if ( this._IsDirty )
				//			{
				//				string[] lt_Str		= new string[] {};
				//				string[] lt_Tbl		= new string[] {};
				//				string[] lt_Cls		= new string[] {};
				//				//...............................................
				//				string[] lt_Fnc	= new string[	this._RfcFncProfiles.Count ];
				//				this._RfcFncProfiles.Keys.CopyTo( lt_Fnc , 0 );
				//				//...............................................
				//				try
				//					{
				//						this.NCORepository.UseRoundtripOptimization = this.UseRoundtrip;
				//						this.NCOLookupErrors	= this.NCORepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls );
				//						this._IsDirty	= false;
				//					}
				//				catch
				//					{	}
				//			}
				//		//...............................................
				//		return	! this._IsDirty;
				//	}

			#endregion

		}
}
