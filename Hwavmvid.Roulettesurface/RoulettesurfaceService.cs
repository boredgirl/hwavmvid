using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Hwavmvid.Roulettesurface
{

    public class RoulettesurfaceService : IDisposable
    {

        private IJSObjectReference javascriptfile;
        private IJSRuntime jsruntime;

        public RoulettesurfaceService(IJSRuntime jsRuntime)
        {
            this.jsruntime = jsRuntime;
        }

        public async Task InitRouletteService()
        {
            this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>(
               "import", "/Modules/Oqtane.ChatHubs/roulettesurfacejsinterop.js");
        }

        public async Task<string> Prompt(string message)
        {
            return await this.javascriptfile.InvokeAsync<string>("showPrompt", message);
        }

        public void Dispose()
        {
            if (javascriptfile != null)
                this.javascriptfile.DisposeAsync();
        }

    }
}