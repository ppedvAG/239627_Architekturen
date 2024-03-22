using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.Devices.BinFord
{
    public class BinFordOvenControl : IOvenControl
    {
        public void StartOven(int temp)
        {
            Console.Beep(temp, 2000);
        }
    }
}
