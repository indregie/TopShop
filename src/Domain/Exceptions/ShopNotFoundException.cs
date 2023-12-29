namespace Domain.Exceptions;

public class ShopNotFoundException : Exception
{
    public ShopNotFoundException() : base("Shop not found.") { }
}
