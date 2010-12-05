using System.IO;

namespace Nina.ViewEngines.Razor
{
    class RazorTemplate : ITemplate
    {
        private readonly RazorEngine.Templating.ITemplate _innerTemplate;

        public RazorTemplate(RazorEngine.Templating.ITemplate innerTemplate)
        {
            _innerTemplate = innerTemplate;
        }

        public void Render<T>(TextWriter output, T data)
        {
            var dynamicInstance = _innerTemplate as RazorEngine.Templating.ITemplate<dynamic>;
            if (dynamicInstance != null)
                dynamicInstance.Model = data;

            var typedInstance = _innerTemplate as RazorEngine.Templating.ITemplate<T>;
            if (typedInstance != null)
                typedInstance.Model = data;

            _innerTemplate.Execute();
            output.Write(_innerTemplate.Result);
        }
    }
}
