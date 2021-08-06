﻿namespace FitnessShopSystem.Data
{
    public class DataConstants
    {
        public class Product
        {
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;

            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;

            public const int PriceMinValue = 5;
            public const int PriceMaxValue = 1000;
        }

        public class Manufacturer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
            public const int PhoneNumberMaxLength = 12;
         
        }

        public class Category
        {
            public const int NameMaxLength = 20;
        }
    }
}
