using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Learn_VContainer_HelloWorld
{
    // VContainer'ı uygulamanıza entegre etmenin temel yolu şudur:
    
    // * Sahnenizde LifetimeScope'u miras alan bir bileşen oluşturun. Bir container'i ve bir scope'u vardır.
    // * LifetimeScope'u miras alan alt sınıfda C# kodu ile bağımlılıkları kaydedin. Bu kompozisyon köküdür.
    // * Sahne oynatılırken, LifetimeScope otomatik olarak Konteyner oluşturur ve kendi PlayerLoopSystem'e gönderir.
    
    // 2. Ardından, sınıfı otomatik olarak kablolayabilen bir ayar yazalım.
    // 3. LifetimeScope'unuza bağlı GameObject oluşturun
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private HelloScreen _helloScreen;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<HelloWorldService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GamePresenter>();
            // RegisterEntryPoint<GamePresenter>(), Unity'nin PlayerLoop olayıyla ilgili arayüzleri kaydetmek için
            // kullanılan bir takma addır.
            // Register<GamePresenter>(Lifetime.Singleton).As<ITickable>() ile benzerdir.
            // MonoBehaviour'a güvenmeden yaşam döngüsü olaylarını kaydetmek, etki alanı mantığı ve sunumun
            // ayrıştırılmasını kolaylaştırır!
            builder.RegisterComponent(_helloScreen);
        }
    }
}