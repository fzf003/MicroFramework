﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFramework
{
    public interface IQueryBus
    {
        System.Threading.Tasks.Task<TResult> QueryAsync<TResult>(MicroFramework.IQuery<TResult> query);
    }
}
