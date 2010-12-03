#region License
//
// Author: Dotan Nahum <dotan@paracode.com>
// Copyright (c) 2009-2010, Dotan Nahum, Paracode.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE.txt for details.
//
#endregion
using System.IO;
using System.Security.Cryptography;


namespace Nina.Support
{
    public class Digest
    {
        private static MD5CryptoServiceProvider _md5CryptoServiceProvider = new MD5CryptoServiceProvider();

        public static string MD5(Stream input)
        {
            var bts = _md5CryptoServiceProvider.ComputeHash(input);
            var s = new System.Text.StringBuilder();
            foreach (var b in bts)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        public static string CRC32(Stream input)
        {
            return Utils.CRC32.Compute(input).ToString();
        }
    }
}
