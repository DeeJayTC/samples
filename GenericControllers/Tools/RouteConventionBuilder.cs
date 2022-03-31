﻿// TCDev.de 2022/03/31
// GenericControllers.RouteConventionBuilder.cs
// https://www.github.com/deejaytc/dotnet-utils

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace GenericControllers.Tools;

/// <summary>
///    Applies route conventions to allow routes for auto generated controllers
///    One of the main pieces to make the magic work.
/// </summary>
public class GenericControllerRouteConvention : IControllerModelConvention
{
   public void Apply(ControllerModel controller)
   {
      if (controller.ControllerType.IsGenericType)
      {
         var genericType = controller.ControllerType.GenericTypeArguments[0];
         controller.ControllerName = genericType.Name;
         if (controller.Selectors.Count > 0)
         {
            var currentSelector = controller.Selectors[0];
            currentSelector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"/{genericType.Name}"));
         }
         else
         {
            controller.Selectors.Add(new SelectorModel
            {
               AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"/{genericType.Name}"))
            });
         }
      }
   }
}
