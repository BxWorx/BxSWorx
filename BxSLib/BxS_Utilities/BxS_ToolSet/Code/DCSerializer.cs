using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_Toolset.Serialize
{
	public class ObjSerializer
		{
			#region "Methods: Exposed"

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public string Serialize<T>(T classObject)
						{
							try
								{
									var lo_XWSettings = new XmlWriterSettings
										{	Indent							= true	,
											OmitXmlDeclaration	= true	,
											NewLineOnAttributes	= true		};

									var lo_StrBld	= new StringBuilder();

									using (var lo_XMLWriter = XmlWriter.Create(lo_StrBld, lo_XWSettings))
										{
											var lo_XMLSer	= new DataContractSerializer(typeof(T));
											lo_XMLSer.WriteObject(lo_XMLWriter, classObject);
											lo_XMLWriter.Flush();

											return	lo_StrBld.ToString();
										}
								}
							catch (Exception)
								{
									return	string.Empty;
								}
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public T DeSerialize<T>(string xmlString)
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

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void DeSerialize<T>(string xmlString, ref T classObject)
						{
							try
								{
									using (var lo_XMLReader = XmlReader.Create(new StringReader(xmlString)))
										{
											var lo_serializer = new DataContractSerializer(typeof(T));
											classObject	= (T)lo_serializer.ReadObject(lo_XMLReader);
										}
								}
							catch (Exception)
								{
									classObject	=	default(T);
								}
						}

			#endregion

		}
}