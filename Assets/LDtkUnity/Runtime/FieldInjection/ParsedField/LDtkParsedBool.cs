﻿using System;
using UnityEngine;

namespace LDtkUnity.FieldInjection
{
    public class LDtkParsedBool : ILDtkValueParser
    {
        public string TypeName => "Bool";

        public object ParseValue(object input)
        {
            return (bool)input;
            
            /*if (bool.TryParse(input, out bool value))
            {
                return value;
            }

            Debug.LogError($"LDtk: Was unable to parse Bool for {input}", LDtkInjectionErrorContext.Context);
            return default;*/
        }
    }
}