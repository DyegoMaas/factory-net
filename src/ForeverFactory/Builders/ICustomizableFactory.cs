﻿using System;

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

        /// <summary>
        /// Creates a set of customizable objects
        /// </summary>
        /// <param name="count">The number of objects to be created.</param>
        IManyBuilder<T> Many(int count);

        /// <summary>
        /// Creates a new builder of "T". It will build a new object, in addition to the previous configurations. 
        /// </summary>
        /// <returns>A builder of "T"</returns>
        ILinkedOneBuilder<T> PlusOne();

        /// <summary>
        /// Creates a new set of customizable objects, following the previous sets created used the "Many" or "Plus" methods.
        /// </summary>
        /// <param name="count">The number of objects to be created.</param>
        IManyBuilder<T> Plus(int count);
    }
}