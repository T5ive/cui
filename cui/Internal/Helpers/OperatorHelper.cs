using System;
using System.Reflection;
using System.Reflection.Emit;

namespace cui.Internal.Helpers
{
    static class OperatorHelper
    {
        internal static Func<T, T, T> FindAddition<T>()
        {
            return CreateOperator<T, T>("op_Addition", OpCodes.Add);
        }

        internal static Func<T, T, T> FindSubtraction<T>()
        {
            return CreateOperator<T, T>("op_Subtraction", OpCodes.Sub);
        }

        internal static Func<T, T, bool> FindLessThan<T>()
        {
            return CreateOperator<bool, T>("op_LessThan", OpCodes.Clt);
        }

        internal static Func<T, T, bool> FindGreaterThan<T>()
        {
            return CreateOperator<bool, T>("op_GreaterThan", OpCodes.Cgt);
        }

        static Func<T, T, TRet> CreateOperator<TRet, T>(string name, OpCode code)
        {
            return CreateDelegate<TRet, T>(TryGetOperatorFunction<T>(name) ?? BuildMethod<TRet, T>(code));
        }

        static Func<T, T, TRet> CreateDelegate<TRet, T>(MethodInfo method)
        {
            return (Func<T, T, TRet>)method.CreateDelegate(typeof(Func<T, T, TRet>));
        }

        static MethodInfo TryGetOperatorFunction<T>(string methodName)
        {
            return typeof(T).GetMethod(methodName, (BindingFlags)(-1), null, CallingConventions.Any, new [] { typeof(T), typeof(T) }, null);
        }

        static DynamicMethod BuildMethod<TRet, T>(OpCode code)
        {
            var method = new DynamicMethod($"{typeof(TRet).FullName}.{code}", typeof(TRet), new [] { typeof(T), typeof(T) });
            var generator = method.GetILGenerator();
            
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(code);
            generator.Emit(OpCodes.Ret);
            
            return method;
        }
    }
}