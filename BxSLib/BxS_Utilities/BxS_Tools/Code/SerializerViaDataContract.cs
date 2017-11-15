using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace Toolset.Serialize
{
	public class SerializerViaDataContract
		{
			#region "Methods: Exposed"

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
					internal string Serialize<T>(T classObject)
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

			#endregion

		}
}