using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Trängselskatt_i_gbg
{
    public class SkattKalkylator
    {
        //Ta emot ett datum som sedan säger vilket priset på skatten är
        public void RäknaTotalBelopp(string tiderSträng)
        {
          
            //förvandla sträng till en datetime lista och filtrera bort lör,sön och juli (steg 4). skapa sedan timespan lista med bara tiderna på dygnet
            List <DateTime> tiderDate = tiderSträng.Split(',').Select(t => DateTime.Parse(t)).Where(l => l.DayOfWeek != DayOfWeek.Saturday && l.DayOfWeek != DayOfWeek.Sunday && l.Month != 7).ToList();
            List<TimeSpan> tider = tiderDate.Select(t => t.TimeOfDay).ToList();
            //tider sorteras efter tid
            tider = tider.OrderBy(t => t).ToList();

          
            if (tider.Count > 1)
            {
                List<TimeSpan> inomEnTimme = new List<TimeSpan>();
                TimeSpan tidigareTid = new TimeSpan(0,0,0);
                bool fortfarandeInomTimme = false;

                //hitta om någon, och då vilka tider som är inom en timme och ta bort alla tiderna inom en timme förutom det med högst pris
                //(steg 5)
                foreach (var tid in tider)
                {
                    if(tider.IndexOf(tid) != 0)
                    {
                        TimeSpan skillnad = tid - tidigareTid;

                        if (Math.Abs(skillnad.TotalMinutes) < 60)
                        {
                            fortfarandeInomTimme = true;
                           
                            if (inomEnTimme.TrueForAll(t => t != tidigareTid))
                            {
                                inomEnTimme.Add(tidigareTid);
                            }
                            inomEnTimme.Add(tid);
                            continue;
                        }         
                        else if (fortfarandeInomTimme)
                        {
                            tider = TaBortAvgifterInomTimmen(inomEnTimme, tider);
                            inomEnTimme.Clear();
                            fortfarandeInomTimme = false;
                        }

                        tidigareTid = tid;
                    }
                    else
                    {
                        tidigareTid = tid;
                    } 
                }

                if (fortfarandeInomTimme)
                {
                    tider = TaBortAvgifterInomTimmen(inomEnTimme, tider);
                    inomEnTimme.Clear();
                    fortfarandeInomTimme = false;
                }
            }

            //låt inte summa gå över 60 (steg 3)
            if (HittaPriser(tider).Sum() > 60)
            {
                Console.WriteLine($"Total avgift: 60 kr");
            }
            else 
            {
                Console.WriteLine($"Total avgift: {HittaPriser(tider).Sum()} kr");
            }
            Console.WriteLine();
        }

        //tar bort alla tider inom inomm en timme i listan förutom högsta priset
        public static List<TimeSpan> TaBortAvgifterInomTimmen(List<TimeSpan> inomEnTimme, List<TimeSpan> tider)
        {
            List<int> priserInomEnTimme = HittaPriser(inomEnTimme);

            inomEnTimme.RemoveAt(priserInomEnTimme.IndexOf(priserInomEnTimme.Max()));

            foreach (var tidInomEnTimme in inomEnTimme)
            {
                tider.Remove(tidInomEnTimme);
            }
            return tider;
        }
        
        //tar reda på vad kostnaden
        public static List<int> HittaPriser(List<TimeSpan> tider)
        {
            List<int> priser = new List<int>();
            foreach (TimeSpan tid in tider)
            {
                for (int i = 0; i < TiderOchPriser.tiderLista.Count - 1; i += 2)
                {
                    if (tid >= TiderOchPriser.tiderLista[i] && tid <= TiderOchPriser.tiderLista[i + 1])
                    {
                        priser.Add(TiderOchPriser.priserLista[i]);
                        break;
                    }
                }
            }
            return priser;
        }
    }
}
