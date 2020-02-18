using AspCoreDependency.Test.Abstract;

namespace AspCoreDependency.Test.Concrete.NameSpace1
{
    public class ManagerA : BaseManager, IServiceA
    {
        public override string Run()
        {
            return $"{this.GetType().Name} run with id: {this.id}";
        }
    }
}
