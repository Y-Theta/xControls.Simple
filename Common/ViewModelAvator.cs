using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.CodeDom;
using System.Reflection.PortableExecutable;
using static xControl.Simple.Common.ViewModelBase;

namespace xControl.Simple.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelAvator
    {
        internal static Dictionary<Type, Type> _avatorCache;
        private static AssemblyBuilder _dynamicAssembly;
        private static ModuleBuilder _dynamicModule;

        public static object CreateViewModelAvator(object instance)
        {
            Type ttype = instance.GetType();
            if (!_avatorCache.ContainsKey(ttype))
            {
                CreateViewModelAvatorT(ttype);
            }
            return Activator.CreateInstance(_avatorCache[ttype], new object[0]);
        }

        private static void CreateViewModelAvatorT(Type ttype)
        {
            var typeBuilder = _dynamicModule.DefineType($"{ttype.Name}_Avt",
                TypeAttributes.Public | TypeAttributes.Class,
                ttype);

            #region   createPropAvator
            var notify = typeof(ViewModelBase).GetMethod("Notify", BindingFlags.Instance | BindingFlags.NonPublic);
            var allprops = ttype.GetProperties();
            foreach (var prop in allprops)
            {
                if (prop.GetCustomAttribute<ObservablePropAttribute>() is ObservablePropAttribute attr)
                {
                    CreatePropAvator(typeBuilder, prop, notify);
                }
            }
            #endregion

            var ctorbuilder = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.SpecialName,
               CallingConventions.Standard, new Type[0]);
            ILGenerator myConstructorIL = ctorbuilder.GetILGenerator();
            myConstructorIL.Emit(OpCodes.Ldarg_0);
            myConstructorIL.Emit(OpCodes.Call, ttype.GetConstructor(new Type[0]));
            myConstructorIL.Emit(OpCodes.Ret);

            var ntype = typeBuilder.CreateType();
            _avatorCache.Add(ttype, ntype);
        }

        private static void CreatePropAvator(TypeBuilder typeBuilder, PropertyInfo propinfo,
            MethodInfo notify = null)
        {
            var fieldBuilder = typeBuilder.DefineField(
                $"_{propinfo.Name.ToLower()}",
                propinfo.PropertyType,
                FieldAttributes.Private);

            var propbuilder = typeBuilder.DefineProperty(
                propinfo.Name,
                PropertyAttributes.None,
                propinfo.PropertyType,
                null);

            #region   field
            #endregion

            #region   SetMethod
            var setMethod = propinfo.GetSetMethod();
            var types = setMethod.GetParameters().Select(p => p.ParameterType);
            var attribute = MethodAttributes.Public | MethodAttributes.Virtual;
            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod(
                setMethod.Name,
                attribute,
                null, 
                new Type[] { propinfo.PropertyType });
            setMethodBuilder.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator setIl = setMethodBuilder.GetILGenerator();

            //
            // set{
            //     field = value;
            //     notify(propname)
            // }
            //
            setIl.Emit(OpCodes.Nop);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);
            setIl.Emit(OpCodes.Nop);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldstr, propinfo.Name);
            setIl.Emit(OpCodes.Callvirt, notify);
            setIl.Emit(OpCodes.Ret);
            propbuilder.SetSetMethod(setMethodBuilder);
            #endregion

            #region   GetMethod
            var getMethod = propinfo.GetGetMethod();
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod(
                getMethod.Name,
                attribute,
                propinfo.PropertyType, 
                new Type[0]);
            ILGenerator getIl = getMethodBuilder.GetILGenerator();
            //
            // get{
            //     return field
            // }
            //
            getIl.Emit(OpCodes.Nop);
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);
            propbuilder.SetGetMethod(getMethodBuilder);
            #endregion
        }

        static ViewModelAvator()
        {
            _avatorCache = new Dictionary<Type, Type>();
            _dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName($"{nameof(ViewModelAvator)}{Guid.NewGuid().ToString("N")}"),
                AssemblyBuilderAccess.Run);
            _dynamicModule = _dynamicAssembly.DefineDynamicModule(nameof(ViewModelAvator));
        }
    }
 
}
