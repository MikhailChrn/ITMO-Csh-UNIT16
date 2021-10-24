using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;
using System.IO;

namespace Задание_01
{
    //1.Необходимо разработать программу для записи информации о товаре в текстовый файл в формате json.
    //Разработать класс для моделирования объекта «Товар».
    //Предусмотреть члены класса «Код товара» (целое число), «Название товара» (строка), «Цена товара» (вещественное число).

    //Создать массив из 5-ти товаров, значения должны вводиться пользователем с клавиатуры.
    //Сериализовать массив в json-строку, сохранить ее программно в файл «Products.json».

    class Product
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
            //Данный блок активизирует работу с файлом с соответствующим адресом
            string sourceFile = @"C:\Users\Михаил\Desktop\ИТМО\Блок 1\Занятие 16\Products.json";
            StreamWriter sw = new StreamWriter(sourceFile, true);
            //Параметр true для sreamwriter указывает на накапливание информации в текущем файле
                                    
            JsonSerializerOptions options = new JsonSerializerOptions()
            //Данный блок взят из видеоурока. Он подключает возможность работы с Cyrillic символами
            //Также каждый новый атрибут JSON печатается с новой строки
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = false
            };

            const int n = 5; //Задаём количество товаров в массиве
            Product[] product = new Product[n]; //Объявляем массив из экземпляров класса Product
            string[] jsonStringArray = new string[n]; //Объявляем массив строк для работы JSON

            
            for (int i = 0; i < n; i++) //С использованием цикла последовательно заполняем все члены массива из класса Product вручную;
            {
                product[i] = new Product(); // Инициализирую каждый новый элемент массива класса Product, ИНАЧЕ появляется ошибка !!

                product[i].ProductCode = i + 1;
                Console.WriteLine("Введите нименование {0} товара:", product[i].ProductCode);
                product[i].ProductName = Console.ReadLine();
            ReadPrice:
                Console.WriteLine("Введите цену данного товара: ");
                try
                {
                    product[i].ProductPrice = Convert.ToDouble(Console.ReadLine());
                }
                catch { Console.WriteLine("Введено некорректное значение!"); goto ReadPrice; };

                jsonStringArray[i] = JsonSerializer.Serialize(product[i], options); //Тут же сериализуем Product в массив из JSON строк

            }
            //Данный блок сохоаняет все строки в файл;
            for (int i = 0; i < n; i++)
            {
                sw.WriteLine(jsonStringArray[i]);
            }

            sw.Close(); //Закрываем сеанс работы с файлом
        }
    }

}
