
using SmartCitySystem.Core;
using SmartCitySystem.Core.Proxy;
using SmartCitySystem.Modules.Security;
class Program
{
    static void Main(string[] args)
    {
        var controller = SmartCityController.Instance;

        ISecurityAccess adminProxy = new SecurityProxy("Administrator");
        ISecurityAccess operatorProxy = new SecurityProxy("Operator");

        bool running = true;
        while (running)
        {
            Console.WriteLine("SmartCity Boshqaruv Konsoli");
            Console.WriteLine("1. Shahar Operatsiyasini Optimallashtirish (Facade)");
            Console.WriteLine("2. Sensorlar Statusini Ko'rish (Abstract Factory)");
            Console.WriteLine("3. Ob-havo Ma'lumotlarini Yangilash (Adapter)");
            Console.WriteLine("4. Xavfsizlik Tizimini Boshqarish (Proxy)");
            Console.WriteLine("5. Chiqish");
            Console.Write("Tanlovingizni kiriting: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n--- Tanlov: 1. Shahar Operatsiyasini Optimallashtirish ---");
                    controller.OptimizeCityOperation();
                    break;

                case "2":
                    Console.WriteLine("\n--- Tanlov: 2. Sensorlar Statusini Ko'rish ---");
                    controller.DisplaySensorStatus();
                    break;

                case "3":
                    Console.WriteLine("\n--- Tanlov: 3. Ob-havo Ma'lumotlarini Yangilash ---");
                    controller.UpdateWeatherInfo();
                    break;

                case "4":
                    Console.WriteLine("\n--- Tanlov: 4. Xavfsizlik Tizimini Boshqarish (Proxy) ---");
                    HandleSecurityMenu(adminProxy, operatorProxy);
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("SmartCity tizimi o'chirildi.");
                    break;

                default:
                    Console.WriteLine("Noto'g'ri tanlov. Qayta urinib ko'ring.");
                    break;
            }
        }
    }

    static void HandleSecurityMenu(ISecurityAccess adminProxy, ISecurityAccess operatorProxy)
    {
        Console.WriteLine("\n--- Xavfsizlik Boshqaruvi ---");
        Console.WriteLine("a. Kamera Yozuvlariga Kirish (Operator talab qilinadi)");
        Console.WriteLine("b. Trevoga Tizimini Qayta Tiklash (Administrator talab qilinadi)");
        Console.Write("Sub-tanlovingizni kiriting (a/b): ");
        string? subChoice = Console.ReadLine()?.ToLower();

        switch (subChoice)
        {
            case "a":
                Console.WriteLine("\n**Operator ruxsati bilan:**");
                operatorProxy.AccessCameraFeeds(); 
                Console.WriteLine("\n**Administrator ruxsati bilan (mumkin emas):**");
                adminProxy.AccessCameraFeeds();
                break;
            case "b":
                Console.WriteLine("\n**Operator ruxsati bilan (rad etilishi kerak):**");
                operatorProxy.ResetAlarmSystem();
                Console.WriteLine("\n**Administrator ruxsati bilan:**");
                adminProxy.ResetAlarmSystem();  
                break;
            default:
                Console.WriteLine("Noto'g'ri tanlov.");
                break;
        }
    }
}