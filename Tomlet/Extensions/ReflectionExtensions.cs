﻿using System;
using System.Linq;
using System.Reflection;

namespace Tomlet.Extensions
{
    internal static class ReflectionExtensions
    {
        internal static bool TryGetBestMatchConstructor(this Type type, out ConstructorInfo? bestMatchConstructor)
        {
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length == 0)
            {
                bestMatchConstructor = null;
                return false;
            }

            var parameterlessConstructor = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);
            if (parameterlessConstructor != null)
            {
                bestMatchConstructor = parameterlessConstructor;
                return true;
            }

            var parameterizedConstructors = constructors.Where(c => c.GetParameters().Length > 0).ToArray();
            if (parameterizedConstructors.Length > 1)
            {
                bestMatchConstructor = null;
                return false;
            }

            bestMatchConstructor = parameterizedConstructors.Single();
            return true;
        }

        internal static bool IsIntegerType(this Type type) {
            switch (Type.GetTypeCode(type)) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.Int16:
                case TypeCode.UInt32:
                case TypeCode.Int32:
                case TypeCode.UInt64:
                case TypeCode.Int64:
                    return true;
                default:
                    return false;
            }
        }

    }
}