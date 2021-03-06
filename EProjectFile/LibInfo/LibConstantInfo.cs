﻿using Newtonsoft.Json;

namespace QIQI.EProjectFile.LibInfo
{
    public class LibConstantInfo
    {
        public string Name { get; set; }
        public string EnglshName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
