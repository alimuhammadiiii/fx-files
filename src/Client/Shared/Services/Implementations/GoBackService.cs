﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionland.FxFiles.Client.Shared.Services.Implementations
{
    public class GoBackService : IGoBackService
    {
        public Func<Task>? GoBackAsync { get; set; } = null;
    }
}