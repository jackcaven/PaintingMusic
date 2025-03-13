using MusicGeneration.Models.Som.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGeneration.Models.Som
{
    class AttributeMapperEngine(SomConfiguration configuration)
    {
        private readonly Dictionary<string, Delegate> methodMappings = new();
    }
}
