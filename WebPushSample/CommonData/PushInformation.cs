using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonData
{
	public class PushInformation
	{
		public string VapidPublic { get; set; }
		public string VapipPrivate { get; set; }

		public List<NotificationTarget> Targets { get; set; }

		public PushInformation()
		{
			Targets = new List<NotificationTarget>();
		}

		public static PushInformation Load(string fileName)
		{
			try
			{
				using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				{
					var serializer = new XmlSerializer(typeof(PushInformation));
					return (PushInformation)serializer.Deserialize(stream);
				}
			}
			catch
			{
			}
			return null;
		}

		public static void Save(PushInformation data, string fileName)
		{
			try
			{
				using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
				{
					var serializer = new XmlSerializer(typeof(PushInformation));
					serializer.Serialize(stream, data);
				}
			}
			catch
			{
			}
		}
	}

	public class NotificationTarget
	{
		public string EndPoint { get; set; }
		public string PublicKey { get; set; }
		public string AuthSecret { get; set; }
		public string ContentEncoding { get; set; }
	}
}
