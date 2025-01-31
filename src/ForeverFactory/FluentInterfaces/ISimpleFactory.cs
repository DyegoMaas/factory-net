﻿using System;
using ForeverFactory.Behaviors;

namespace ForeverFactory.FluentInterfaces
{
    public interface ISimpleFactory<T> : ICustomizeOneBuildOne<T>
        where T : class
    {
        /// <summary>
        ///     Configures this factory to instantiate the object of type "T" using this constructor.
        /// </summary>
        /// <param name="customConstructor">Constructor used to build "T" objects</param>
        ISimpleFactory<T> UsingConstructor(Func<T> customConstructor);

        /// <summary>
        ///     Defines the behavior used to fill the properties of a class.
        ///     DoNotFillBehavior is used by default, but you can selected other behaviors tool. 
        /// </summary>
        /// <see cref="DoNotFillBehavior"/>
        /// <see cref="FillWithEmptyValuesBehavior"/>
        /// <param name="behavior">The type of the behavior to be used</param>
        ISimpleFactory<T> WithBehavior(Behavior behavior);
        
        /// <summary>
        ///     Explicitly state that you are building one instance with some configurations.
        ///     This configuration allows for chaining more objects by chaining with <code>.Plus()</code> and <code>.PlusOne()</code>.
        /// </summary>
        ICustomizeOneBuildOneWithNavigation<T> One();
        
        /// <summary>
        ///     Creates a set of customizable objects
        /// </summary>
        /// <param name="count">The number of objects to be created.</param>
        ICustomizeManyBuildMany<T> Many(int count);
    }
}