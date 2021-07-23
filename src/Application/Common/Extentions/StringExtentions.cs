using System;
namespace CleanArchitecture.Application.Common.Extentions
{
    public static class StringExtentions
    {
        public static byte[] GetBase64(this String str)
        {
            return Convert.FromBase64String(str.Substring(str.LastIndexOf(',') + 1));
        }
    }
}
