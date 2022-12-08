namespace Banks.Services;

public class Singleton
{
    private static Singleton? instance;

    protected Singleton()
    {
    }

    public static Singleton GetInstance()
    {
        if (instance == null)
            instance = new Singleton();
        return instance;
    }
}