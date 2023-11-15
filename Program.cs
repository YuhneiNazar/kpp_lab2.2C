using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int numberOfProcessesToGenerate = 200;
        int lowerBoundInterval = 1;
        int upperBoundInterval = 10;
        int lowerBoundServiceTime = 2;
        int upperBoundServiceTime = 8;


        Random random = new Random();

        int destroyedProcesses = 0;

        object lockObject = new object();


        Thread processingThread = new Thread(() =>
        {
            for (int i = 0; i < numberOfProcessesToGenerate; i++)
            {
                int interval = random.Next(lowerBoundInterval, upperBoundInterval + 1);
                System.Threading.Thread.Sleep(interval);

                int serviceTime = random.Next(lowerBoundServiceTime, upperBoundServiceTime + 1);

                if (serviceTime <= interval)
                {
                    Console.WriteLine($"Процес {i + 1} обслуговується. Час обслуговування: {serviceTime} одиниць.");
                }
                else
                {
                    lock (lockObject)
                    {
                        Console.WriteLine($"Процес {i + 1} знищено. Час очікування: {interval} одиниць.");
                        destroyedProcesses++;
                    }
                }
            }
        });


        processingThread.Start();


        processingThread.Join();


        double destructionPercentage = (double)destroyedProcesses / numberOfProcessesToGenerate * 100;


        Console.WriteLine($"Загальна кількість знищених процесів: {destroyedProcesses}");
        Console.WriteLine($"Відсоток знищених процесів: {destructionPercentage:F2}%");
    }
}
