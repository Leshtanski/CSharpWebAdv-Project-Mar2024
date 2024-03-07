namespace TennisShopSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Brand
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Product
        {
            public const int TitleMinLength = 2; //10
            public const int TitleMaxLength = 50;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "2000";
        }

        public static class Seller 
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }
    }
}
