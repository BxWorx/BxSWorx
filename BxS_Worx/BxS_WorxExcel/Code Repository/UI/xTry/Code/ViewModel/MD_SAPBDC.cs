using System;
using System.ComponentModel;
//.........................................................
using BxS_WorxIPX.NCO;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxExcel.UI
{
	internal class MD_SAPBDC
		{
			#region "Declarations"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal MD_SAPBDC( INCOx_Controller	NCOxCntlr )
					{
						this._NCOxCntlr	= NCOxCntlr;
						this.Request		= this._NCOxCntlr.NewSAPSessionRequest();
						this.List				= new	BindingList<IDTO_Session>();
					}

			#endregion

				private readonly INCOx_Controller		_NCOxCntlr;

				internal	DTO_SAPSessionRequest	Request { get; set; }
				public	BindingList<IDTO_Session>	List	{ get; }

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void FetchSAPSessionList()
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
						if ( Properties.Settings.Default.XML_SAPSession.Length.Equals(0) )
							{
								this.Request.User = "*"															;
								this.Request.Name	= "*"															;
								this.Request.From	= new DateTime( 2000 , 01 , 01 )	;
								this.Request.To		= new DateTime( 2999 , 12 , 31 )	;
								this.Request.FromX	= true													;
								this.Request.ToX		= true													;
								//...
								this.SaveSettings();
							}
						else
							{
								this.Request	= this._NCOxCntlr.DeserializeSAPSessionRequest( Properties.Settings.Default.XML_SAPSession );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void SaveSettings()
					{
						Properties.Settings.Default.XML_SAPSession	= this._NCOxCntlr.SerializeSAPSessionRequest( this.Request );
					}
		}
}
