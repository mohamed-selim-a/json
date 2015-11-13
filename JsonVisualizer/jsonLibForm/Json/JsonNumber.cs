﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json
{
    class JsonNumber : JsonValue
    {
        public double value;
        public JsonNumber(double value)
        {
            this.value = value;
        }

        public override string Render()
        {
            return value.ToString();
        }
    }
}