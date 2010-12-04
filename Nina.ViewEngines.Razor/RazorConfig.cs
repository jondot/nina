using Nina.ViewEngines.Razor;

namespace Nina.Configuration
{
    public static class RazorConfig
    {
        public static void WithRazor(this ViewConfiguration config)
        {
            config.ViewEngineExtension = "haml";
            config.Engine = new NinaRazorTemplateEngine();
        }
    }
}
