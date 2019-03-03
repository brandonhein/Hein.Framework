﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hein.Framework.Dynamo.Criterion
{
    public static class QueryOver
    {
        public static IQueryCriteria<T> Of<T>()
        {
            return new QueryCriteria<T>();
        }
    }
}
