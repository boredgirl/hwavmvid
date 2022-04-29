using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hwavmvid.Motorsport.Shared.Items;
using System.Linq;

namespace Hwavmvid.Motorsport.Racewaymaps
{

    public class Motorsportracewayservice : IDisposable
    {

        private IJSRuntime jsruntime;
        private IJSObjectReference javascriptfile;

        public Racewaymap Map { get; set; }
        public List<Racewaymapitem<Racewayitemtype>> Items { get; set; } = new List<Racewaymapitem<Racewayitemtype>>();

        public event Action<MotorsportracewayEvent> Onitemremoved;
        public event Action UpdateUI;
        public event Action OnUpdateComponent;
        public event Action<Racewaymapitem<Racewayitemtype>> ItemRemoved;

        public Motorsportracewayservice(IJSRuntime jsRuntime)
        {
            this.jsruntime = jsRuntime;
        }
        public async Task InitMotorsportracewayService()
        {
            this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>(
               "import", "/Modules/Oqtane.ChatHubs/hwavmvidmotorsportjsinterop.js");
        }

        public void AddMapColumnItem(int rowid, int colid, Racewaymapitem<Racewayitemtype> item)
        {

            try
            {
                var col = this.GetMapColumn(rowid, colid);
                if (col != null)
                {
                    var itemlist = col.GetColumnItemsGenericlistBytype(item.Racewayitemtype);
                    if (itemlist != null)
                    {
                        itemlist.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
        public Racewaycolumn GetMapColumn(int rowid, int colid)
        {
            return this.Map.Columns.FirstOrDefault(item => item.RowId == rowid && item.ColumnId == colid);
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
        public void Dispose()
        {
            if (javascriptfile != null)
                this.javascriptfile.DisposeAsync();
        }

    }
}