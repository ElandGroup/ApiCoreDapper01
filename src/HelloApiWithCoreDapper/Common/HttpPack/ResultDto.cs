using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloApiWithCoreDapper.Common.HttpPack
{
    public class ResultDto
    {
        public bool success { get; set; }
        public object result { get; set; }
        public ErrorDto errorDto { get; set; }     
    }
}
