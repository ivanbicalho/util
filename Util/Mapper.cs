using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Mapper
    {
        public static TOut Map<TIn, TOut>(TIn objTIn) where TIn : class where TOut : class, new()
        {
            var objTOut = Activator.CreateInstance<TOut>();
            return ExecuteMap(objTIn, objTOut);
        }

        public static TOut Map<TIn, TOut>(TIn objTIn, TOut objTOut) where TIn : class where TOut : class, new()
        {
            return ExecuteMap<TIn, TOut>(objTIn, objTOut);
        }

        private static TOut ExecuteMap<TIn, TOut>(TIn objTIn, TOut objTOut) where TIn : class where TOut : class, new()
        {
            if (objTIn == null) { return null; }
            if (objTOut == null) { objTOut = Activator.CreateInstance<TOut>(); }

            foreach (var propTOut in objTOut.GetType().GetProperties())
            {
                var propTIn = objTIn.GetType().GetProperty(propTOut.Name);
                if (propTIn == null) { continue; }

                if (propTIn.PropertyType == propTOut.PropertyType
                    || propTIn.PropertyType == Nullable.GetUnderlyingType(propTOut.PropertyType)
                    || Nullable.GetUnderlyingType(propTIn.PropertyType) == propTOut.PropertyType && propTIn.GetValue(objTIn) != null)
                {
                    // Seta no objTOut a propriedade do objTIn
                    propTOut.SetValue(objTOut, propTIn.GetValue(objTIn));
                    continue;
                }

                if (propTIn.PropertyType.IsClass && propTOut.PropertyType.IsClass)
                {
                    // Executa o método Mapper.Map novamente, porém agora com uma propriedade do objTOut. 
                    // Ex: Mapper.Map<TIn, TOut>(Obj)
                    // Onde, TIn = Tipo da propriedade do objeto de entrada
                    //       TOut = Tipo da propriedade do objeto de saída
                    //       Obj = Valor da propriedade do objeto de entrada

                    var mapMethod = typeof(Mapper).GetMethod("ExecuteMap", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(propTIn.PropertyType, propTOut.PropertyType);
                    var objTOutMapMethod = mapMethod.Invoke(null, new[] { propTIn.GetValue(objTIn), propTOut.GetValue(objTOut) });

                    propTOut.SetValue(objTOut, objTOutMapMethod);
                }
            }

            return objTOut;
        }
    }
}
