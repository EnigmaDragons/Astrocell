using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Astrocell.Battles
{
    [TestClass]
    public class ContentTests
    {
        [TestMethod]
        public void Content_SampleCharacters_CanCreate()
        {
            Samples.CreateDumbBrute();
            Samples.CreateElectrician();
        }
    }
}
