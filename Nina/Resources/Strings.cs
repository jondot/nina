#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
namespace Nina.StringResources
{
    class Strings
    {
        public const string HTML_404 = @"<html><body><div style=""text-align:center""><img src=""{NINA404}/nina404.png""/><br/>
                                            Nina doesn't know this trick.
<pre>

//Global.asax.cs
protected void Application_Start(object sender, EventArgs e)
{
	RouteTable.Routes.Add(new MountingPoint<HelloWorld>(""/""));
}

//HelloWorld.cs
class HelloWorld : Nina
{
    public HelloWorld()
    {
        Get(""hello"", (m, c) =>
        {
            Text(""hello world!"");
        });
    }
}
</pre>
</body></html>";
    }
}
