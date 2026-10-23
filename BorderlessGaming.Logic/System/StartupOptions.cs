using CommandLine;
using CommandLine.Text;

namespace BorderlessGaming.Logic.System
{
    public class StartupOptions
    {
        [Option('m', "minimize", DefaultValue = false, HelpText = "Inicia la aplicaci\u00f3n minimizada.")]
        public bool Minimize { get; set; }

        [Option('s', "silent", DefaultValue = false, HelpText = "Inicia la aplicaci\u00f3n en modo silencioso.")]
        public bool Silent { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
