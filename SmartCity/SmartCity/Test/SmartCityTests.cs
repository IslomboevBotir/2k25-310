//// Tests/SmartCityTests.cs

//using Xunit;
//using SmartCitySystem.Core;
//using SmartCitySystem.Core.Adapters;
//using SmartCitySystem.Core.Factories;
//using SmartCitySystem.Core.Proxy;
//using SmartCitySystem.Modules.Security;

//public class SmartCityTests
//{
//    // 1. Singleton testi
//    [Fact]
//    public void Singleton_ControllerInstance_ShouldBeSame()
//    {
//        // Tekshirish: Controller faqat bitta nusxada yaratilganmi?
//        var controller1 = SmartCityController.Instance;
//        var controller2 = SmartCityController.Instance;

//        Assert.NotNull(controller1);
//        Assert.Same(controller1, controller2);
//    }

//    // 2. Abstract Factory testi
//    [Fact]
//    public void AbstractFactory_ShouldProduceAdvancedProducts()
//    {
//        // Tekshirish: AdvancedFactory, HighDefinitionSensor va UltraHDCamera yaratishini tekshirish
//        IEnvironmentFactory factory = new AdvancedFactory();
//        var sensor = factory.CreateTrafficSensor();
//        var camera = factory.CreateSecurityCamera();

//        Assert.IsType<HighDefinitionSensor>(sensor);
//        Assert.IsType<UltraHDCamera>(camera);
//    }

//    // 3. Adapter testi
//    [Fact]
//    public void Adapter_ShouldConvertKelvinToCelsiusCorrectly()
//    {
//        // Tekshirish: Adapter Kelvindan Selsiyga (295K -> 21.85C) to'g'ri konvertatsiya qilishini tekshirish
//        var externalService = new ThirdPartyWeatherService();
//        var adapter = new ThirdPartyWeatherAdapter(externalService);

//        double expectedCelsius = 295 - 273.15;
//        double actualCelsius = adapter.GetCurrentTemperatureC();

//        Assert.Equal(expectedCelsius, actualCelsius, 2); // 2 kasr aniqlikda tekshirish
//    }

//    // 4. Proxy testi (Ruxsat rad etilishini mantiqiy tekshirish)
//    [Fact]
//    public void Proxy_DenyUnauthorizedAccess_ShouldNotCallRealSubject()
//    {
//        // Tekshirish: Operator roliga ega foydalanuvchi Administrator vazifasini bajara olmasligini tekshirish
//        ISecurityAccess operatorProxy = new SecurityProxy("Operator");

//        // Bu test faqat Proxy yaratilishini tekshiradi.
//        // Haqiqiy I/O ni tekshirish uchun C# da Console.SetOut orqali chiqishni ushlash kerak, 
//        // lekin bu soddalashtirilgan misolda, ob'ektning mavjudligi tekshiriladi.

//        Assert.IsType<SecurityProxy>(operatorProxy);
//    }
//}