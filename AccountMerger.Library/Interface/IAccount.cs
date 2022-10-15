﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Interface
{
    public interface IAccount
    {
        public string Application { get; set; }
        public ISet<string> Emails { get; set; }
        public string Name { get; set; }
    }
}
