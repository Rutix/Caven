using Caven.Common.Windows;
using NUnit.Framework;

namespace Caven.Tests.Caven.Common.Windows
{
    [TestFixture]
    class HammingWindowTest
    {
        [Test]
        public void IsTheWindowLengthCorrect()
        {
            HammingWindow hanning = new HammingWindow();
            Assert.AreEqual(0, hanning.GetWindow(0).Length);
            Assert.AreEqual(10, hanning.GetWindow(10).Length);
            Assert.AreEqual(205, hanning.GetWindow(205).Length);
        }
    }
}
