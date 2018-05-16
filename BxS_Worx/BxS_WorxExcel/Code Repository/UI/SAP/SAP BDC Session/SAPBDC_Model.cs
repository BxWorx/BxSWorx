using System;
using System.ComponentModel;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI.Forms
{
	internal class SAPBDC_Model
		{
			#region "Declarations"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SAPBDC_Model( INCOx_Controller	ncoxCntlr )
					{
						this._NCOxCntlr	= ncoxCntlr;
						//...
						this.Request	= this._NCOxCntlr.NewSAPSessionRequest();
						this.List			= new	BindingList<IDTO_Session>();
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly INCOx_Controller		_NCOxCntlr;

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	IDTO_SessionRequest				Request	{ get;	private set; }
				internal	BindingList<IDTO_Session>	List		{ get; }
				//...
				private		string	MySettings	{	get	=>	Properties.Settings.Default.XML_SAPSession					;
																				set		{ Properties.Settings.Default.XML_SAPSession	= value	;	} }

			#endregion

			//===========================================================================================
			#region "Methods: Virtual"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void ClearList()
					{
						this.List.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void UpdateSAPSessionList()
					{
						this.List.Clear();
						//...
						foreach ( IDTO_Session lo_DTO in this._NCOxCntlr.RequestSAPSessionList( this.Request ) )
							{
								this.List.Add( lo_DTO	);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void GetSettings()
					{
						if ( this.MySettings.Length.Equals(0) )
							{
								this.FactorySettings();
								this.SaveSettings();
							}
						else
							{
								this.Request	= this._NCOxCntlr.DeserializeSAPSessionRequest( this.MySettings );
								//...
								this.Request.FromX	= false;
								this.Request.ToX		= false;
								//IDTO_SessionRequest	lo_Req	= this._NCOxCntlr.DeserializeSAPSessionRequest( this.MySettings );
								//lo_Req.FromX	= false;
								//lo_Req.ToX		= false;
								//this.Request	=	lo_Req;
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void SaveSettings()
					{
						this.MySettings		= this._NCOxCntlr.SerializeSAPSessionRequest( this.Request );
					}

			#endregion

			//===========================================================================================
			#region "Methods: Private"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void FactorySettings()
					{
						this.Request.User		= "*"															;
						this.Request.Name		= "*"															;
						this.Request.From		= new DateTime( 2000 , 01 , 01 )	;
						this.Request.To			= new DateTime( 2999 , 12 , 31 )	;
						this.Request.FromX	= false														;
						this.Request.ToX		= false;
					}

			#endregion

		}
}
