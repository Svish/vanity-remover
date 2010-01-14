namespace Geeky.VanityRemover.Core
{
    public class OptionsParser
    {
        public Options Parse(params string[] arguments)
        {
            var options = Options.Default;

            if (arguments == null)
                return options;

            foreach(var a in arguments)
            {
                switch(a.ToLowerInvariant())
                {
                    case "-q":
                        options.Quiet = true;
                        break;
                    case "-s":
                        options.Silent = true;
                        break;
                    case "-a":
                        options.CleanAll = true;
                        break;
                    case "-v":
                        options.Verbose = true;
                        break;
                }
            }
            
            return options;
        }
    }
}