﻿using System;

namespace ForeverFactory.FluentInterfaces
{
    /// <summary>
    ///     This interface allows building one customized object of type "T".
    /// </summary>
    /// <typeparam name="T">The type of object that will be built.</typeparam>
    public interface ICustomizeOneBuildOne<out T> : IBuildOne<T>
    {
        /// <summary>
        ///     Defines the value of a property.
        /// </summary>
        /// <param name="setMember">Sets the value of a Property.
        ///     <example>x => x.Name = "Karen"</example>
        ///     >
        /// </param>
        ICustomizeOneBuildOne<T> With<TValue>(Func<T, TValue> setMember);
        
        /// <summary>
        ///     Executes the callback passing the instance with its current state.
        /// </summary>
        /// <param name="callback">
        ///     <example>x => Console.WriteLine(x.Name)</example>
        /// </param>
        ICustomizeOneBuildOne<T> Do(Action<T> callback);
    }
}