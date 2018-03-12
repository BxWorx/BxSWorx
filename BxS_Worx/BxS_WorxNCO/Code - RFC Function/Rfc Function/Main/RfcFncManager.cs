using System;
using System.Collections.Concurrent;
using System.Reflection;
//using System.Threading.Tasks;
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

				public	bool	UseRoundtrip	{	get;	set; }
				//.................................................
				public	SMC.RfcRepository				NCORepository			{ get { return this._RfcDestination.NCORepository; } }
				public	SMC.RfcLookupErrorList	NCOLookupErrors		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void LoadFncParmIndex<T>( T fncParmIndex )
					{
						if ( this.FetchMetadata() )
							{
								foreach ( PropertyInfo lo_PI in	fncParmIndex.GetType().GetProperties() )
									{
										var y =	(SAPFncParmNameAttribute) Attribute.GetCustomAttribute(lo_PI,typeof( SAPFncParmNameAttribute ));
										if (y != null)
											{
												var x = this._RfcDestination.NCORepository.
											}
										//lo_PI.SetValue(lo_PI, this.NCORepository. )
										//y.SAPName
										//LO_PO


										//string lc_PName	= lo_PI.Name;
										//object[] att		= lo_PI.GetCustomAttributes(	typeof( SAPFncParmNameAttribute )
										//																						, true														);

										//var			xx	= (SAPFncParmNameAttribute)att[0];
										//string indx = xx.SAPName;
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool PrepareFunction( IRfcFncBase rfcFunc )
					{
						if ( this._RfcFncProfiles.TryGetValue(	rfcFunc.SAPFunctionName
																									, out IRfcFncProfile lo_Prof ))
							{
								if ( this.FetchMetadata() )
									{
										rfcFunc.NCORfcFunction	= this.NCORepository.CreateFunction( rfcFunc.SAPFunctionName );

										this.LoadFncParmIndex(lo_Prof);

									}
							}
						return	true;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void RegisterProfile( IRfcFncProfile rfcFncProfile , bool loadMetadata = false )
					{
						this._RfcFncProfiles.TryAdd( rfcFncProfile.FunctionName , rfcFncProfile );
						if (loadMetadata)
							{
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool FetchMetadata( string fncName = default(string) )
					{
						bool	lb_Ret	= true;
						//...............................................
						//
						if (		!	string.IsNullOrEmpty(fncName)
								&&	!	this.NCORepository.CachedFunctionMetadata.FindIndex( (s)=> s.Equals(fncName) ).Equals(0) )
							{
								return	true;
							}
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
								this.NCORepository.UseRoundtripOptimization = this.UseRoundtrip;
								this.NCOLookupErrors	= this.NCORepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls );

								foreach ( KeyValuePair< string , IRfcFncProfile > lo_Prof in this._RfcFncProfiles )
									{
										try
											{
												SMC.RfcFunctionMetadata ZZ = this._RfcDestination.NCORepository.GetFunctionMetadata( lo_Prof.Key );

												foreach ( PropertyInfo lo_PI in	lo_Prof.GetType().GetProperties() )
													{
														var y =	(SAPFncParmNameAttribute) Attribute.GetCustomAttribute(lo_PI,typeof( SAPFncParmNameAttribute ));
														if (y != null)
															{
																int x = ZZ.TryNameToIndex(y.SAPName);
																lo_PI.SetValue( lo_PI , x );
															}
													}

											}
										catch (Exception)
											{

											throw;
											}


							}
						catch
							{	return	false; }
						//...............................................
						return	lb_Ret;
					}

			#endregion

		}
}
