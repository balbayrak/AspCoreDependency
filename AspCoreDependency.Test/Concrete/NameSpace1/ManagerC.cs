using AspCoreDependency.Test.Abstract;

namespace AspCoreDependency.Test.Concrete.NameSpace1
{
    public class ManagerC : BaseManager, IServiceC
    {
        public override string Run()
        {
            return $"{this.GetType().Name} run with id: {this.id}";
        }
    }
}
