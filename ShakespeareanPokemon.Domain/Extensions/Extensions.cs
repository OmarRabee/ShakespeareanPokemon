using System.ComponentModel;
using System.Reflection;

namespace ShakespeareanPokemon.Domain.Extensions
{
   public static class Extensions
   {
      public static string GetDescription(this Enum value)
      {
         FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

         DescriptionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

         if (attributes != null && attributes.Any())
         {
            return attributes.First().Description;
         }

         return value.ToString();
      }
   }
}
