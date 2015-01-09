using System;
using System.IO;
using Microsoft.Web.XmlTransform;

namespace config_transform
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 && args.Length != 3)
                throw new ArgumentException("Usage: config-transform config.xml transform.xml [original.xml]. If original.xml is not provided, config.xml is overwritten.");

            var transformer = new XmlTransformableDocument { PreserveWhitespace = true };

            var sourceIndex = args.Length == 3 ? 2 : 0;

            if (File.Exists(args[sourceIndex]) == false)
                throw new FileNotFoundException(string.Format("Config file {0} could not be found.", args[sourceIndex]));

            if (File.Exists(args[1]) == false)
                throw new FileNotFoundException(string.Format("Transform file {0} could not be found.", args[1]));

            transformer.Load(args[sourceIndex]);

            var transform = new XmlTransformation(args[1]);

            if (!transform.Apply(transformer))
                throw new Exception("Transformation failed.");

            transformer.Save(args[0]);
        }
    }
}
