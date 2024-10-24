namespace Catalog.Domain.Utils;

public static class PriceManager
{
    public static decimal CalculatePrice(decimal price, decimal discountRate, decimal discountAmount)
    {
        ValidateArguments(price, ref discountRate, ref discountAmount);

        return (discountAmount, discountRate) switch
        {
            (0, 0) => price,
            (_, 0) => price - discountAmount,
            (_, _) => price - (price * discountRate / 100)
        };
    }

    private static void ValidateArguments(decimal price, ref decimal discountRate, ref decimal discountAmount)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));
        if (price < discountAmount)
            throw new ArgumentException("Discount amount cannot be greater than price", nameof(discountAmount));
        if (discountRate >= 100)
            throw new ArgumentException("Discount amount cannot be greater than or equal to 100", nameof(discountAmount));

        discountAmount = Math.Max(0, discountAmount);
        discountRate = Math.Max(0, discountRate);
    }
}