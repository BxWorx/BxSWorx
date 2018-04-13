using System;
//.........................................................
using SMC	= SAP.Middleware.Connector;
//.........................................................
using static	BxS_WorxNCO.Main.NCO_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.Main
{
	internal abstract class RfcFncBase : IRfcFncBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal RfcFncBase( IRfcFncProfile profile	)
					{
						this.Profile	= profile	??	throw		new	ArgumentException( $"{typeof(RfcFncBase).Namespace}:- Profile null" );
						//.............................................
						this.MyID	= Guid.NewGuid();
						this._NCORfcFunction	= new	Lazy<SMC.IRfcFunction>( ()=> this.Profile.CreateFunction() , cz_LM	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly	Lazy< SMC.IRfcFunction >	_NCORfcFunction;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	Guid		MyID				{	get; }
				public	string	SAPFncName	{	get		{	return	this.Profile.FunctionName	;	}	}
				//.................................................
				public	IRfcFncProfile		Profile					{ get; }
				public	SMC.IRfcFunction	NCORfcFunction	{ get { return	this._NCORfcFunction.Value; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool Invoke( SMC.RfcDestination rfcDestination )
					{
						bool	lb_Ret	= false;
						//.............................................
						try
							{
								this.NCORfcFunction.Invoke( rfcDestination );
								lb_Ret	= true;
							}
						catch ( Exception ex )
							{
								throw	new Exception( "NCO invoke error" , ex );
							}
						//.............................................
						return	lb_Ret;
					}

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected void LoadTable(		SMC.IRfcTable	data
																, int						index
																, bool					reverse = false )
					{
						SMC.IRfcTable lt_Tbl	= this.NCORfcFunction.GetTable( index );

						if ( reverse )
							{	data.Append( lt_Tbl ); }
						else
							{	lt_Tbl.Append( data ); }
					}

			#endregion

		}
}
