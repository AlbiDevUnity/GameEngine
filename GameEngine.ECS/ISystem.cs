
namespace GameEngine.ECS.First
{
    public interface ISystem
    {
        //signature

        void Process(IEnumerable<Entity> entities);
    }
}
