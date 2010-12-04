using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Razor;
using Microsoft.CSharp;

namespace Nina.ViewEngines.Razor
{
    internal class NinaRazorTemplateEngine : ITemplateEngine
    {
        public const string EXT = "cshtml";

        public ITemplate Compile<T>(string template)
        {
            throw new NotImplementedException("in progress..");
        }

      
    }

    public abstract class DataView<T> 
    {
        public T ViewData { get; set; }
    }

}
    