using System;
using System.Reflection;
using System.Reflection.Emit;

namespace cui.Internal.Helpers
{
    static class OperatorHelper
    {
        internal static Func<T, T, T> FindAddition<T>()
        {
            return CreateOperator<T, T>("op_Addition", IsUnsigned<T>() ? OpCodes.Add_Ovf_Un : OpCodes.Add_Ovf);
        }

        internal static Func<T, T, T> FindSubtraction<T>()
        {
            return CreateOperator<T, T>("op_Subtraction", IsUnsigned<T>() ? OpCodes.Sub_Ovf_Un : OpCodes.Sub_Ovf);
        }

        internal static Func<T, T, bool> FindLessThan<T>()
        {
            return CreateOperator<bool, T>("op_LessThan", IsUnsigned<T>() ? OpCodes.Clt_Un : OpCodes.Clt);
        }

        internal static Func<T, T, bool> FindGreaterThan<T>()
        {
            return CreateOperator<bool, T>("op_GreaterThan", IsUnsigned<T>() ? OpCodes.Cgt_Un : OpCodes.Cgt);
        }

        internal static Func<T, T, bool> FindEquality<T>()
        {
            return CreateOperator<bool, T>("op_Equality", OpCodes.Ceq);
        }

        static Func<T, T, TRet> CreateOperator<TRet, T>(string name, OpCode fallbackOpcode)
        {
            return CreateDelegate<TRet, T>(TryGetOperatorFunction<T>(name) ?? BuildMethod<TRet, T>(fallbackOpcode));
        }

        static bool IsUnsigned<T>()
        {
            return typeof(T) == typeof(byte)
                   || typeof(T) == typeof(uint)
                   || typeof(T) == typeof(ushort)
                   || typeof(T) == typeof(ulong);
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
            var method = new DynamicMethod($"{typeof(TRet).Name}.{code}", typeof(TRet), new [] { typeof(T), typeof(T) });
            var generator = method.GetILGenerator();
            
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(code);
            generator.Emit(OpCodes.Ret);
            
            return method;
        }
    }
}