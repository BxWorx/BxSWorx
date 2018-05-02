using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using BxS_WorxNCO.Destination.API;
using BxS_WorxNCO.RfcFunction.BDCTran;
using BxS_WorxNCO.RfcFunction.DDIC;
using BxS_WorxNCO.RfcFunction.SAPMsg;
using BxS_WorxNCO.RfcFunction.TableReader;

using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal class RfcFncController : IRfcFncController
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncController( IBxSDestination rfcDestination )
					{
						this.RfcDestination		= rfcDestination	??	throw		new ArgumentException( $"{typeof(BDC_Data).Namespace}:- BDCData null" );
						//.............................................
						this._FncProfFactory		= new	Lazy< Dictionary< string, Func<IRfcFncProfile> > >( ()=> this.LoadProfileFactories() );
						//.............................................
						this._RfcFncProfiles		= new ConcurrentDictionary< string , IRfcFncProfile >();
						this._Lock							= new object();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	object	_Lock	;
				//.................................................
				private	readonly	Lazy<	Dictionary< string , Func<IRfcFncProfile> > >	_FncProfFactory;
				private readonly	ConcurrentDictionary< string , IRfcFncProfile >			_RfcFncProfiles;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	IBxSDestination		RfcDestination	{ get; }
				public	SMC.RfcRepository	SMCRepository		{ get { return this.RfcDestination.SMCRepository; } }
				public	int								ProfileCount		{ get	{ return this._RfcFncProfiles.Count;				} }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: General"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task UpdateProfilesAsync( bool	optimiseMetadataFetch = true )
					{
						await	this.RfcDestination.FetchMetadataAsync( optimiseMetadataFetch ).ConfigureAwait(false);
						//.............................................
						foreach ( KeyValuePair< string , IRfcFncProfile > lo_Prof in this._RfcFncProfiles.Where( kvp => ! kvp.Value.IsReady ) )
							{
								try
									{
										lo_Prof.Value.Metadata	= this.RfcDestination.FetchFunctionMetadata( lo_Prof.Value.FunctionName );
										lo_Prof.Value.ReadyProfile();
									}
								catch ( Exception ex )
									{
										throw	new Exception( "Profile Metadata Load error" , ex );
									}
							}
					}

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Create Functions"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	BDC_Function				CreateBDCFunctionStd		()=>	new	BDC_Function			( (BDC_Profile)				this.GetAddProfile( cz_BDCStd )					);
				public	BDC_Function				CreateBDCFunctionAlt		()=>	new	BDC_Function			( (BDC_Profile)				this.GetAddProfile( cz_BDCAlt )					);
				public	SAPMsg_Function			CreateSAPMsgFunction		()=>	new SAPMsg_Function		( (SAPMsg_Profile)		this.GetAddProfile( cz_SAPMsgCompiler	) );
				public	TblRdr_Function			CreateTblRdrFunction		()=>	new TblRdr_Function		( (TblRdr_Profile)		this.GetAddProfile( cz_TableReader )		);
				public	DDICInfo_Function		CreateDDICInfoFunction	()=>	new DDICInfo_Function	( (DDICInfo_Profile )	this.GetAddProfile( cz_DDICInfo )				);

			#endregion

			//===========================================================================================
			#region "Methods: Exposed: Registration"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	void	RegisterBDCStd	()=>	this.RegisterProfile( cz_BDCStd					);
				public	void	RegisterBDCAlt	()=>	this.RegisterProfile( cz_BDCAlt					);

				public	void	RegisterSAPMsg	()=>	this.RegisterProfile( cz_SAPMsgCompiler	);
				public	void	RegisterTblRdr	()=>	this.RegisterProfile( cz_TableReader		);
				public	void	RegisterDDICIno	()=>	this.RegisterProfile( cz_DDICInfo				);

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private bool ProfileExists( string name )=>	this._RfcFncProfiles.ContainsKey( name );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private Dictionary<string, Func<IRfcFncProfile>> LoadProfileFactories()
					{
						var X = new Dictionary<string, Func<IRfcFncProfile>>
							{
								{ cz_BDCStd, this.CreateBDCProfileSTD },
								{ cz_BDCAlt, this.CreateBDCProfileALT },
								{ cz_SAPMsgCompiler, this.CreateSAPMsgProfile },
								{ cz_TableReader, this.CreateTblRdrProfile },
								{ cz_DDICInfo, this.CreateDDICInfoProfile }
							};
						return	X;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				// Factories for Profiles
				//
				private	BDC_Profile					CreateBDCProfileSTD		()=>	new BDC_Profile				( BDC_Factory.Instance , false	);
				private	BDC_Profile					CreateBDCProfileALT		()=>	new BDC_Profile				( BDC_Factory.Instance , true		);

				private	SAPMsg_Profile			CreateSAPMsgProfile		()=>	new SAPMsg_Profile		( SAPMsg_Factory.Instance		);
				private	TblRdr_Profile			CreateTblRdrProfile		()=>	new TblRdr_Profile		( TblRdr_Factory.Instance		);
				private	DDICInfo_Profile		CreateDDICInfoProfile	()=>	new	DDICInfo_Profile	( DDICInfo_Factory.Instance );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IRfcFncProfile GetAddProfile( string name )
					{
						if ( ! this.ProfileExists( name ) )
							{
								this.RegisterProfile( name );
							}
						//.............................................
						this._RfcFncProfiles.TryGetValue(	name , out IRfcFncProfile lo_Prof );
						return	lo_Prof;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void RegisterProfile( string name )
					{
						if ( ! this.ProfileExists( name ) )
							{
								lock (this._Lock)
									{
										if ( ! this.ProfileExists( name ) )
											{
												if ( this._FncProfFactory.Value.TryGetValue( name , out Func<IRfcFncProfile> lo_Fact ) )
													{
														if ( this._RfcFncProfiles.TryAdd( name , lo_Fact() ) )
															{
																this.RfcDestination.RegisterRfcFunctionForMetadata( name );
															}
													}
											}
									}
							}
					}

			#endregion

		}
}
