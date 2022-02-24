using System.Text;
using Newtonsoft.Json;

namespace CTA.BlazorWasm.Shared.Services
{
    public static class SerializeAndEncode
    {
        public static async Task<string> ObjectToJsonAndEncode(object objectToSerialize)
        {
            var json = await Task.Run(() => JsonConvert.SerializeObject(objectToSerialize));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        public static async Task<string> EncodedStringToJson(string encodedString)
        {
            byte[] byteArray = await Task.Run(() => Convert.FromBase64String(encodedString));
            return Encoding.UTF8.GetString(byteArray);
        }

    }
}
