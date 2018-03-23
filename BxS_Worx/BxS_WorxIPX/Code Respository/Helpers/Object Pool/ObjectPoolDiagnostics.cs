using System.Threading;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxIPX.Helpers.ObjectPool
{
	public class ObjectPoolDiagnostics
		{
			#region "Declarations"

				internal	int _ResetFailedCount;
				internal	int _ReturnedByRessurectionCount;
				internal	int _HitCount;
				internal	int _MissCount;
				internal	int _InstancesCreated;
				internal	int _InstancesDestroyed;
				internal	int _OverflowCount;
				internal	int _ReturnedCount;

			#endregion

			//===========================================================================================
			#region "Properties"

				public	int LiveInstancesCount						{	get { return	this._InstancesCreated - this._InstancesDestroyed	; }	}
				public	int ResetFailedCount							{	get { return	this._ResetFailedCount														; }	}
				public	int ReturnedByRessurectionCount		{	get { return	this._ReturnedByRessurectionCount									; }	}
				public	int HitCount											{	get { return	this._HitCount																		; }	}
				public	int MissCount											{	get { return	this._MissCount																		; }	}
				public	int InstancesCreated							{	get { return	this._InstancesCreated														; }	}
				public	int InstancesDestroyed						{	get { return	this._InstancesDestroyed													; }	}
				public	int OverflowCount									{	get { return	this._OverflowCount																; }	}
				public	int ReturnedCount									{	get { return	this._ReturnedCount																; }	}

			#endregion

			//===========================================================================================
			#region "Methods: Internal"

				internal	void	UpCreatedCount			()	{	Add( ref	this._InstancesCreated						);	}
				internal	void	UpDestroyedCount		()	{ Add( ref	this._InstancesDestroyed					);	}
				internal	void	UpHitCount					()	{	Add( ref	this._HitCount										);	}
				internal	void	UpMissCount					()	{	Add( ref	this._MissCount										);	}
				internal	void	UpOverflowCount			()	{	Add( ref	this._OverflowCount								);	}
				internal	void	UpResetFailedCount	()	{	Add( ref	this._ResetFailedCount						);	}
				internal	void	UpRessurectionCount	()	{	Add( ref	this._ReturnedByRessurectionCount	);	}
				internal	void	UpReturnedCount			()	{	Add( ref	this._ReturnedCount								);	}

			#endregion

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private static void Add( ref int Count )=> Interlocked.Increment( ref Count );
	}
}


