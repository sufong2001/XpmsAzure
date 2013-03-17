using Funq;
using ServiceStack.Configuration;
using System;

namespace Xpms.WebServices.IoC
{
    public class IocContainerAdapter : IContainerAdapter, IRelease
    {
        public Container Container { get; set; }

        public IocContainerAdapter(Container container)
        {
            Container = container;
        }

        public T TryResolve<T>()
        {
            return Container.TryResolve<T>();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public void Release(object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}