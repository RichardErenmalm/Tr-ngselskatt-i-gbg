namespace Trängselskatt_i_gbg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SkattKalkylator skattkalk = new SkattKalkylator();

            Console.WriteLine("Exempel från steg ett (ta emot en tid och visa priset) som ska visa 13 kr: ");
            skattkalk.RäknaTotalBelopp("2023-05-31 17:45");

            Console.WriteLine("Exempel från steg två (ta emot flera datum och tider i samma sträng) som ska visa 42 kr: ");
            skattkalk.RäknaTotalBelopp("2023-05-31 08:00, 2023-05-31 12:00, 2023-05-31 13:45, 2023-05-31 17:45");

            Console.WriteLine("Exempel från steg tre (summa kan inte gå över 60) som ska visa 60 kr: ");
            skattkalk.RäknaTotalBelopp("2023-05-31 07:00, 2023-05-31 15:30, 2023-05-31 08:25, 2023-05-31 17:30");

            Console.WriteLine("Exempel från steg fyra (ta bort skatter från lördag, söndag och juli) som ska visa 0 kr: ");
            skattkalk.RäknaTotalBelopp("2023-06-03, 2023-06-04, 2023-07-01");

            Console.WriteLine("Exempel från steg fem (ta bara betalt en gång inom en timme, alltid högsta priset) som ska visa 18 kr: ");
            skattkalk.RäknaTotalBelopp("2023-05-31 06:20, 2023-05-31 06:45, 2023-05-31 07:10");
           
        }
    }
}
