using VContainer.Unity;

namespace Learn_VContainer_HelloWorld
{
    // 4. Yeni HelloWorldService nasıl kullanılır?
    // Kayıtlı nesneler otomatik olarak bağımlılık enjeksiyonuna sahip olacaktır.
    // Aşağıdaki gibi:

    // 5. Unity'de bir uygulama yazmak için Unity'nin yaşam döngüsü olaylarını
    // kesmek zorundayız. (Tipik olarak MonoBehaviour'un Start / Update / OnDestroy / vb.)
    // VContainer ile kayıtlı nesneler bunu MonoBehaviour'dan bağımsız olarak yapabilir.
    // Bu, bazı işaretleyici arayüzlerinin uygulanması ve kaydedilmesiyle otomatik olarak yapılır.


    // public class GamePresenter : ITickable
    // {
    //     private readonly HelloWorldService _helloWorldService;
    //
    //     public GamePresenter(HelloWorldService helloWorldService)
    //     {
    //         _helloWorldService = helloWorldService;
    //     }
    //
    //     public void Tick()
    //     {
    //         _helloWorldService.Hello();
    //     }
    // }

    // Şimdi, Tick() Unity'nin Güncelleme zamanlamasında yürütülecektir.

    // Bu nedenle, herhangi bir yan etki giriş noktasını işaretleyici arayüzü üzerinden tutmak iyi bir uygulamadır.

    // (Tasarım gereği, MonoBehaviour için Start / Update vb. kullanmak yeterlidir. VContainer'ın işaretleyici arayüzü,
    // alan mantığı ve sunum mantığının giriş noktasını ayırmak için bir fonksiyondur. )

    // Bunu Unity'nin yaşam döngüsü olaylarında çalışıyor olarak kaydetmeliyiz.

    public class GamePresenter : IStartable
    {
        private readonly HelloWorldService _helloWorldService;
        private readonly HelloScreen _helloScreen;
        
        public GamePresenter(HelloWorldService helloWorldService, HelloScreen helloScreen)
        {
            _helloWorldService = helloWorldService;
            _helloScreen = helloScreen;
        }
        
        public void Start()
        {
            _helloScreen.HelloButton.onClick.AddListener(() =>
            {
                _helloWorldService.Hello();
            });   
        }
    }
    
    // Bunu yaparak, domain logic / control flow / view component'i ayırmayı başardık.
    // GamePresenter: Sadece Control Flow'dan sorumludur.
    // HelloWorldService: Yalnızca işlevsellikten sorumludur, her zaman, her yerden çağrılabilir.
    // HelloScreen: Yalnızca View'den sorumludur.
}