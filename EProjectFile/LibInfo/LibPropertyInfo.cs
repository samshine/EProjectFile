﻿using Newtonsoft.Json;

namespace QIQI.EProjectFile.LibInfo
{
    public class LibPropertyInfo
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
