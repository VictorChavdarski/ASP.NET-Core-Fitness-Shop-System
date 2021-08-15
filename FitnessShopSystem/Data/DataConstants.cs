namespace FitnessShopSystem.Data
{
    public class DataConstants
    {
        public class Delivery
        {
            public const int CustomerFirstNameMinLength = 3;
            public const int CustomerFirstNameMaxLength = 30;

            public const int CustomerLastNameMinLength = 3;
            public const int CustomerLastNameMaxLength = 30;

            public const int CompanyNameMinLength = 3;
            public const int CompanyNameMaxLength = 30;

            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 50;

            public const int PostalCodeMinValue = 4;
            public const int PostalCodeMaxValue = 5;

            public const int CityNameMinLength = 3;
            public const int CityNameMaxLength = 30;

            public const int CountryNameMinLength = 3;
            public const int CountryNameMaxlength = 30;

            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 12;
        }
    
        public class TrainingProgram
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 1000;
        }

        public class Instructor
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 3;
            public const int LastNameMaxLength = 30;

            public const int AgeMinValue = 18;
            public const int AgeMaxValue = 60;

            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 12;
        }

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
