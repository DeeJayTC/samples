// TCDev.de 2022/03/31
// GenericControllers.Apples.cs
// https://www.github.com/deejaytc/dotnet-utils

namespace GenericControllers;

public class Something : IObjectBase
{
   public string Name { get; set; }
   public string Description { get; set; }
   public string TheThing = "Something";
   public int Id { get; set; }
}

public class SomeOtherThing : IObjectBase
{
   public string Name { get; set; }
   public string Description { get; set; }
   public string TheThing = "TheOtherThing";
   public int Id { get; set; }
}

public interface IObjectBase
{
   int Id { get; set; }
}
