public class PlayerContext
{
    public Wallet wallet { get; private set; } = new Wallet(new PlayerPrefsWalletSaver());
}
