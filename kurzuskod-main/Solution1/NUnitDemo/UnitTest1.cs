namespace NUnitDemo
{
    public class Tests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test, Category("Auto"), Author("Akos"), Timeout(1000), Description("Ez a teszt azt csinálja, hogy ...")/*,TestOf(typeof())*/]
        public void Test1()
        {
            Thread.Sleep(5000);
        }

        [Test, Category("Motor")]
        public void Test2()
        {

        }

        [TestCase(4, "Akos")]
        [TestCase(10, "Akos2")]
        [TestCase(20, "Akos3")]
        public void Test3(int j, string t)
        {

        }

        [Test]
        public void Test4([Values(4, 10, 20)] int x, [Range(1, 10, 1)] int y)
        {

        }

        [Test]
        public void Test5([Random(10)] int x)
        {

        }

        [TestCaseSource(nameof(Generate))]
        public void Test10(int j, string t)
        {

        }

        static IEnumerable<object[]> Generate()
        {
            yield return new object[] { 4, "Akos" };
            yield return new object[] { 10, "Akos2" };
            yield return new object[] { 20, "Akos3" };
        }
    }
}