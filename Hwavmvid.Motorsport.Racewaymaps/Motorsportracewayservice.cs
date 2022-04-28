using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hwavmvid.Motorsport.Shared.Items;
using System.Linq;

namespace Hwavmvid.Motorsport.Racewaymaps
{

    public class Motorsportraceway : IDisposable
    {

        private IJSRuntime jsruntime;
        private IJSObjectReference javascriptfile;

        public List<Racewaymapitem<Racewayitemtype>> Items { get; set; } = new List<Racewaymapitem<Racewayitemtype>>();

        public event Action<MotorsportracewayEvent> Onitemremoved;
        public event Action UpdateUI;
        public event Action OnUpdateComponent;
        public event Action<Racewaymapitem<Racewayitemtype>> ItemRemoved;

        public Motorsportraceway(IJSRuntime jsRuntime)
        {
            this.jsruntime = jsRuntime;
        }
        public async Task InitMotorsportracewayService()
        {
            this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>(
               "import", "/Modules/Oqtane.ChatHubs/hwavmvidmotorsportjsinterop.js");
        }
        public void Addmapitem(Racewaymapitem<Racewayitemtype> mapitem)
        {
            if (this.Items.Find(item => item.ColumnId.Equals(mapitem.Id)) == null)
            {
                this.Items.Add(mapitem);
                this.UpdateUI?.Invoke();
            }
        }
        public void Removemapitem(string id)
        {
            Racewaymapitem<Racewayitemtype> item = this.Items.FirstOrDefault(item => item.Id == id);
            if (item != null)
            {
                this.Items.Remove(item);
                this.ItemRemoved?.Invoke(item);
            }
        }
        public void ExposeRemovedItem(Racewaymapitem<Racewayitemtype> item)
        {
            MotorsportracewayEvent e = new MotorsportracewayEvent() { Item = item };
            this.Onitemremoved?.Invoke(e);
        }
        public void UpdateComponent()
        {
            this.OnUpdateComponent?.Invoke();
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