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
        #region Properties

        private const string EXCEPTION_DUPLICATE_NAMES = "Existem propriedades com o mesmo nome. Para utilizar o mapeamento com 'IgnoreCase', as propriedades devem ter nomes distintos";

        #endregion

        #region Public Methods

        public static TOut Map<TIn, TOut>(TIn objTIn, bool ignoreCase = false)
            where TIn : class
            where TOut : class, new()
        {
            var objTOut = Activator.CreateInstance<TOut>();
            return ExecuteMap(objTIn, objTOut, ignoreCase);
        }

        public static TOut Map<TIn, TOut>(TIn objTIn, TOut objTOut, bool ignoreCase = false)
            where TIn : class
            where TOut : class, new()
        {
            return ExecuteMap<TIn, TOut>(objTIn, objTOut, ignoreCase);
        }

        public static IEnumerable<TOut> MapList<TIn, TOut>(IEnumerable<TIn> listTIn, bool ignoreCase = false)
            where TIn : class
            where TOut : class, new()
        {
            var listTOut = new List<TOut>();
            return ExecuteMapList(listTIn, listTOut, ignoreCase);
        }

        public static IEnumerable<TOut> MapList<TIn, TOut>(IEnumerable<TIn> listTIn, List<TOut> listTOut, bool ignoreCase = false)
            where TIn : class
            where TOut : class, new()
        {
            return ExecuteMapList(listTIn, listTOut, ignoreCase);
        }

        #endregion

        #region Private Methods

        private static TOut ExecuteMap<TIn, TOut>(TIn objTIn, TOut objTOut, bool ignoreCase)
            where TIn : class
            where TOut : class, new()
        {
            if (objTIn == null) { return null; }
            if (objTOut == null) { objTOut = Activator.CreateInstance<TOut>(); }

            if (ignoreCase) { ValidateCase(objTIn, objTOut); }

            foreach (var propTOut in objTOut.GetType().GetProperties())
            {
                PropertyInfo propTIn;

                if (ignoreCase)
                    propTIn = objTIn.GetType().GetProperty(propTOut.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                else
                    propTIn = objTIn.GetType().GetProperty(propTOut.Name);

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

        private static IEnumerable<TOut> ExecuteMapList<TIn, TOut>(IEnumerable<TIn> listTIn, List<TOut> listTOut, bool ignoreCase)
            where TIn : class
            where TOut : class, new()
        {
            if (listTIn == null) { return null; }
            if (listTOut == null) { listTOut = new List<TOut>(); }

            foreach (var objTIn in listTIn)
            {
                var objTOut = Activator.CreateInstance<TOut>();
                listTOut.Add(ExecuteMap(objTIn, objTOut, ignoreCase));
            }

            return listTOut;
        }

        private static void ValidateCase<TIn, TOut>(TIn objTIn, TOut objTOut)
            where TIn : class
            where TOut : class, new()
        {
            var duplicateTInNames = objTIn.GetType().GetProperties().OfType<PropertyInfo>()
                .GroupBy(item => item.Name.ToLower())
                .Where(group => group.Count() > 1)
                .Count();

            var duplicateTOutNames = objTOut.GetType().GetProperties().OfType<PropertyInfo>()
                .GroupBy(item => item.Name.ToLower())
                .Where(group => group.Count() > 1)
                .Count();

            if (duplicateTInNames > 0 || duplicateTOutNames > 0)
            {
                throw new Exception(EXCEPTION_DUPLICATE_NAMES);
            }
        }

        #endregion
    }
}
