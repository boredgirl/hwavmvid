using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hwavmvid.Motorsport.Raceways
{

    public class Motorsportraceway : IDisposable
    {

        private IJSObjectReference javascriptfile;
        private IJSRuntime jsruntime;

        public Motorsportraceway(IJSRuntime jsRuntime)
        {
            this.jsruntime = jsRuntime;
        }
        public async Task Initracewayervice()
        {
            this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>("import", "/Modules/Oqtane.ChatHubs/hwavmvidmotorsportjsinterop.js");
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