using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Windows.Controls;

namespace RockPaperScissorsAI
{
    public class Neuron
    {
        public int weight1 { get; set; }
        public int weight2 { get; set; }
        static private double bestAccuracy = 0;

        public Neuron()
        {
            weight1 = 1;
            weight2 = 20000;
        }

        public Neuron(int weight1, int weight2)
        {
            this.weight1 = weight1;
            this.weight2 = weight2;
        }

        public Neuron(int[] weights)
        {
            weight1 = weights[0];
            weight2 = weights[1];
        }

        static public Neuron Learning(Neuron[] neurons, Button rockButton, Button scissorsButton, Button paperButton)
        {
            Random random = new Random();

            bestAccuracy = 0;
            Neuron bestNeuron = null;

            int iterations = 300;
            int successfulTests = 0;

            Button button = new Button();

            // Проведення тестів для нейронів
            foreach (Neuron n in neurons)
            {
                successfulTests = 0;
                for (int i = 0; i < iterations; i++)
                {
                    Option randomOption = (Option)random.Next(0, 3); // Генеруємо випадкову опцію

                    switch (randomOption)
                    {
                        case Option.Rock:
                            button = rockButton;
                            break;
                        case Option.Scissors:
                            button = scissorsButton;
                            break;
                        case Option.Paper:
                            button = paperButton;
                            break;
                        default:
                            break;
                    }

                    // Кількість чорних пікселів
                    int pixels = Picture.CountBlackPixels(Picture.ConvertToBlackAndWhite(Picture.GetFirstQuarter(Picture.GetBitmapFromButton(button))));

                    // Нейрон думає
                    Option aiOption = (Option)random.Next(0, 3);

                    if (0 < pixels && pixels < n.weight1)
                    {
                        aiOption = Option.Rock;
                    }
                    else if (n.weight1 < pixels && pixels < n.weight2)
                    {
                        aiOption = Option.Scissors;
                    }
                    else if (n.weight2 < pixels && pixels < 20000)
                    {
                        aiOption = Option.Paper;
                    }

                    if (RPS.Check(randomOption, aiOption)) // Перевірка
                    {
                        successfulTests++;
                    }
                }

                double accuracy = (double)successfulTests / iterations * 100; // Відсоток правильних відповідей
                if (accuracy > bestAccuracy) // Перевірка кращого нейрона
                {
                    bestAccuracy = accuracy;
                    bestNeuron = n;
                }
            }
            return bestNeuron;
        }

        //public static void NeuronLearn(Button rockButton, Button scissorsButton, Button paperButton)
        //{
        //    int neuronAmount = 10;
        //    Neuron[] neurons = new Neuron[neuronAmount];

        //    // Ініціалізація нейронів з випадковими вагами
        //    Random random = new Random();
        //    for (int i = 0; i < neuronAmount; i++)
        //    {
        //        int weight1 = random.Next(1, 27999);
        //        int weight2 = random.Next(weight1, 28000);
        //        neurons[i] = new Neuron(weight1, weight2);
        //    }

        //    Neuron bestNeuron = Learning(neurons, rockButton, scissorsButton, paperButton);
        //    MessageBox.Show($"Gen: 0\nBest Neuron: {bestNeuron.weight1} - {bestNeuron.weight2}\nAccuracy: {bestAccuracy}%");

        //    // Клонування кращого нейрона з відмінністю
        //    double difference = 10000; // Початкова відмінність
        //    int gen = 1;

        //    while (bestAccuracy < 95)
        //    {
        //        // Розмноження bestNeuron в Neuron[] з 10 нейронів
        //        for (int i = 0; i < neuronAmount; i++)
        //        {
        //            difference = difference - (difference / gen);

        //            int[] weights = new int[2];
        //            int weight1 = bestNeuron.weight1 + random.Next(-(int)difference, (int)difference);
        //            int weight2 = bestNeuron.weight2 + random.Next(-(int)difference, (int)difference);

        //            // Перевірка ваг
        //            if (0 > weight1 && weight1 < weight2 && weight2 < 28000)
        //            {
        //                weight1 = 0;
        //            }
        //            else if (weight1 > weight2)
        //            {
        //                int temp = weight1;
        //                weight1 = weight2;
        //                weight2 = temp;
        //            }
        //            else if (weight2 > 28000)
        //            {
        //                weight2 = 28000;
        //            }

