using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pixeval.Extensions.Common;
using Pixeval.Extensions.ImageTransformers.RealESRGAN;

namespace Pixeval.Extensions.ImageTransformers.Tests;

[TestClass]
public sealed class Test
{
    [TestMethod]
    [DataRow(@"..\..\..\..\Pixeval.Extensions.RealESRGAN\Assets\logo.png")]
    public async Task TestMethod(string imagePath)
    {
        IExtensionsHost host = new ExtensionsHost();
        const string prefix = @"C:\Users\poker\AppData\Local\Packages\PokerKo.4454907E5DDB5_0wpjzgvbyjvyr\";
        host.Initialize("zh-Hans", prefix + "TempState", prefix + @"LocalState\Extensions", null!);
        foreach (var extension in host.Extensions)
            extension.OnExtensionLoaded();
        await using var stream = Helper.OpenAsyncRead(imagePath);
        await using var stream2 = Helper.OpenAsyncWrite(prefix + @"TempState\logo_output.png");
        await using var realESRGAN = new RealESRGAN.RealESRGAN();
        await realESRGAN.SuperResolveAsync(stream, stream2);
        foreach (var extension in host.Extensions)
            extension.OnExtensionUnloaded();
    }
}
