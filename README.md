# AspCoreDependency

A plugin for *Microsoft.Extensions.DependencyInjection* to support automatically injecting all types which defines *ITransientType*, *IScopedType*, *ISingletonType*. 

# How to Use

* **Add AspCoreDependency package to your project.**
```
 PM> Install-Package AspCoreDependency.Core
```
* **Implements interfaces according to Transient, Scoped, Singleton lifetimes.**
```csharp
   public interface IServiceA : IService, ISingletonType
   {}
   
   public interface IServiceB : IService, IScopedType
   {}
   
   public interface IServiceC : IService, ITransientType
   {}
   
```
* **Ensure fire the AutoBind method in Startup.cs**
```csharp
   services.AutoBind();
        
      //  or with namespace constraint
 
   services.AutoBind(option =>
   {
      option.namespaceStr = "AspCoreDependency.Test.Concrete.NameSpace1";
   });
```
* **Alternatively, use injection with custom name for implementation types in Startup.cs**

```csharp
  services.BindTransientByName<IService>()
                    .Add("serviceA", typeof(ManagerA))
                    .Add("serviceB", typeof(ManagerB))
                    .Add("serviceC", typeof(ManagerC))
                    .Build();
```

# Test Project
You can see all usage in *AspCoreDependency.Test.csproj* project
