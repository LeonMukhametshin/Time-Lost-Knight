public static class BuffExtentions
{
    public static void Refresh(this IBuff buff, BuffContainer container)
    {
        buff.Deinitialize();
        buff.Initialize(container);
    }
}