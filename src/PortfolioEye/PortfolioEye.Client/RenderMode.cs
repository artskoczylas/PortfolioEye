using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace PortfolioEye.Client;

public static class MyRenderMode
{
    public static IComponentRenderMode InteractiveWasmWithoutPrerendering { get; } = new InteractiveWebAssemblyRenderMode(prerender: false);
    //public static IComponentRenderMode InteractiveWasmWithoutPrerendering { get; } = Microsoft.AspNetCore.Components.Web.RenderMode.InteractiveWebAssembly;

}