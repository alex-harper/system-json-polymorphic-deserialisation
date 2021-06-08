using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymorphicDeserialisationDemo.Models
{
    public interface ILayer : ITypeDiscriminator
    {        
        public int Height { get; set; }

        public string Id { get; set; }

        public int? Index { get; set; }
    }

    public class ALayer : ILayer
    {
        public string TypeDiscriminator => nameof(ALayer);

        public int InPoint { get; set; }

        public bool Is3D { get; set; } = false;

        //ILayer
        public int Height { get; set; }
        public string Id { get; set; }
        public int? Index { get; set; }
    }

    public class BLayer : ILayer
    {
        public string TypeDiscriminator => nameof(BLayer);

        public string Name { get; set; }

        public int OutPoint { get; set; }

        //ILayer
        public int Height { get; set; }
        public string Id { get; set; }
        public int? Index { get; set; }
    }
}
