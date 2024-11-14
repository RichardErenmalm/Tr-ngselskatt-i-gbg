using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trängselskatt_i_gbg
{
    public static class TiderOchPriser
    {
        static Dictionary<TimeSpan, int> tiderOchPriser = new Dictionary<TimeSpan, int>
        {
              { new TimeSpan(6,0,0), 8},
              { new TimeSpan(6,29,59), 8},

              { new TimeSpan(6,30,0), 13 },
              { new TimeSpan (6,59,59), 13 },

              { new TimeSpan(7,0,0), 18 },
              { new TimeSpan (7,59,59), 18 },

              { new TimeSpan(8,0,0), 13},
              { new TimeSpan (8,29,59), 13},

              { new  TimeSpan(8,30,0), 8 },
              { new TimeSpan (14,59,59), 8},

              { new TimeSpan(15,0,0), 13 },
              { new TimeSpan (15,29,59), 13},

              { new TimeSpan(15,30,0), 18 },
              { new TimeSpan (16,59,59), 18},

              { new TimeSpan(17,0,0), 13 },
              { new TimeSpan (17,59,59), 13},

              { new TimeSpan(18,0,0), 8 },
              { new TimeSpan (18,29,59), 8},

              { new TimeSpan(18,30,0), 0 },
              { new TimeSpan (5,59,59), 0 },
        };
        public static List<TimeSpan> tiderLista = tiderOchPriser.Keys.ToList();
        public static List<int> priserLista = tiderOchPriser.Values.ToList();
    }
}
