using Newtonsoft.Json;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicForecast.Clases.Helper
{
	public class JsonSerialize : ISerializer, IDeserializer
	{
		private readonly Newtonsoft.Json.JsonSerializer _serializer;

		public JsonSerialize(Newtonsoft.Json.JsonSerializer serializer)
		{
			this._serializer = serializer;
		}

		public string ContentType
		{
			get { return "application/json"; }
			set => ContentType = value;
		}

		public string DateFormat { get; set; }

		public string Namespace { get; set; }

		public string RootElement { get; set; }

		public string Serialize(object obj)
		{
            using var stringWriter = new StringWriter();
            using var jsonTextWriter = new JsonTextWriter(stringWriter);
            _serializer.Serialize(jsonTextWriter, obj);

            return stringWriter.ToString();
        }

		public T Deserialize<T>(RestSharp.IRestResponse response)
		{
			var content = response.Content;

            using var stringReader = new StringReader(content);
            using var jsonTextReader = new JsonTextReader(stringReader);
            return _serializer.Deserialize<T>(jsonTextReader);
        }

		public static JsonSerialize Default => new JsonSerialize(new Newtonsoft.Json.JsonSerializer()
		{
			NullValueHandling = NullValueHandling.Ignore
		});
	}
}
