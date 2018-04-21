﻿using System;
//.........................................................
using BxS_WorxUtil.General;
using BxS_WorxUtil.ObjectPool;
using BxS_WorxUtil.Progress;

using static	BxS_WorxUtil.Main.UTL_Constants;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.Main
{
	public sealed class UTL_Controller : IUTL_Controller
		{
			#region "Constructors: Singleton"

				//.................................................
				private	static readonly	Lazy< UTL_Controller >	_Instance		= new		Lazy< UTL_Controller >( ()=>		new UTL_Controller() , cz_LM );
				public	static					UTL_Controller					Instance		{	get { return _Instance.Value; }	}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private UTL_Controller()
					{
						this._IO				= new	Lazy< IO >					( ()=> new	IO()					);
						this._Serlizer	= new	Lazy< Serializer >	( ()=> new	Serializer()	);
					}

			#endregion

			//===========================================================================================
			#region "Declarations"

				private	readonly Lazy< IO >						_IO;
				private	readonly Lazy< Serializer >		_Serlizer;

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public	IO					CreateIO					()=> this._IO				.Value	;
				public	Serializer	CreateSerializer	()=> this._Serlizer	.Value	;

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public PriorityQueue		<T> CreatePriorityQueue	<T>	()													where T: class				=>	new PriorityQueue		<T>();
				public ObjectPool				<T>	CreateObjectPool		<T>	(	Func<T>	factory	= null )	where T: PooledObject	=>	new ObjectPool			<T>( factory );

				public ProgressHandler	<T>	CreateProgressHandler	<T>	(		Func<T>	factory
																																,	int			reportInterval	= 10 )	where T: class	=>	new ProgressHandler	<T>(	factory
																																																																							, reportInterval );

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨


			#endregion

		}
}