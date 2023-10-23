using System;
using System.Collections.Generic;

namespace RsGEN.Utils
{
    public static class Helper
    {
        private static Random RANDOM;

        public static T GetRandomValueFromCollection<T>(List<T> collection)
        {
            return collection[(RANDOM ??= new Random()).Next(collection.Count)];
        }

        public static T GetRandomValueFromCollection<T>(T[] collection)
        {
            return collection[(RANDOM ??= new Random()).Next(collection.Length)];
        }

        public static Enum[] GetFlags(Enum input)
        {
            var flags = new List<Enum>();

            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    flags.Add(value);

            return flags.ToArray();
        }

        public static string GetStringValue(this Enum value)
        {
            var input = value.ToString();
            var result = "";

            foreach (var t in input)
            {
                if (char.IsUpper(t)) result += ' ';

                result += t;
            }

            return result.Trim();
        }

        public static byte Clamp(byte value, byte min, byte max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static byte Ceil(float f)
        {
            return (byte)Math.Ceiling(f);
        }
    }
}