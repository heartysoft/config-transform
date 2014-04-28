using System;
using System.IO;
using Microsoft.Web.XmlTransform;

namespace config_transform
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("Usage: config-transform config.xml transform.xml");

            var transformer = new XmlTransformableDocument { PreserveWhitespace = true };

            if (File.Exists(args[0]) == false)
                throw new FileNotFoundException(string.Format("Config file {0} could not be found.", args[0]));

            if (File.Exists(args[1]) == false)
                throw new FileNotFoundException(string.Format("Transform file {0} could not be found.", args[1]));

            transformer.Load(args[0]);

            var transform = new XmlTransformation(args[1]);

            if (!transform.Apply(transformer))
                throw new Exception("Transformation failed.");

            transformer.Save(args[0]);
        }
    }
}
