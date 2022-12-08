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

        public static T CreateViewModelAvator<T>() 
        {
            Type ttype = typeof(T);
            if (!_avatorCache.ContainsKey(ttype))
            {
                CreateViewModelAvatorT(ttype);
            }
            return (T)Activator.CreateInstance(_avatorCache[ttype], new object[0]);
        }

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
                ttype,
                new Type[] { typeof(INotifyPropertyChanged) });
            //Type.EmptyTypes);

            #region   Inotify builder
            // 
            //  public event PropertyChangedEventHandler PropertyChanged;
            //
            var Inotifyfield = typeBuilder.DefineField(
                nameof(INotifyPropertyChanged.PropertyChanged),
                typeof(PropertyChangedEventHandler),
                FieldAttributes.Private);
            var Inotifyevent = typeBuilder.DefineEvent(
                nameof(INotifyPropertyChanged.PropertyChanged),
                EventAttributes.None,
                typeof(PropertyChangedEventHandler));
            var add = CreateAddRemoveMethod(typeBuilder, Inotifyfield, true);
            var remove = CreateAddRemoveMethod(typeBuilder, Inotifyfield, false);
            Inotifyevent.SetAddOnMethod(add);
            Inotifyevent.SetRemoveOnMethod(remove);
            #endregion

            #region   createPropAvator
            var allprops = ttype.GetProperties();
            foreach (var prop in allprops)
            {
                if (prop.GetCustomAttribute<ObservablePropAttribute>() is ObservablePropAttribute attr)
                {
                    CreatePropAvator(typeBuilder, prop, Inotifyfield);
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
            FieldBuilder notify = null)
        {

            var propbuilder = typeBuilder.DefineProperty(
                propinfo.Name,
                PropertyAttributes.None,
                propinfo.PropertyType,
                null);

            #region   field
            var fieldBuilder = typeBuilder.DefineField(
                $"_{propinfo.Name.ToLower()}",
                propinfo.PropertyType,
                FieldAttributes.Private);
            #endregion

            #region   SetMethod
            var setMethod = propinfo.GetSetMethod();
            var types = setMethod.GetParameters().Select(p => p.ParameterType);
            var attribute = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod(
                setMethod.Name,
                attribute,
                setMethod.ReturnType, 
                types.ToArray());
            ILGenerator setIl = setMethodBuilder.GetILGenerator();

            //
            // set{
            //     field = value;
            //     propchanged?.invoke(this,new arg(name))
            // }
            //
            Label a = setIl.DefineLabel();
            Label ret = setIl.DefineLabel();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldfld, notify);
            setIl.Emit(OpCodes.Dup);
            setIl.Emit(OpCodes.Brtrue_S, a);
            setIl.Emit(OpCodes.Pop);
            setIl.Emit(OpCodes.Br_S, ret);
            setIl.MarkLabel(a);

            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldstr, propinfo.Name);
            setIl.Emit(OpCodes.Newobj, typeof(PropertyChangedEventArgs).GetConstructor(new Type[] { typeof(string) }));
            setIl.Emit(OpCodes.Callvirt, typeof(PropertyChangedEventHandler).GetMethod("Invoke"));
            setIl.MarkLabel(ret);
            setIl.Emit(OpCodes.Ret);

            propbuilder.SetSetMethod(setMethodBuilder);
            #endregion

            #region   GetMethod
            var getMethod = propinfo.GetGetMethod();
            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod(
                getMethod.Name,
                attribute,
                getMethod.ReturnType,
                Type.EmptyTypes);
            ILGenerator getIl = getMethodBuilder.GetILGenerator();
            //
            // get{
            //     return field
            // }
            //
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);
            propbuilder.SetGetMethod(getMethodBuilder);
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        private static MethodBuilder CreateAddRemoveMethod(
           TypeBuilder typeBuilder, FieldBuilder eventField, bool isAdd)
        {
            MethodAttributes eventflag = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final;

            string prefix = "remove_";
            string delegateAction = "Remove";
            if (isAdd)
            {
                prefix = "add_";
                delegateAction = "Combine";
            }
            MethodBuilder addremoveMethod =
            typeBuilder.DefineMethod(prefix + "PropertyChanged", eventflag, null, new[] { typeof(PropertyChangedEventHandler) });
            MethodImplAttributes eventMethodFlags =
                MethodImplAttributes.Managed |
                MethodImplAttributes.Synchronized;
            addremoveMethod.SetImplementationFlags(eventMethodFlags);

            ILGenerator ilGen = addremoveMethod.GetILGenerator();

            // PropertyChanged += value; // PropertyChanged -= value;
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldfld, eventField);
            ilGen.Emit(OpCodes.Ldarg_1);
            ilGen.EmitCall(OpCodes.Call,
                typeof(Delegate).GetMethod(
                delegateAction,
                new[] { typeof(Delegate), typeof(Delegate) }),
                null);
            ilGen.Emit(OpCodes.Castclass, typeof(PropertyChangedEventHandler));
            ilGen.Emit(OpCodes.Stfld, eventField);
            ilGen.Emit(OpCodes.Ret);

            MethodInfo intAddRemoveMethod =
            typeof(INotifyPropertyChanged).GetMethod(
            prefix + "PropertyChanged");
            typeBuilder.DefineMethodOverride(
            addremoveMethod, intAddRemoveMethod);

            return addremoveMethod;
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
