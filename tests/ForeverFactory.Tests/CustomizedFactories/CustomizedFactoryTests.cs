﻿using FluentAssertions;
using ForeverFactory.Tests.CustomizedFactories.ExampleFactories;
using Xunit;

namespace ForeverFactory.Tests.CustomizedFactories
{
    public class CustomizedFactoryTests
    {
        [Fact]
        public void Custom_factories_should_produce_instances_using_custom_constructor()
        {
            var customizedFactory = new ProductFactory();

            var product = customizedFactory.Build();

            product.Name.Should().Be("Nimbus 2000");
            product.Category.Should().Be("Brooms");
            product.Description.Should().Be("The best flight await you!");
        }
    }
}