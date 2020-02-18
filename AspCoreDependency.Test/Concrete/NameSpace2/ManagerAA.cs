using AspCoreDependency.Test.Abstract;

namespace AspCoreDependency.Test.Concrete.NameSpace2
{
    public class ManagerAA : BaseManager, IServiceA
    {
        public ManagerAA() : base()
        {

        }
        public override string Run()
        {
            return $"{this.GetType().Name} run with id: {this.id}";
        }
    }
}
