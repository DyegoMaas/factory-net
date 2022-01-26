﻿using System;

namespace ForeverFactory.Generators.Transforms.Factories
{
    internal class FillWithSequentialValuesTransformFactory : BaseRecursiveTransformFactory
    {
        public FillWithSequentialValuesTransformFactory(RecursiveTransformFactoryOptions options = null) 
            : base(options)
        {
        }

        protected override Func<object> GetBuildFunctionForSpecializedProperty(TargetInfo targetInfo, int index)
        {
            var sequentialNumber = index + 1;
            
            if (targetInfo.TargetType == typeof(string)) 
                return () => targetInfo.Name + sequentialNumber;
            
            if (targetInfo.TargetType == typeof(byte))
                return () =>
                {
                    if (sequentialNumber > byte.MaxValue)
                        return (byte)(sequentialNumber % byte.MaxValue);
                    return (byte)sequentialNumber;
                };
            
            if (targetInfo.TargetType == typeof(short))
                return () =>
                {
                    if (sequentialNumber > short.MaxValue)
                        return (short)(sequentialNumber % short.MaxValue);
                    return (short)sequentialNumber;
                };
            
            if (targetInfo.TargetType == typeof(ushort))
                return () =>
                {
                    if (sequentialNumber > ushort.MaxValue)
                        return (ushort)(sequentialNumber % ushort.MaxValue);
                    return (ushort)sequentialNumber;
                };
            
            if (targetInfo.TargetType == typeof(int))
                return () => sequentialNumber;
            
            if (targetInfo.TargetType == typeof(uint))
                return () => (uint)sequentialNumber;
            
            if (targetInfo.TargetType == typeof(long))
                return () => sequentialNumber;
            
            if (targetInfo.TargetType == typeof(ulong))
                return () => (ulong)sequentialNumber;
            
            if (targetInfo.TargetType == typeof(float))
                return () => sequentialNumber;
            
            if (targetInfo.TargetType == typeof(double))
                return () => sequentialNumber;
            
            if (targetInfo.TargetType == typeof(decimal))
                return () => Convert.ToDecimal(sequentialNumber);

            return null;
        }
    }
}