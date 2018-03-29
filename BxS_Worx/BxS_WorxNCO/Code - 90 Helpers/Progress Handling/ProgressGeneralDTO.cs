using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxNCO.Helpers.Progress
{
	public class ProgressGeneralDTO
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ProgressGeneralDTO( int total	= 100 )
					{
						this.Total	= total;
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	int	_Total		;
				private	int _Completed;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int	Total									{ get	{ return	this._Total			; }		set { Interlocked.Exchange(	ref	this._Total			, value	)	; } }
				public	int Completed							{ get	{ return	this._Completed	; }		set	{ Interlocked.Exchange(	ref this._Completed	, value	)	; } }

				public	int PercentageCompleted		{ get	{	return	this.Total.Equals(0)	?	0	:		(								this.Completed		/ this.Total ) * 100	;	} }
				public	int PercentageOutstanding	{ get	{	return	this.Total.Equals(0)	?	0	:	( ( this.Total -	this.Completed )	/ this.Total ) * 100	;	} }

				public string Message	{ get; set; }

			#endregion

		}
}
