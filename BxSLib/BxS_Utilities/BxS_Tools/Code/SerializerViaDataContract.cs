using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace Toolset.Serialize
{
	public class SerializerViaDataContract
		{
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public bool SerialiseToFile<T>(T ClassObject, string FullFileName)
					{
						bool lb_Ret	= true;
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
				public T DeSerialiseFromFile<T>(string FullFileName)
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

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public string SerializeObject<T>(T classObject)
						{
							using (var lo_memStream = new MemoryStream())
								{
									var lo_serializer = new DataContractSerializer(typeof(T));

									lo_serializer.WriteObject(lo_memStream, classObject);

									lo_memStream.Seek(0, SeekOrigin.Begin);

									using (var lo_streamReader = new StreamReader(lo_memStream))
										{
											return lo_streamReader.ReadToEnd();
										}
								}
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public T DeSerializeObject<T>(string xmlString)
						{
							try
								{
									using (var lo_XMLReader = XmlReader.Create(new StringReader(xmlString)))
										{
											var lo_serializer = new DataContractSerializer(typeof(T));
											return (T)lo_serializer.ReadObject(lo_XMLReader);
										}
								}
							catch (Exception)
								{
									return	default(T);
								}
						}

			#endregion

		}
}