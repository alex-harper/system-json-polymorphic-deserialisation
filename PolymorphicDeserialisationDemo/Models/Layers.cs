using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PolymorphicDeserialisationDemo.Models
{
    public abstract class Layer
    {
        [JsonPropertyName("ty")]
        public abstract int TypeDiscriminator { get; }

        public int Height { get; set; }

        public string Id { get; set; }

        public int? Index { get; set; }
    }

    public class ALayer : Layer
    {
        [JsonPropertyName("ty")]
        public override int TypeDiscriminator => 1;

        public int InPoint { get; set; }

        public bool Is3D { get; set; } = false;
    }

    public class BLayer : Layer
    {
        [JsonPropertyName("ty")]
        public override int TypeDiscriminator => 2;
        
        public string Name { get; set; }

        public int OutPoint { get; set; }        
    }
}
