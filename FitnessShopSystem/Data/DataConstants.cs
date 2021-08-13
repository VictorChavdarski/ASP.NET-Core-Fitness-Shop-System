namespace FitnessShopSystem.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int PasswordMinLength = 8;
            public const int PasswordMaxLength = 16;
        }

        public class Product
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;

            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;

            public const int PriceMinValue = 5;
            public const int PriceMaxValue = 500;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;

            public const int FlavourMinLength = 2;
            public const int FlavourMaxLength = 20;
        }

        public class Manufacturer
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;

            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 12;
        }

        public class Category
        {
            public const int NameMaxLength = 20;
        }
    }
}
