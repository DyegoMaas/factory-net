﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Extensions;
using ForeverFactory.Generators.Transforms.Factories;
using Xunit;

namespace ForeverFactory.Tests.Generators.Transforms.Factories
{
    public class FillWithSequentialValuesTransformFactoryTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void It_should_fill_properties_with_sequential_values(int index, int sequentialNumberExpected)
        {
            var transform = new FillWithSequentialValuesTransformFactory()
                .GetTransform<ClassWithManyDifferentTypesOfProperties>();

            var instance = new ClassWithManyDifferentTypesOfProperties();
            transform.ApplyTo(instance, index);

            instance.StringProperty.Should().Be($"StringProperty{sequentialNumberExpected}");
            instance.ByteProperty.Should().Be((byte) sequentialNumberExpected, "byte properties should be filled");
            instance.ShortProperty.Should().Be((short) sequentialNumberExpected);
            instance.UShortProperty.Should().Be((ushort) sequentialNumberExpected);
            instance.IntProperty.Should().Be(sequentialNumberExpected);
            instance.UIntProperty.Should().Be((uint) sequentialNumberExpected);
            instance.LongProperty.Should().Be(sequentialNumberExpected);
            instance.ULongProperty.Should().Be((ulong) sequentialNumberExpected);
            instance.FloatProperty.Should().Be(sequentialNumberExpected);
            instance.DoubleProperty.Should().Be(sequentialNumberExpected);
            instance.DecimalProperty.Should().Be(sequentialNumberExpected);
        }

        public class SequentialDateTimeTests
        {
            private readonly FillWithSequentialValuesTransformFactory _factory;

            public SequentialDateTimeTests()
            {
                _factory = new FillWithSequentialValuesTransformFactory();
            }
            
            [Theory]
            [MemberData(nameof(SequentialDateTimeValues))]
            public void It_should_fill_datetime_properties_with_sequential_values(int index,
                DateTime sequentialDateTimeExpected)
            {
                var transform = _factory.GetTransform<ClassWithManyDifferentTypesOfProperties>();

                var instance = new ClassWithManyDifferentTypesOfProperties();
                transform.ApplyTo(instance, index);

                instance.DateTimeProperty.Should().Be(sequentialDateTimeExpected);
            }

            public static IEnumerable<object[]> SequentialDateTimeValues =>
                new List<object[]>
                {
                    new object[] {0, 1.January(1753)},
                    new object[] {1, 2.January(1753)},
                    new object[] {2, 3.January(1753)},
                };
        }

        public class ClassWithManyDifferentTypesOfProperties
        {
            public string StringProperty { get; set; }
            public byte ByteProperty { get; set; }
            public short ShortProperty { get; set; }
            public ushort UShortProperty { get; set; }
            public int IntProperty { get; set; }
            public uint UIntProperty { get; set; }
            public long LongProperty { get; set; }
            public ulong ULongProperty { get; set; }
            public float FloatProperty { get; set; }
            public double DoubleProperty { get; set; }
            public decimal DecimalProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
        }

        public class NumberOverflowTests
        {
            private readonly FillWithSequentialValuesTransformFactory _factory;

            public NumberOverflowTests()
            {
                _factory = new FillWithSequentialValuesTransformFactory();
            }
            
            [Fact]
            public void Bytes_should_reset_to_1_when_overflowing()
            {
                var transform = _factory.GetTransform<ClassWithManyDifferentTypesOfProperties>();
                var instance = new ClassWithManyDifferentTypesOfProperties();

                transform.ApplyTo(instance, index: byte.MaxValue - 1);
                instance.ByteProperty.Should().Be(byte.MaxValue);

                transform.ApplyTo(instance, index: byte.MaxValue);
                instance.ByteProperty.Should().Be(1);

                transform.ApplyTo(instance, index: byte.MaxValue + 1);
                instance.ByteProperty.Should().Be(2);

                transform.ApplyTo(instance, index: byte.MaxValue * 2);
                instance.ByteProperty.Should().Be(1);
            }

            [Fact]
            public void Short_should_reset_to_1_when_overflowing()
            {
                var transform = _factory.GetTransform<ClassWithManyDifferentTypesOfProperties>();
                var instance = new ClassWithManyDifferentTypesOfProperties();

                transform.ApplyTo(instance, index: short.MaxValue - 1);
                instance.ShortProperty.Should().Be(short.MaxValue);

                transform.ApplyTo(instance, index: short.MaxValue);
                instance.ShortProperty.Should().Be(1);

                transform.ApplyTo(instance, index: short.MaxValue + 1);
                instance.ShortProperty.Should().Be(2);

                transform.ApplyTo(instance, index: short.MaxValue * 2);
                instance.ShortProperty.Should().Be(1);
            }

            [Fact]
            public void UShort_should_reset_to_1_when_overflowing()
            {
                var transform = _factory.GetTransform<ClassWithManyDifferentTypesOfProperties>();
                var instance = new ClassWithManyDifferentTypesOfProperties();

                transform.ApplyTo(instance, index: ushort.MaxValue - 1);
                instance.UShortProperty.Should().Be(ushort.MaxValue);

                transform.ApplyTo(instance, index: ushort.MaxValue);
                instance.UShortProperty.Should().Be(1);

                transform.ApplyTo(instance, index: ushort.MaxValue + 1);
                instance.UShortProperty.Should().Be(2);

                transform.ApplyTo(instance, index: ushort.MaxValue * 2);
                instance.UShortProperty.Should().Be(1);
            }
        }

        public class RecursionTests
        {
            [Theory]
            [InlineData(0, 1)]
            [InlineData(1, 2)]
            [InlineData(2, 3)]
            public void It_should_build_a_function_that_recursively_sets_all_properties_to_the_name_of_the_property(
                int index, int sequentialNumberExpected)
            {
                var transform = new FillWithSequentialValuesTransformFactory().GetTransform<ClassA>();

                var instanceOfA = new ClassA();
                transform.ApplyTo(instanceOfA, index);

                instanceOfA.PropertyX.Should().Be($"PropertyX{sequentialNumberExpected}");
                instanceOfA.B.Should().NotBeNull();

                var instanceOfB = instanceOfA.B;
                instanceOfB.PropertyY.Should().Be($"PropertyY{sequentialNumberExpected}");
                instanceOfB.C.Should().NotBeNull();

                var instanceOfC = instanceOfB.C;
                instanceOfC.PropertyZ.Should().Be($"PropertyZ{sequentialNumberExpected}");
            }
            
            [Fact]
            public void Should_disable_recursive_fill()
            {
                var factoryWithRecursionDisabled = new FillWithSequentialValuesTransformFactory(
                    new RecursiveTransformFactoryOptions {
                        EnableRecursiveInstantiation = false
                    }
                );

                var transform = factoryWithRecursionDisabled.GetTransform<ClassA>();
                var instance = new ClassA();
                transform.ApplyTo(instance);

                instance.PropertyX.Should().Be("PropertyX1");
                instance.B.Should().Be(null, "should not recursively fill when it is disable via options");
            }
        }

        private class ClassA
        {
            public string PropertyX { get; set; }
            public ClassB B { get; set; }
        }

        private class ClassB
        {
            public string PropertyY { get; set; }
            public ClassC C { get; set; }
        }
        
        private class ClassC
        {
            public string PropertyZ { get; set; }
        }
    }
}