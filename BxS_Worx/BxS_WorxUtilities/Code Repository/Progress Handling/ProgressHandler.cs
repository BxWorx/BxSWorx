﻿using System;
using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.Progress
{
	public class ProgressHandler<T>	where T:class
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal ProgressHandler(		Func<T>	factory
																	,	int			reportInterval	= 10 )
					{
						this._Factory				= factory					;
						this._RepInterval		= reportInterval	;
						//.............................................
						this.IsActive		= false	;
						//...
						this._HitCount	= 0;
						this._ChkCount	= this._RepInterval - 1	;
						this._Handler		= new	Lazy< IProgress<T> >( ()=>	new Progress<T>() );
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private readonly Func<T>							_Factory;
				private readonly Lazy< IProgress<T> >	_Handler;

				private int _RepInterval	;
				private int _ChkCount			;
				private	int _HitCount			;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	bool	IsActive				{ get; set; }

				public	int		ReportInterval	{ get { return	this._RepInterval;																}
																				set { this._RepInterval = value;	this._ChkCount	= value -1 ;	} }

				public	bool	GoingToHit			{ get {	Interlocked.Increment( ref this._HitCount )	;
																							return	this._HitCount >= this._ChkCount		; } }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T Create()
					{
						return	this._Factory();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Report( T dto = null )
					{
						if ( dto != null )
							{
								this._Handler.Value.Report	( dto );
							}
						//.............................................
						Interlocked.Exchange( ref this._HitCount , 0 );
					}

			#endregion
		}
}
