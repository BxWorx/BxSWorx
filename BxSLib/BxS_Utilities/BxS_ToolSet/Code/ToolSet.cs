using System;
using System.Collections.Generic;
using System.Threading;
using BxS_Toolset.DataContainer;
//.........................................................
using BxS_Toolset.IODisk;
using BxS_Toolset.Serialize;
using BxS_Toolset.Queue;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset
{
	public class ToolSet
		{
			#region "Declarations"

				private readonly	Lazy<IO>						_IO		= new Lazy<IO>						( () => new IO()						,	LazyThreadSafetyMode.ExecutionAndPublication );
				private readonly	Lazy<ObjSerializer>	_Ser	= new Lazy<ObjSerializer>	( () => new ObjSerializer()	,	LazyThreadSafetyMode.ExecutionAndPublication );

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public QueueManager<TCls>	CreateQueueManager<TCls>(int NoQueues = 3, bool IncludeZeroQue = true)
																		where TCls: class
					{
						var lo_QM	= new QueueManager<TCls>(NoQueues);
						int ln_St	= 1;
						int ln_En	= lo_QM.MaxQueues;
						//.............................................
						if (IncludeZeroQue)		ln_St = 0;
						//.............................................
						for (int i = ln_St; i <= ln_En; i++)
							{
								lo_QM.AddQueue(this.CreateQueue<TCls>(), i);
							}
						//.............................................
						return	lo_QM;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public BxSQueue<TCls>	CreateQueue<TCls>()
															where TCls: class
					{
						return	new BxSQueue<TCls>();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DataTable<TCls, TKey>	CreateDataTable<TCls, TKey>(	Func<TKey, TCls>	createNew	)
																				where TCls: class
					{
						return	new DataTable<TCls, TKey>(createNew);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTController<TCls, TKey>	CreateDTController<TCls, TKey>(	string						fullPathName	,
																																				Func<TKey, TCls>	createNew			,
																																				bool							autoLoad			= true	)
																					where TCls: class
					{
						var lt_Types	=new List<Type>();
						return	this.CreateDTController<TCls, TKey>(fullPathName, createNew, lt_Types, autoLoad);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public DTController<TCls, TKey> CreateDTController<TCls, TKey>(	string						fullPathName	,
																																				Func<TKey, TCls>	createNew			,
																																				List<Type>        knownTypes		,
																																				bool							autoLoad			= true	)
																					where TCls: class
					{
						return	new DTController<TCls, TKey>(	this._IO	.Value	,
																									this._Ser	.Value	,
																									fullPathName			,
																									createNew					,
																									knownTypes				,
																									autoLoad						);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public IO GetIO()
					{
						return	this._IO.Value;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ObjSerializer GetSerlizser()
					{
						return	this._Ser.Value;
					}

			#endregion

		}
}
