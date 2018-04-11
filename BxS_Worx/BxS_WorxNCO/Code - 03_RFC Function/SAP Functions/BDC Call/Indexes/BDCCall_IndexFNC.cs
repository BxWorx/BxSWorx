using System;
//.........................................................
using	static	BxS_WorxNCO.RfcFunction.Main	.SAPRfcFncConstants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.RfcFunction.BDCTran
{
	internal class BDCCall_IndexFNC
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal BDCCall_IndexFNC( BDCCall_Profile profile )
					{
						this._Profile		= profile;
						//.............................................
						this._TCode			= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "IF_TCODE"							) );
						this._Skip1			= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "IF_SKIP_FIRST_SCREEN"	) );
						this._CTUOpt		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "IS_OPTIONS"						) );
						this._TabBDC		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "IT_BDCDATA"						) );
						this._TabMSG		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "ET_MSG"								) );
						this._TabSPA		= new Lazy<int>( ()=> this._Profile.Metadata.TryNameToIndex( "CT_SETGET_PARAMETER"	) );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly	BDCCall_Profile		_Profile;
				//.................................................
				private	readonly	Lazy<int>		_TCode	;
				private	readonly	Lazy<int>		_Skip1	;
				private	readonly	Lazy<int>		_CTUOpt	;
				private	readonly	Lazy<int>		_TabBDC	;
				private	readonly	Lazy<int>		_TabMSG	;
				private	readonly	Lazy<int>		_TabSPA	;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	string	Name			{ get { return	cz_BDCCall; } }
				//.................................................
				internal	int			TCode			{ get { return	this._Profile.IsReady ?	this._TCode	.Value : 0	; } }
				internal	int			Skip1			{ get { return	this._Profile.IsReady ?	this._Skip1	.Value : 0	; } }
				internal	int			CTUOpt		{ get { return	this._Profile.IsReady ?	this._CTUOpt.Value : 0	; } }
				internal	int			TabBDC		{ get { return	this._Profile.IsReady ?	this._TabBDC.Value : 0	; } }
				internal	int			TabMSG		{ get { return	this._Profile.IsReady ?	this._TabMSG.Value : 0	; } }
				internal	int			TabSPA		{ get { return	this._Profile.IsReady ?	this._TabSPA.Value : 0	; } }

			#endregion

		}
}
