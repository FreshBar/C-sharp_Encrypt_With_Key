using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutKey
{
    class Program
    {
        static int pos(string key, char symb)
        {
            int result = 0;
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] < symb)
                {
                    result++;
                }

            }
            return result;
        }
        static bool is_unique_symbs(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string new_str = str;
                new_str = new_str.Remove(i);
                if (new_str.IndexOf(str[i]) != -1)
                    return false;
            }

            return true;
        }


        static void Encrypt()
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            text = text.Replace(" ", "");
            if (text == "")
            {
                Console.WriteLine("Ничего не введено!");
                return;
            }
            while (true)
            {
                Console.Write("Введите ключ: ");
                string key = Console.ReadLine();
                is_unique_symbs(key);

                if (is_unique_symbs(key) == false)
                {
                    Console.WriteLine("Запрещено использование одинаковых символов в ключе!");
                    continue;
                }


                int columns = key.Length;
                if (columns <= 0)
                {
                    Console.WriteLine("Ключ не может быть  пустым!");
                    continue;
                }
            
                // Console.WriteLine(text);
                string[] Array = new string[columns];
                int k = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    Array[k++] += text[i];
                    if (k == columns)
                    {
                        k = 0;
                    }
                }

                string[] Arr = new string[columns];
                for (int i = 0; i < columns; i++)
                {
                    Arr[pos(key, key[i])] = Array[i];
                }

                string encrypted = "";
                for (int i = 0; i < columns; i++)
                {
                    Console.WriteLine(Array[i]);
                    encrypted += Arr[i];
                }

                k = 0;
                for (int i = 0; i < encrypted.Length; i++)
                {
                    Console.Write(Char.ToUpper(encrypted[i]));
                    k++;
                    if (k == 5 && i != encrypted.Length - 1)
                    {
                        Console.Write(" ");
                        k = 0;

                    }

                }
                break;
            }
            
        }
        static void Decrypt()
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            if (text == "")
            {
                Console.WriteLine("Ничего не введено!");
                return;
            }
            while (true)
            {
                Console.Write("Введите ключ: ");
                string key = Console.ReadLine();
                text = text.Replace(" ", "");
                is_unique_symbs(key);
                if (is_unique_symbs(key) == false)
                {
                    Console.WriteLine("Запрещено использование одинаковых символов в ключе!");
                    continue;
                }


                int columns = key.Length;
                if (columns <= 0)
                {
                    Console.WriteLine("Ключ не может быть  пустым!");
                    continue;
                }
                int symbsInCol = text.Length / columns;
                int reminder = text.Length - symbsInCol * columns;

                int[] arrWithColumnsCount = new int[columns];
                for (int i = 0; i < columns; i++)
                {
                    int cols = pos(key, key[i]);
                    arrWithColumnsCount[cols] = symbsInCol;
                    if (reminder > 0)
                    {
                        reminder--;
                        arrWithColumnsCount[cols]++;
                    }


                }

                string[] strs = new string[columns];
                string[] strsNew = new string[100];
                int counter = 0;
                for (int i = 0; i < columns; i++)
                {
                    int symbsInStr = arrWithColumnsCount[i];
                    for (int j = counter; j < counter + symbsInStr; j++)
                    {
                        strs[i] += text[j];
                    }
                    counter += symbsInStr;
                }
                for (int i = 0; i < columns; i++)
                {
                    int col = pos(key, key[i]);
                    strsNew[i] = strs[col];
                    Console.WriteLine(strsNew[i]);//
                }
                int position = text.Length - symbsInCol * columns;
                string decrypt = "";
                while (strsNew[0] != "")
                {
                    position--;
                    position += columns;
                    position %= columns;
                    decrypt += strsNew[position][strsNew[position].Length - 1];
                    strsNew[position] = strsNew[position].Remove(strsNew[position].Length - 1);
                    //Console.WriteLine(decrypt[decrypt.Length - 1]);

                }
                //Console.WriteLine(decrypt);
                decrypt = new string(decrypt.ToCharArray().Reverse().ToArray());
                Console.WriteLine(decrypt);
                break;
            }
        }
        static void Main(string[] args)
        {
            string check = "y";
            do
            {
                Console.Write("Зашифровать-1\nРасшифровать-2\n");
                int menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {

                    Encrypt();

                }
                else if (menu == 2)
                {
                    Decrypt();
                }
                else
                {
                    Console.Write("Выбран несуществующий пункт!");
                }
                //Console.Read();
                Console.WriteLine("\nХотите продолжить? (y/n)");
                check = Convert.ToString(Console.ReadLine());
            }
            while (check == "y") ;
         }
     }
    
}
