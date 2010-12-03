#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System;

namespace Nina
{
	public class ApplicationException : Exception
	{
	    public ApplicationException(string message) : base(message)
	    {
	    }
	}
}
