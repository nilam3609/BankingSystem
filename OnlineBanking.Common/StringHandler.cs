using System;

namespace OnlineBanking.Common
{
    public static class StringHandler
    {
        public static string InsertSpace(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                name += " ";
            }
            return name;
        }


        // cant create non static method into static 
        //public string NonStaticInsertSpace(string name)
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        name = name.Trim();
        //    }
        //    return name;
        //}

        public static string  InsertSpaceExtentedMethod(this string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                name += " ";
            }
            return name;
        }
    }
}
