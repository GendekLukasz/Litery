using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Litery
{
    class MergeSort
    {

        public static Int64 Sort(int[] tab)
        {
         
            int from = 0;
            int interval1  = 1;
            int interval2 = 1;
            Int64 result = 0;

            int n = 1;
   
            while (interval1  < tab.Length)
            {
                from = 0;
                n = 1;
                for (;;)
                {
                    if (interval1  * n * 2 <= tab.Length)
                    {
                        result += Merge(tab, from, interval1 , interval1 );
                        from = from + interval1  * 2;
                        n++;
                        continue;
                    }
                
                    if (interval1  * n * 2 > tab.Length && interval1  * n *2 -interval1 <tab.Length)
                    {
                        interval2 = tab.Length - (interval1  * n * 2 - interval1 );
                        result += Merge(tab, from, interval1 , interval2);
                    }
                    break;


                }
                interval1  = interval1  * 2;

            }
            return result;
        }

        public static Int64 Merge(int[] tab, int from1, int interval1 , int interval2)
        {
            
            int licznik = from1;
            int tab1Counter = 0, tab2Counter = 0;
            Int64 result = 0; 
            int tab1CounterTo = from1 + interval1 ;
            int tab2CounterTo = tab1CounterTo + interval2;

            int[] tab1 = new int[interval1 ];
            int[] tab2 = new int[interval2];
            for (int i = from1; i < tab1CounterTo; i++)
            {
                tab1[i - from1] = tab[i];
            }
            for (int i = tab1CounterTo; i < tab2CounterTo; i++)
            {
                tab2[i - tab1CounterTo] = tab[i];
            }

            while (licznik < from1 + interval1  + interval2)
            {

                if (tab1Counter == interval1  && tab2Counter < interval2)
                {

                    tab[licznik] = tab2[tab2Counter];

                    tab2Counter++;
                    licznik++;
                   
                    continue;
                }
                if (tab2Counter == interval2 && tab1Counter < interval1 )
                {
                    tab[licznik] = tab1[tab1Counter];
                    tab1Counter++;
                    licznik++;
                    result= result +tab2Counter ;
                    continue;
                }
                if (tab1Counter < interval1  && tab2Counter < interval2 && tab1[tab1Counter] < tab2[tab2Counter])
                {
                    tab[licznik] = tab1[tab1Counter];
                    tab1Counter++;
                    licznik++;
                    result = result + tab2Counter ;
                    continue;
                }
                if (tab1Counter < interval1  && tab2Counter < interval2 && tab1[tab1Counter] > tab2[tab2Counter])
                {

                    tab[licznik] = tab2[tab2Counter];
                    tab2Counter++;
                    licznik++;
                   
                    continue;
                }

                if (tab1Counter < interval1  && tab2Counter < interval2 && tab1[tab1Counter] == tab2[tab2Counter])
                {

                    tab[licznik] = tab1[tab1Counter];
                    tab1Counter++;
                    licznik++;
                    result = result + tab2Counter ;
                    tab[licznik] = tab2[tab2Counter];
                    tab2Counter++;
                    licznik++;
                    
                    continue;
                }


            }
            return result;
        }
    }
    struct LetterPosition
    {
        public int position;
        public char letter;
    }
    class OperationsOnLIT
    {
        public static string address = "lit";
        public static void SaveToOut(Int64 result)
        {
            StreamWriter sw = new StreamWriter("lit.out");
            sw.WriteLine(result);
            sw.Flush();
        }

        public static Int64 NumberOfInversion()
        {

            DateTime now2 = DateTime.Now;
            StreamReader sr1 = new StreamReader(address + ".in");

            List<int> lettersToCheck = new List<int>();

            string litLine1 = sr1.ReadLine();
            string litLine2 = sr1.ReadLine();
            string litLine3 = sr1.ReadLine();
            sr1.Close();
            string tmp1 = string.Empty;

            Int64 val;
            
            for (int i = 0; i < litLine1.Length; i++)
            {
                if (char.IsDigit(litLine1[i]))
                {
                    tmp1 += litLine1[i];
                }
                else
                {
                    break;
                }
            }
            val = int.Parse(tmp1);
            int[] numbersToSort = new int[val];



            Queue<Queue<LetterPosition>> queueOfTheSameLetters = new Queue<Queue<LetterPosition>>();

            queueOfTheSameLetters.Enqueue(new Queue<LetterPosition>());
            queueOfTheSameLetters.Peek().Enqueue(new LetterPosition { position = 0, letter = litLine3[0] });
            for (int i = 1; i < val; i++)
            {
                for (int j = 0; j < queueOfTheSameLetters.Count(); j++)
                {
                    if (queueOfTheSameLetters.Peek().Peek().letter == litLine3[i])
                    {

                        queueOfTheSameLetters.Peek().Enqueue(new LetterPosition { position = i, letter = litLine3[i] });

                        break;


                    }
                    
                    if (j == queueOfTheSameLetters.Count() - 1)
                    {
                        queueOfTheSameLetters.Enqueue(new Queue<LetterPosition>());
                     
                            while (queueOfTheSameLetters.Peek().Count != 0)
                            {
                                queueOfTheSameLetters.Enqueue(queueOfTheSameLetters.Dequeue());
                            };
                        queueOfTheSameLetters.Peek().Enqueue(new LetterPosition { position = i, letter = litLine3[i] });
                   
                        
                        break;
                    }
                    else
                    {
                        queueOfTheSameLetters.Enqueue(queueOfTheSameLetters.Dequeue());
                    }
                }
            }

            for (int i = 0; i < val; i++)
            {
                for (int j = 0; j < queueOfTheSameLetters.Count(); j++)
                {
                    if (queueOfTheSameLetters.Peek().Peek().letter == litLine2[i])
                    {

                        numbersToSort[queueOfTheSameLetters.Peek().Dequeue().position] = i;
                        if (queueOfTheSameLetters.Peek().Count == 0)
                        {
                            queueOfTheSameLetters.Dequeue();
                        }

                        break;


                    }
                    else
                    {
                        queueOfTheSameLetters.Enqueue(queueOfTheSameLetters.Dequeue());
                    }
                }
            }






         
            DateTime stop2 = DateTime.Now;
            TimeSpan time2 = stop2 - now2;
            Console.WriteLine("Tworzenie tablicy: " + time2);
            Console.WriteLine();
            Console.WriteLine();

            DateTime now1 = DateTime.Now;
            Int64 x = MergeSort.Sort(numbersToSort);
            DateTime stop1 = DateTime.Now;
            TimeSpan time1 = stop1 - now1;
            Console.WriteLine("Sortowanie: " + time1);
     
            return x ;
        }
        
    }

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();
        Int64 result = OperationsOnLIT.NumberOfInversion();
        Console.WriteLine();
        Console.WriteLine("result: "+ result);
        Console.WriteLine();
 
        OperationsOnLIT.SaveToOut(result);
      
        Console.ReadKey();
    }
}
}
