﻿using System;
using ForeverFactory.Behaviors;

namespace ForeverFactory
{
    public interface ICustomizeFactoryOptions<T>
        where T : class
    {
        ICustomizeFactoryOptions<T> UseConstructor(Func<T> customConstructor);
        ICustomizeFactoryOptions<T> Set<TValue>(Func<T, TValue> setMember);
        ICustomizeFactoryOptions<T> SetDefaultBehavior(Behavior behavior);
    }
}