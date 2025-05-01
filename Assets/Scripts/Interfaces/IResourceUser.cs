namespace Interfaces
{
    public interface IResourceUser
    {
        void UseResource(int amount);
        void RecoverResource(int amount);
        int GetCurrentResource();
        int GetMaxResource();
    }
}
