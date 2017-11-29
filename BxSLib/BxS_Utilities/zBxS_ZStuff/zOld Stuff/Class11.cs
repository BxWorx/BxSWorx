using System;
using System.Collections.Generic;
using System.Text;

namespace BxS_SAPGUI.Code.zOld_Stuff
{
		class Class11
		{
		}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class Program
{
		public static void Main()
		{
				const string xml = @"<myXML>
		<MyGroup>
				<source>OCU</source> 
				<endianity>
						<set>LITTLE</set><!--LITTLE/BIG Endian-->
				</endianity>
			 <msgNumber>
					<Number>5</Number>
					<Field>
						<name>Pos_X</name>
						<entry>
								 <ByteOffset>
										<offset>8</offset>
								 </ByteOffset>
								 <ByteSize>
										<Size>4</Size>
								 </ByteSize>
						</entry>
					</Field>
					<Field>
						<name>Pos_Y</name>
						<entry>
								 <ByteOffset>
										<offset>12</offset>
								 </ByteOffset>
								 <ByteSize>
										<Size>4</Size>
								 </ByteSize>
						</entry>
					</Field>
			 </msgNumber>
		</MyGroup>
</myXML>";

				var doc = XDocument.Parse(xml);

				var msgs =
						from msg in doc.Descendants("msgNumber")
						select new Message
						{
								Number = (int) msg.Element("Number"),
								Fields = msg
										.Elements("Field")
										.Select(x => new Field
										{
												Name = (string) x.Element("name"),
												ByteOffset = (int) x.Descendants("offset").Single(),
												ByteSize = (int) x.Descendants("Size").Single(),
										})
										.ToList()
						};

				foreach (var msg in msgs)
				{
			Console.WriteLine("Msg: {0}", msg.Number);

						foreach (var field in msg.Fields)
						{
								Console.WriteLine("{0}: Offset {1} Size {2}", field.Name, field.ByteOffset, field.ByteSize);
						}
				}
		}
}

public class Message
{
		public int Number { get; set; }
		public List<Field> Fields { get; set; }
}

public class Field
{
		public string Name { get; set; }
		public int ByteOffset { get; set; }
		public int ByteSize { get; set; }
}