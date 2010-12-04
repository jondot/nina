using System;
using System.IO;
using System.Web;
using NDjango.Interfaces;

namespace Nina.ViewEngines.NDjango
{
    internal class IntegrationTemplateLoader : ITemplateLoader
    {
        public TextReader GetTemplate(string path)
        {
            return File.OpenText(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, path+"."+DjangoEngine.EXT));
        }

        public bool IsUpdated(string path, DateTime timestamp)
        {
            return false;
        }
    }
}