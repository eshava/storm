﻿using System;
using System.Data;

namespace Eshava.Storm.Models
{
	internal struct DeserializerState
    {
        public readonly int Hash;
        public readonly Func<IDataReader, object> Func;

        public DeserializerState(int hash, Func<IDataReader, object> func)
        {
            Hash = hash;
            Func = func;
        }
    }
}