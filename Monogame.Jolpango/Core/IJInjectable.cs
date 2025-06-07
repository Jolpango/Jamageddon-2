namespace MonoGame.Jolpango.Core
{
    public interface IJInjectable<T>
    {
        void Inject(T service);
    }
}
