using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoEvolutivo
{
    class Program
    {
        static void Main(string[] args)
        {
            string WORD_BASE = "MVM INGENIERIA DE SOFTWARE";
            string WORD_TEST = "MVM INxEeIfRIA DE SOFTWARE";

            AlgoritmoWordComparation wordComparation = new AlgoritmoWordComparation(WORD_BASE, WORD_TEST);

        }
    }
}
