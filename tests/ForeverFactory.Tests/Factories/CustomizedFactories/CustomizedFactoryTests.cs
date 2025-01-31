﻿using FluentAssertions;
using ForeverFactory.Tests.Factories.CustomizedFactories.ExampleFactories;
using Xunit;

namespace ForeverFactory.Tests.Factories.CustomizedFactories
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

        [Fact]
        public void It_should_apply_properties_set_in_the_customization()
        {
            var products = new AnotherProductFactory()
                .Many(2)
                .Build();

            foreach (var product in products)
            {
                product.Description.Should().Be("Orange");
            }
        }

        private class AnotherProductFactory : MagicFactory<Product>
        {
            protected override void Customize(ICustomizeFactoryOptions<Product> customization)
            {
                customization
                    .UseConstructor(() => new Product(name: "Shirt", category: "Clothes"))
                    .Set(x => x.Description = "Orange");
            }
        }
    }
}