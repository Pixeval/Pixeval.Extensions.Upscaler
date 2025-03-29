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
        host.Initialize("zh-Hans", prefix + "TempState", prefix + @"LocalState\Extensions");
        await using var stream = Helper.OpenAsyncRead(@"..\..\..\..\Pixeval.Extensions.Upscaler\Resources\logo.png");
        await using var upscaler = new Upscaler();
        var result = await upscaler.UpscaleAsync(stream.ToIStream());
        Assert.IsNotNull(result);
    }
}
