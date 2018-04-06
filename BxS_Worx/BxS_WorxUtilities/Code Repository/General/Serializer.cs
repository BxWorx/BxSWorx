using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_WorxUtil.General
{
	public class Serializer
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Serializer()
					{ }

			#endregion

			//===========================================================================================
			#region "Methods: Exposed"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public string Serialize<T>( T classObject )
					{
						var	lt_Types	= new List<Type>();
						return	this.Serialize( classObject , lt_Types );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public string Serialize<T>(		T						classObject
																		,	List<Type>	knownTypes	)
					{
						var lo_XWSettings		= new XmlWriterSettings	{		Indent							= true
																													,	OmitXmlDeclaration	= true
																													,	NewLineOnAttributes	= true	};

						var lo_StrBld	= new StringBuilder();

						using ( var lo_XMLWriter = XmlWriter.Create( lo_StrBld , lo_XWSettings ) )
							{
								var lo_XMLSer	= new DataContractSerializer( typeof(T) , knownTypes );

								lo_XMLSer.WriteObject( lo_XMLWriter , classObject );
								lo_XMLWriter.Flush();

								return	lo_StrBld.ToString();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T DeSerialize<T>( string xmlString )
					{
						var	lt_Types	= new List<Type>();

						return	this.DeSerialize<T>(	xmlString	:	xmlString
																				,	knownTypes: lt_Types	);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public T DeSerialize<T>(	string			xmlString
																,	List<Type>	knownTypes	)
					{
						using ( var lo_XMLReader = XmlReader.Create( new StringReader( xmlString ) ) )
							{
								var lo_serializer = new DataContractSerializer( typeof(T) , knownTypes );
								return (T)lo_serializer.ReadObject( lo_XMLReader );
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void DeSerialize<T>(		string	xmlString
																		,	ref T		classObject	)
					{
						var	lt_Types	= new List<Type>();

						this.DeSerialize(		xmlString
															,	ref classObject
															,	lt_Types				);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void DeSerialize<T>(		string			xmlString
																		,	ref T				classObject
																		,	List<Type>	knownTypes	)
					{
						using ( var lo_XMLReader = XmlReader.Create( new StringReader( xmlString ) ) )
							{
								var lo_serializer = new DataContractSerializer( typeof(T) , knownTypes );
								classObject	= (T)lo_serializer.ReadObject( lo_XMLReader );
							}
					}

			#endregion

		}
}