#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.Web;

namespace Nina.Filters
{
    /// <summary>
    /// Filters are small units of a context's pipeline (chain of command).
    /// In contrast to a result, they can happen at any stage of the request or response.
    /// They too, are composite, and are able to stop execution of their descendant filters.
    /// 
    /// You can also compose filters behavior into a result through the BeforeResultFilter and
    /// and AfterResultFilter.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Execute the filter and return wether to continue
        /// </summary>
        /// <param name="c">context</param>
        /// <returns>true if to continue; false to break</returns>
        bool Execute(HttpContext c);
    }
}