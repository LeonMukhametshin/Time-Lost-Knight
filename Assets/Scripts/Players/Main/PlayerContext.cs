public class PlayerContext
{
    public Wallet wallet { get; private set; } = new Wallet(new PlayerPrefsWalletSaver());
    public HealthSystem health { get; private set; } = new HealthSystem(100, 100);
}