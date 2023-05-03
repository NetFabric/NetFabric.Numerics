using System.Numerics;

namespace NetFabric.Numerics;

public interface IAngle<TSelf>
    : IAdditionOperators<TSelf, TSelf, TSelf>
    where TSelf: IAngle<TSelf>?
{

}