using System;
using System.Collections.Concurrent;
using System.Reflection;
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
						this.NCORepository	= rfcDestination.NCODestination.Repository;
						//.............................................
						this.UseRoundtrip			= true;
						this._RfcFncProfiles	= new ConcurrentDictionary< string , IRfcFncProfile >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	ConcurrentDictionary< string , IRfcFncProfile >	_RfcFncProfiles;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	UseRoundtrip	{	get	{ return	this.NCORepository.UseRoundtripOptimization;					}
																			set {					this.NCORepository.UseRoundtripOptimization = value;	} }
				//.................................................
				public	SMC.RfcRepository				NCORepository			{ get; }
				public	SMC.RfcLookupErrorList	NCOLookupErrors		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool PrepareFunction( IRfcFncBase rfcFunc )
					{
						if ( this._RfcFncProfiles.TryGetValue( rfcFunc.SAPFunctionName, out IRfcFncProfile lo_Prof ))
							{
								if ( this.FetchMetadata() )
									{
										rfcFunc.NCORfcFunction	= this.NCORepository.CreateFunction( rfcFunc.SAPFunctionName );
										rfcFunc.Profile					= lo_Prof;
									}
							}
						return	true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterFunction( IRfcFncProfile rfcFncProfile )
					{
						this._RfcFncProfiles.TryAdd( rfcFncProfile.FunctionName , rfcFncProfile );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool FetchMetadata( string fncName = default(string) )
					{
						//...............................................
						//
						if (		!string.IsNullOrEmpty(fncName)
								&&	!this.NCORepository.CachedFunctionMetadata.FindIndex( (s)=> s.Equals(fncName) ).Equals(0) )
							{
								return	true;
							}
						//...............................................
						//...............................................
						string[] lt_Str		= new string[] {};
						string[] lt_Tbl		= new string[] {};
						string[] lt_Cls		= new string[] {};
						//...............................................
						string[] lt_Fnc	= new string[	this._RfcFncProfiles.Count ];
						this._RfcFncProfiles.Keys.CopyTo( lt_Fnc , 0 );
						//...............................................
						try
							{
								this.NCOLookupErrors = this.NCORepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls );
								return	this.NCOLookupErrors == null ;
							}
						catch (Exception)
							{
								return	false;
							}
					}

				private void Xx<T>(T index) where T: class
					{
						foreach ( PropertyInfo lo_PI in	index.GetType().GetProperties() )
							{
								string lc_PName	= lo_PI.Name;
								object[] att = lo_PI.GetCustomAttributes( typeof(SAPFncParmNameAttribute),true );


							}
					}

			#endregion

		}
}
