﻿using System;
using ForeverFactory.Behaviors;
using ForeverFactory.Customizations.Global;
using ForeverFactory.Generators.Options;

namespace ForeverFactory.Customizations
{
    internal class OptionsCollector<T> : IOptionsCollector<T>
        where T : class
    {
        private readonly Action<ICustomizeFactoryOptions<T>> _customize;
        private readonly ObjectFactoryOptions<T> _options;
        private readonly CustomizeFactoryOptions<T> _customizationOptions;

        public OptionsCollector(Action<ICustomizeFactoryOptions<T>> customize)
        {
            _customize = customize;
            
            _options = new ObjectFactoryOptions<T>();
            _customizationOptions = new CustomizeFactoryOptions<T>(_options);
            LoadStaticCustomizationInto(_customizationOptions);
        }

        public IObjectFactoryOptions<T> Collect()
        {
            _customize.Invoke(_customizationOptions);

            return _options;
        }

        private void LoadStaticCustomizationInto(CustomizeFactoryOptions<T> customizationOptions)
        {
            if (ForeverFactoryGlobalSettings.GlobalBehavior != null)
            {
                _customizationOptions.SetDefaultBehavior(ForeverFactoryGlobalSettings.GlobalBehavior);
            }
        }

        internal void UpdateConstructor(Func<T> customConstructor)
        {
            _options.CustomConstructor = customConstructor;
        }
        
        internal void UpdateBehavior(Behavior behavior)
        {
            _options.SelectedBehavior = behavior;
        }
    }
}