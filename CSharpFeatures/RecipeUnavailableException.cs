using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures
{
  
    [Serializable]
    public class RecipeUnavailableException : Exception
    {
        public RecipeUnavailableException() { }
        public RecipeUnavailableException(string message) : base(message) { }
        public RecipeUnavailableException(string message, Exception inner) : base(message, inner) { }
        protected RecipeUnavailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
