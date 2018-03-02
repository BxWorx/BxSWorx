using System;
using System.Collections.Concurrent;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Common
{
	internal class RfcFunctionPool : IRfcFunctionPool
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFunctionPool()
					{
						this._Functions		= new ConcurrentDictionary< string , string >();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	ConcurrentDictionary< string , string >	_Functions;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	SMC.RfcRepository				NCORepository			{ get; set; }
				public	SMC.RfcLookupErrorList	NCOLookupErrors		{ get; private set; }

			#endregion

			//===========================================================================================
			#region "Methods"

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public void RegisterFunction( string name )
				{
					this._Functions.TryAdd( name , name );
				}

			//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			public bool FetchMetadata()
				{
					string[] lt_Fnc	= new string[	this._Functions.Keys.Count ];
					string[] lt_Str	= new string[] {};
					string[] lt_Tbl	= new string[] {};
					string[] lt_Cls	= new string[] {};

					this._Functions.Keys.CopyTo( lt_Fnc , 0 );
					//...............................................
					try
						{
							this.NCORepository.UseRoundtripOptimization	= true;
							this.NCOLookupErrors = this.NCORepository.MetadataBatchQuery( lt_Fnc, lt_Str, lt_Tbl, lt_Cls );
							return	this.NCOLookupErrors == null ;
						}
					catch (Exception)
						{
							return	false;
						}
				}

			#endregion

		}
}
