﻿using System;

namespace MagicalLifeAPI.InternalExceptions
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException() : base("A duplicate entry was detected")
        {
        }

        public DuplicateEntryException(string msg) : base(msg)
        {
        }
    }
}
