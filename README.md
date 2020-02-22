# AspCoreDependency

A plugin for *Microsoft.Extensions.DependencyInjection* to support automatically injecting all types which defines *ITransientType*, *IScopedType*, *ISingletonType*. 

# How to Use

* **Add DependencyInjection package to your project.**
```
 PM> Install-Package Microsoft.Extensions.DependencyInjection
```
* **Add AspCoreDependency dll to your project and implements interfaces according to Transient, Scoped, Singleton situation.**
```
   public interface IServiceA : IService, ISingletonType
   {}
   
   public interface IServiceB : IService, IScopedType
   {}
   
   public interface IServiceC : IService, ITransientType
   {}
   
```
* **Ensure fire the AutoBind method in Startup.cs**
```
   services.AutoBind();
        
        or with namespace constraint
 
   services.AutoBind(option =>
   {
      option.namespaceStr = "AspCoreDependency.Test.Concrete.NameSpace1";
   });
```
* **Alternatively, use injection with custom name for implementation types in Startup.cs**

```
  services.BindTransientByName<IService>()
                    .Add("serviceA", typeof(ManagerA))
                    .Add("serviceB", typeof(ManagerB))
                    .Add("serviceC", typeof(ManagerC))
                    .Build();
```