        //            weights[0] = weight1;
        //            weights[1] = weight2;
        //            neurons[i] = new Neuron(weights); // Заміна поточного нейрона клоном bestNeuron
        //        }

        //        bestNeuron = Learning(neurons, rockButton, scissorsButton, paperButton);

        //        // Виведення результатів
        //        MessageBox.Show($"Gen: {gen}\nBest Neuron: {bestNeuron.weight1} - {bestNeuron.weight2}\nAccuracy: {bestAccuracy}%");
        //        gen++;
        //    }
        //}

        public static void NeuronLearn(Button rockButton, Button scissorsButton, Button paperButton)
        {
            int neuronAmount = 10;
            Neuron[] neurons = new Neuron[neuronAmount];
            Random random = new Random();

            // Ініціалізація нейронів з випадковими вагами
            for (int i = 0; i < neuronAmount; i++)
            {
                int weight1 = random.Next(1, 19999);
                int weight2 = random.Next(weight1, 20000);
                neurons[i] = new Neuron(weight1, weight2);
            }

            Neuron bestNeuron = Learning(neurons, rockButton, scissorsButton, paperButton);
            MessageBox.Show($"Gen: 0\nBest Neuron: {bestNeuron.weight1} - {bestNeuron.weight2}\nAccuracy: {bestAccuracy}%");

            double difference = 5000; // Початкова відмінність
            int gen = 1;

            while (bestAccuracy < 90)
            {
                Neuron previous = bestNeuron;
                double previousAccuracy = bestAccuracy;

                // Розмноження bestNeuron в Neuron[] з 10 нейронів
                for (int i = 0; i < neuronAmount; i++)
                {
                    double weight1Change = NextGaussian(random, 0, difference);
                    double weight2Change = NextGaussian(random, 0, difference);

                    int weight1 = (int)Math.Max(0, Math.Min(19999, bestNeuron.weight1 + weight1Change));
                    int weight2 = (int)Math.Max(weight1, Math.Min(20000, bestNeuron.weight2 + weight2Change));

                    neurons[i] = new Neuron(weight1, weight2);
                }

                if (bestAccuracy < previousAccuracy)
                {
                    bestNeuron = previous;
                    difference += difference; // Збільшення різниці
                }
                else
                {
                    difference -= difference / 10; // Зменшення різниці
                }

                bestNeuron = Learning(neurons, rockButton, scissorsButton, paperButton);
                    MessageBox.Show($"Gen: {gen}\nBest Neuron: {bestNeuron.weight1} - {bestNeuron.weight2}\nAccuracy: {bestAccuracy}%");
                gen++;
            }
            bestNeuron.SerializeWeight();
        }


        // Генеруємо значення за розподілом Гаусса
        private static double NextGaussian(Random random, double mean = 0, double stdDev = 1)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return mean + stdDev * randStdNormal;
        }

        static public RPS NeuronPlay(Button button)
        {
            Neuron neuron = new Neuron();
            neuron.DeserializeWeight();
            Random random = new Random();
            int pixels = Picture.CountBlackPixels(Picture.ConvertToBlackAndWhite(Picture.GetFirstQuarter(Picture.GetBitmapFromButton(button))));
            Option aiOption = (Option)random.Next(0, 3);

            if (0 < pixels && pixels < neuron.weight1)
            {
                aiOption = Option.Rock;
            }
            else if (neuron.weight1 < pixels && pixels < neuron.weight2)
            {
                aiOption = Option.Scissors;
            }
            else if (neuron.weight2 < pixels && pixels < 20000)
            {
                aiOption = Option.Paper;
            }
            return new RPS(aiOption);
        }

        public void SerializeWeight()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Neuron));
                using (TextWriter writer = new StreamWriter("weights.xml"))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during serialization: " + ex.Message);
            }
        }

        public void DeserializeWeight()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Neuron));

                if (File.Exists("weights.xml"))
                {
                    using (TextReader reader = new StreamReader("weights.xml"))
                    {
                        Neuron neuron = (Neuron)serializer.Deserialize(reader);
                        this.weight1 = neuron.weight1;
                        this.weight2 = neuron.weight2;
                    }
                }
                else
                {
                    MessageBox.Show("File 'weights.xml' does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during deserialization: " + ex.Message);
            }
        }

        public override string ToString()
        {
            return $"{weight1}; {weight2}";
        }
    }
}
