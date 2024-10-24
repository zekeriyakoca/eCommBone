namespace Catalog.Domain.Models;

public class SquareBasePricePolicy : CustomPricePolicy
{
    public SquareBasePricePolicy()
    {
        base.Spec1 = "0";
        base.Spec2 = "0";
        base.BasePrice = 0;
    }
    
    public int Width {
        get => int.TryParse(base.Spec1, out int width) ? width : 0;

        set
        {
            base.Spec1 = value.ToString();
        }
    }
    public int Height {
        get => int.TryParse(base.Spec2, out int height) ? height : 0;
        set
        {
            base.Spec2 = value.ToString();
        }
    }
    
    public override decimal Price => base.BasePrice * Width * Height;
}