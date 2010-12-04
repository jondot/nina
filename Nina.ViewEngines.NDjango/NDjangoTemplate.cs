using System.Collections.Generic;
using System.IO;
using NDjango.Interfaces;

namespace Nina.ViewEngines.NDjango
{
    internal  class NDjangoTemplate : ITemplate
    {
        private string _template;
        private ITemplateManager _manager;
        private Dictionary<string, object> _dict;

        public NDjangoTemplate(string template, ITemplateManager manager)
        {
            _manager = manager;
            _template = template;
            _dict = new Dictionary<string, object>();
        }

        public void Render<T>(TextWriter output, T data)
        {
            _dict.Clear();
            _dict["ViewData"] = data;
            var reader = _manager.RenderTemplate(_template, _dict);
            char[] buffer = new char[4096];
            int count = 0;
            while ((count = reader.ReadBlock(buffer, 0, 4096)) > 0)
                output.Write(buffer, 0, count);
        }
    }
}