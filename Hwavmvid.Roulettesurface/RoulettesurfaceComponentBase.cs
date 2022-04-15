using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hwavmvid.Roulette;
using System.Collections.Generic;

namespace Hwavmvid.Roulettesurface
{
    public class RoulettesurfaceComponentBase : ComponentBase, IDisposable
    {

        [Inject] public RouletteService RouletteService { get; set; }

        public const int NumberItemsContainerHeight = 400;
        public List<RoulettesurfaceNumber> NumberItems { get; set; }
        public RouletteNumber WinItem { get; set; }

        public string Black { get; set; } = "black";
        public string Red { get; set; } = "red";
        public string Carpetgreen { get; set; } = "rgba(33,109,70,0.8)"; // #216d46
        public string Transparent { get; set; } = "transparent";

        protected override async Task OnInitializedAsync()
        {
            this.RouletteService.OnWinItemDetected += WinItemDetected;
            this.NumberItems = this.GetSurfaceNumbers();
            await base.OnInitializedAsync();
        }
        public List<RoulettesurfaceNumber> GetSurfaceNumbers()
        {
            List<RoulettesurfaceNumber> items = new List<RoulettesurfaceNumber>();
            for (var i = 0; i <= 37; i++)
            {
                var item = new RoulettesurfaceNumber()
                {
                    Value = i,
                    Color = i == 0 ? this.Carpetgreen : i % 2 == 0 ? this.Black : this.Red,
                };

                items.Add(item);
            }

            return items;
        }
        public void WinItemDetected(RouletteEvent e)
        {
            this.WinItem = e.WinItem;
            this.StateHasChanged();
        }
        public void Dispose()
        {
            this.RouletteService.OnWinItemDetected -= WinItemDetected;
        }

    }
}
