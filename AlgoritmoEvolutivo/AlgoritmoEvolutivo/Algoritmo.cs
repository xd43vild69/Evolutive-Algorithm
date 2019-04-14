using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoEvolutivo
{
    public class AlgoritmoWordComparation
    {

        public string WordBase { get; set; }
        public string WordTest { get; set; }
        public Dictionary<int, WordPoints> CopiesDict { get; set; }
        const int SIZE_COPIES = 50;
        const int SIZE_MAX_POINTS = 25;
        const int PERCENT = 3;
        public bool IsMaxScoreReady { get; set; }
        public int Generacion { get; set; }

        /// <summary>
        /// Constructor recibe palabras a comparar.
        /// </summary>
        /// <param name="wordBase">Palabra base a comparar.</param>
        /// <param name="wordTest">Palabra comparada.</param>
        public AlgoritmoWordComparation(string wordBase, string wordTest)
        {
            IsMaxScoreReady = false;

            WordBase = wordBase;
            WordTest = wordTest;
            Generacion = 0;

            while (!IsMaxScoreReady)
            {
                CreateCopies(SIZE_COPIES);
                GetPointsWords();

                if (IsMaxScoreReady)
                {
                    break;
                }

                WordBase = GetNewWordTest();
                Generacion++;
            }

            Console.WriteLine("-.-");
            Console.Read();

        }

        public void CreateCopies(int sizeCopies)
        {
            CopiesDict = new Dictionary<int, WordPoints>();

            for (int i = 0; i < sizeCopies - 1; i++)
            {
                string wordTmp = CreateRandomWord(WordTest);
                WordPoints wordPoints = new WordPoints() { Points = 0, Word = wordTmp };
                CopiesDict.Add(i, wordPoints);
            }
        }

        public string CreateRandomWord(string word)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
            char[] wordTmp = word.ToCharArray();

            var random = new Random();
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (IsValidToChangeCharacter())
                {
                    wordTmp[i] = chars[random.Next(chars.Length)];
                }
            }
            return new string(wordTmp);
        }

        public bool IsValidToChangeCharacter()
        {
            bool isValid = false;

            var random = new Random();
            int num = random.Next(0, 100);
            if (num < PERCENT)
            {
                isValid = true;
            }

            return isValid;
        }

        public void GetPointsWords()
        {
            foreach (var wordTmp in CopiesDict)
            {
                WordPoints wordPointsTmp = wordTmp.Value;
                int points = CompareWords(WordBase, wordPointsTmp.Word);

                wordPointsTmp.Points = points;

                if (points == SIZE_MAX_POINTS)
                {
                    Console.WriteLine("Eureka!!! en posición " + wordTmp.Key.ToString() + " concidio la Palabra " + wordTmp.Value.Word + " con todos los puntos.");
                    IsMaxScoreReady = true;
                    break;
                }
            }
        }

        public string GetNewWordTest()
        {
            var maxPointsWord = CopiesDict.Max(x => x.Value.Points);
            var newWord = CopiesDict.Where(x => x.Value.Points == maxPointsWord).First().Value.Word;

            Console.WriteLine("Generación:" + Generacion + "- Mutación : " + newWord + " Puntaje : " + maxPointsWord.ToString() + " " );

            return newWord;
        }

        public int CompareWords(string wordBase, string wordCompare)
        {
            int points = 0;
            for (int i = 0; i < wordBase.Length - 1; i++)
            {
                if (wordBase[i] == wordCompare[i])
                {
                    points++;
                }
            }
            return points;
        }
    }

    public class WordPoints
    {
        public string Word { get; set; }
        public int Points { get; set; }
    }

}
