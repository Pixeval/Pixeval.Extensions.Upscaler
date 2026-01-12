using Pixeval.Extensions.Common;
using Pixeval.Extensions.Upscaler;

namespace UpscalerTest;

[TestClass]
public sealed class Test
{
    [TestMethod]
    public async Task TestMethod()
    {
        IExtensionsHost host = new ExtensionsHost();
        const string prefix = @"C:\Users\poker\AppData\Local\Packages\PokerKo.4454907E5DDB5_0wpjzgvbyjvyr\";
        host.Initialize("zh-Hans", prefix + "TempState", prefix + @"LocalState\Extensions", null!);
        foreach (var extension in host.Extensions)
            extension.OnExtensionLoaded();
        await using var stream = Helper.OpenAsyncRead(@"..\..\..\..\Pixeval.Extensions.Upscaler\Resources\logo.png");
        await using var stream2 = Helper.OpenAsyncWrite(prefix + @"TempState\logo_output.png");
        await using var upscaler = new Upscaler();
        await upscaler.UpscaleAsync(stream, stream2);
        foreach (var extension in host.Extensions)
            extension.OnExtensionLoaded();
    }
}
