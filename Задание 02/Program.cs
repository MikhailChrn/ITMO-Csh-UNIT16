using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;
using System.IO;

namespace Задание_02

//2.Необходимо разработать программу для получения информации о товаре из json-файла.
//Десериализовать файл «Products.json» из задачи 1. Определить название самого дорогого товара.
{
    class Product //Класс для работы с товарами
    {
        [JsonPropertyName("productCode")]
        public int ProductCode { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productPrice")]
        public double ProductPrice { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = @"C:\Users\Михаил\Desktop\ИТМО\Блок 1\Занятие 16\Products.json";
            StreamReader sr = new StreamReader(sourceFile); //Объявляем метод StreamReader
            int stringCount = File.ReadAllLines(sourceFile).Length;//Считаем кол-во строк в файле;

            string jsonString;
            Product[] product = new Product[stringCount]; //Вынужден объявлять массив на конкретное кол-во экземпляров
            int i = 0;

            while ((jsonString = sr.ReadLine()) != null)
            {
                product[i] = new Product(); //Эта конструкция не работает, если при объявлении массива отсутсвует stringCount
                product[i] = JsonSerializer.Deserialize<Product>(jsonString);
                i++;
            }

            for (i = 0; i < stringCount; i++) //Данная конструкция выводит элементы всех экземпляров
            {
                Console.Write("{0,-5}  ", product[i].ProductCode);
                Console.Write("{0,-30}  ", product[i].ProductName);
                Console.WriteLine("{0:F} руб.", product[i].ProductPrice);
            }
            Console.ReadKey();
        }
    }
}
