﻿using System;
using ForeverFactory.Behaviors;

namespace ForeverFactory.Builders
{
    public interface ICustomizableFactory<T> : IOneBuilder<T>
        where T : class
    {
        /// <summary>
        /// Configures this factory to instantiate the object of type "T" using this constructor.
        /// </summary>
        /// <param name="customConstructor">Constructor used to build "T" objects</param>
        ICustomizableFactory<T> UsingConstructor(Func<T> customConstructor);

        // ICustomizableFactory<T> WithBehavior(Behaviors chosenBehavior);

        // TODO document
        ICustomizableFactory<T> WithBehavior(Behavior behavior);

        /// <summary>
        ///     Creates a set of customizable objects
        /// </summary>
        /// <param name="count">The number of objects to be created.</param>
        IManyBuilder<T> Many(int count);
    }
}