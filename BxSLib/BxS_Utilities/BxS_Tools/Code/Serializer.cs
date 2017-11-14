using System;
using System.IO;
using System.Runtime.Serialization;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace Toolset.IO
{
	public class Serializer
		{
			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool SerialiseViaDataContract<T>(T ClassObject, string FullFileName)
					{
						var lb_Ret	= true;
						//.............................................
						try
							{
								using (FileStream lo_FSWriter	= File.Create(FullFileName, 0, FileOptions.None))
									{
										var	lo_Ser	= new DataContractSerializer(typeof(T));
										lo_Ser.WriteObject(lo_FSWriter, ClassObject);
									}
							}
						catch (System.Exception)
							{
								lb_Ret	= false;
							}
						//.............................................
						return	lb_Ret;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T DeSerialiseViaDataContract<T>(string FullFileName)
					{
						T	lo_Ret;
						//.............................................
						try
							{
								using (var lo_FS = new FileStream(FullFileName,	FileMode.Open, FileAccess.Read))
									{
										var	lo_Ser	= new DataContractSerializer(typeof(T));
										lo_Ret			= (T)lo_Ser.ReadObject(lo_FS);
									}
							}
						catch (Exception)
							{
								lo_Ret	= default(T);
							}
						//.............................................
						return	lo_Ret;
					}

			#endregion
		}
}