using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Web.XmlTransform;

namespace config_transform
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2 && args.Length != 3)
            {
                Console.Error.WriteLine("Usage: config-transform config.xml transform.xml [original.xml]. If original.xml is not provided, config.xml is overwritten.");
                return -1;
            }

            var transformer = new XmlTransformableDocument { PreserveWhitespace = true };

            var sourceIndex = args.Length == 3 ? 2 : 0;

            if (File.Exists(args[sourceIndex]) == false)
            {
                Console.Error.WriteLine(string.Format("Config file {0} could not be found.", args[sourceIndex]));
                return -2;
            }

            if (File.Exists(args[1]) == false)
            {
                Console.Error.WriteLine(string.Format("Transform file {0} could not be found.", args[1]));
                return -3;
            }

            transformer.Load(args[sourceIndex]);

            var transform = new XmlTransformation(args[1]);

            if (!transform.Apply(transformer))
            {
                Console.Error.WriteLine("Transformation failed.");
                return -4;
            }

            transformer.Save(args[0]);

            return 0;
        }
    }
}
