public interface IWalletSaver
{
    bool Save(Wallet wallet);
    bool Load(out Wallet wallet);
}