using System;
using Newtonsoft.Json.Linq;

namespace Ludo.Entities
{
    [Serializable]
    public class Capsule
    {
        public string Message { get; set; }
        public JObject Data { get; } = new JObject();
        public bool State { get; set; } = true;

        public Capsule Set(string key, JToken value)
        {
            Data[key] = value;
            return this;
        }

        public Capsule Info(string msg)
        {
            Message = msg;
            return this;
        }

        public Capsule Fail()
        {
            State = false;
            return this;
        }
    }
}