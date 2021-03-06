using BlazorAlerts;
using BlazorBrowserResize;
using BlazorColorPicker;
using BlazorDraggableList;
using BlazorFileUpload;
using BlazorModal;
using BlazorVideo;
using BlazorNotifications;
using BlazorPager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Oqtane.ChatHubs.Hubs;
using Oqtane.Extensions;
using Oqtane.Infrastructure;
using System;
using BlazorDynamicLayout;
using Oqtane.ChatHubs.Models;
using BlazorVideoPlayer;
using BlazorSlider;
using BlazorDevices;
using BlazorDownload;
using System.Net.Http;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using Hwavmvid.Jsapinotifications;
using Hwavmvid.Blackjack;
using Hwavmvid.Roulette;
using Hwavmvid.Rouletteitellisense;
using Hwavmvid.Roulettesurface;
using Hwavmvid.Roulettecoins;
using Hwavmvid.Roulettebetoptions;
using Hwavmvid.Roulettebets;
using Hwavmvid.Motorsport.Racewaymaps;

namespace Oqtane
{
    public class ExtendedStartup : IServerStartup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddMemoryCache();

            services.TryAddHttpClientWithAuthenticationCookie();

            services.AddScoped<BlazorAlertsService, BlazorAlertsService>();
            services.AddScoped<BlazorDraggableListService, BlazorDraggableListService>();
            services.AddScoped<BlazorFileUploadService, BlazorFileUploadService>();
            services.AddScoped<BlazorColorPickerService, BlazorColorPickerService>();
            services.AddScoped<BlazorVideoService, BlazorVideoService>();
            services.AddScoped<BlazorVideoPlayerService, BlazorVideoPlayerService>();
            services.AddScoped<BlazorBrowserResizeService, BlazorBrowserResizeService>();
            services.AddScoped<BlazorModalService, BlazorModalService>();
            services.AddScoped<BlazorNotificationsService, BlazorNotificationsService>();
            services.AddScoped<BlazorDynamicLayoutService, BlazorDynamicLayoutService>();
            services.AddScoped<BlazorSliderService, BlazorSliderService>();
            services.AddScoped<BlazorDevicesService, BlazorDevicesService>();
            services.AddScoped<BlazorDownloadService, BlazorDownloadService>();
            services.AddScoped<BlazorPagerService<ChatHubRoom>, BlazorPagerService<ChatHubRoom>>();
            services.AddScoped<BlazorPagerService<ChatHubUser>, BlazorPagerService<ChatHubUser>>();
            services.AddScoped<BlazorPagerService<ChatHubCam>, BlazorPagerService<ChatHubCam>>();
            services.AddScoped<BlazorPagerService<ChatHubInvitation>, BlazorPagerService<ChatHubInvitation>>();
            services.AddScoped<BlazorPagerService<ChatHubIgnore>, BlazorPagerService<ChatHubIgnore>>();
            services.AddScoped<BlazorPagerService<ChatHubIgnoredBy>, BlazorPagerService<ChatHubIgnoredBy>>();
            services.AddScoped<BlazorPagerService<ChatHubModerator>, BlazorPagerService<ChatHubModerator>>();
            services.AddScoped<BlazorPagerService<ChatHubBlacklistUser>, BlazorPagerService<ChatHubBlacklistUser>>();
            services.AddScoped<BlazorPagerService<ChatHubWhitelistUser>, BlazorPagerService<ChatHubWhitelistUser>>();
            services.AddScoped<JsapinotificationService, JsapinotificationService>();
            services.AddScoped<BlackjackService, BlackjackService>();
            services.AddScoped<RouletteService, RouletteService>();
            services.AddScoped<RoulettesurfaceService, RoulettesurfaceService>();
            services.AddScoped<RoulettecoinsService, RoulettecoinsService>();
            services.AddScoped<RouletteBetoptionsService, RouletteBetoptionsService>();
            services.AddScoped<RouletteBetsService, RouletteBetsService>();
            services.AddScoped<RouletteitellisenseService, RouletteitellisenseService>();
            services.AddScoped<Motorsportracewayservice, Motorsportracewayservice>();

            services.AddServerSideBlazor()
                .AddHubOptions(options => options.MaximumReceiveMessageSize = 512 * 1024);

            services.AddCors(option =>
            {
                option.AddPolicy("wasmcorspolicy", (builder) => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

            services.AddSignalR()
                .AddHubOptions<ChatHub>(options =>
                {
                    options.EnableDetailedErrors = true;
                    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                    options.MaximumReceiveMessageSize = Int64.MaxValue;
                    options.StreamBufferCapacity = Int32.MaxValue;
                })
                .AddNewtonsoftJsonProtocol(options =>
                {
                    options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseTenantResolution();
            app.UseBlazorFrameworkFiles();
            app.UseRouting();
            app.UseCors("wasmcorspolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub", options =>
                {
                    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                    options.ApplicationMaxBufferSize = Int64.MaxValue;
                    options.TransportMaxBufferSize = Int64.MaxValue;
                    options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);
                    options.LongPolling.PollTimeout = TimeSpan.FromSeconds(10);
                });
            });
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {

        }

    }

    public static class WasmChatServiceCollectionExtensions
    {
        internal static IServiceCollection TryAddHttpClientWithAuthenticationCookie(this IServiceCollection services)
        {

            var httpClientService = services.FirstOrDefault(x => x.ServiceType == typeof(HttpClient));
            if (httpClientService != null)
            {
                services.Remove(httpClientService);
            }

            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                services.AddScoped(s =>
                {
                    // creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var navigationManager = s.GetRequiredService<NavigationManager>();
                    var client = new HttpClient(new HttpClientHandler { UseCookies = false });
                    client.BaseAddress = new Uri(navigationManager.Uri);
                    client.Timeout = TimeSpan.FromHours(20);

                    // set the cookies to allow HttpClient API calls to be authenticated
                    var httpContextAccessor = s.GetRequiredService<IHttpContextAccessor>();
                    foreach (var cookie in httpContextAccessor.HttpContext.Request.Cookies)
                    {
                        client.DefaultRequestHeaders.Add("Cookie", cookie.Key + "=" + cookie.Value);
                    }

                    return client;
                });
            }

            return services;
        }
    }

}
