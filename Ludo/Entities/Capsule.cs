using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Ludo.Entities
{
    [Serializable]
    public class Capsule
    {
        public string Message { get; set; }

        public JObject Data { get; set; } = new JObject();
        public bool State { get; set; }
    }
}