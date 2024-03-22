namespace ppedv.CubesPizza.Model.Contracts
{
    public interface IFoodService
    {
        IEnumerable<Pizza> GetSpeisekarte(bool nurVegetarisch = false);
    }
}