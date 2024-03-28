using System.ComponentModel.DataAnnotations;

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

            public const string QuantityMinValue = "1";
            public const string QuantityMaxValue = "1000";
        }

        public static class Seller 
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class OrderDetails
        {
            public const int OrderDetailsFirstNameMinLength = 2;
            public const int OrderDetailsFirstNameMaxLength = 250;

            public const int OrderDetailsLastNameMinLength = 2;
            public const int OrderDetailsLastNameMaxLength = 250;

            public const int OrderDetailsAddressMinLength = 10;
            public const int OrderDetailsAddressMaxLength = 300;

            public const int OrderDetailsPhoneNumberMinLength = 7;
            public const int OrderDetailsPhoneNumberMaxLength = 15;

            public const int OrderDetailsEmailAddressMinLength = 10;
            public const int OrderDetailsEmailAddressMaxLength = 200;

            public const int OrderDetailsCommentMinLength = 1;
            public const int OrderDetailsCommentMaxLength = 2000;

            public const string OrderDetailsTotalPriceMinValue = "0.01";
            public const string OrderDetailsTotalPriceMaxValue = "50000";
        }
    }
}
