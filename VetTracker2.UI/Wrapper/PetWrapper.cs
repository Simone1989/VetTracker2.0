﻿using System;
using System.Collections.Generic;
using VetTracker2.Model;

namespace VetTracker2.UI.Wrapper
{
    public class PetWrapper : ModelWrapper<Pet>
    {
        public PetWrapper(Pet model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Type
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Ilness
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Owner
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robots are bad";
                    }
                    break;
            }
        }
    }
}
