using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Moq;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit
{
    class AutoMoqServiceResolver : ISubDependencyResolver
    {
        private IKernel kernel;

        public AutoMoqServiceResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return dependency.TargetType.IsInterface;
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            var mock = typeof(Mock<>).MakeGenericType(dependency.TargetType);
            return ((Mock)kernel.Resolve(mock)).Object;
        }
    }
}
