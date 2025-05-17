namespace Interfaces
{
    public interface IResourceUser
    {
        void UseResource(int amount);
        void RecoverResource(int amount);
        int GetCurrentResource();
        int GetMaxResource();
        // event System.Action<int, int> OnResourceChanged; // Opcional para notificaciones
    }
}
