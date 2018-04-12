using System;
//.........................................................
using BxS_WorxNCO.RfcFunction.Main;

using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexFNCx : RfcFunctionIndex
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexFNCx()
					{
						this._TCode			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IF_TCODE"							) );
						this._Skip1			= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	) );
						this._CTUOpt		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IS_OPTIONS"						) );
						this._TabBDC		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "IT_BDCDATA"						) );
						this._TabMSG		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "ET_MSG"								) );
						this._TabSPA		= new Lazy<int>( ()=> this.Metadata.TryNameToIndex( "CT_SETGET_PARAMETER"		) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	Lazy<int>		_TCode	;
				private	readonly	Lazy<int>		_Skip1	;
				private	readonly	Lazy<int>		_CTUOpt	;
				private	readonly	Lazy<int>		_TabBDC	;
				private	readonly	Lazy<int>		_TabMSG	;
				private	readonly	Lazy<int>		_TabSPA	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	Name			{ get { return	this.Metadata.Name; } }
				//.................................................
				internal	int			TCode			{ get { return	this.IsLoaded	?	this._TCode	.Value : cz_No	; } }
				internal	int			Skip1			{ get { return	this.IsLoaded	?	this._Skip1	.Value : cz_No	; } }
				internal	int			CTUOpt		{ get { return	this.IsLoaded	?	this._CTUOpt.Value : cz_No	; } }
				internal	int			TabBDC		{ get { return	this.IsLoaded	?	this._TabBDC.Value : cz_No	; } }
				internal	int			TabMSG		{ get { return	this.IsLoaded	?	this._TabMSG.Value : cz_No	; } }
				internal	int			TabSPA		{ get { return	this.IsLoaded	?	this._TabSPA.Value : cz_No	; } }

			#endregion

		}
}
