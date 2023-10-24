namespace NUnitDemo2
{

    public class ModuleUnderTest
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class Person { public string Name { get; set; } }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            int testResult = 9;
            string r = string.Empty;

            Assert.That(testResult, Is.Not.EqualTo(10).Within(2));
            Assert.That(testResult, Is.Not.Zero.And.Not.EqualTo(10));
            Assert.That(testResult, Is.Not.LessThanOrEqualTo(10));

            Assert.That(testResult == 9, Is.True);
            Assert.That(new Person(), Has.Property("Name").Matches<string>(v => v.Length > 6));
            Assert.That(new Person(), Has.Property("Name").Length.GreaterThan(6));

            List<int> ints = new List<int>();
            List<Person> people = new List<Person>();
            List<Person> people2 = new List<Person>();

            Assert.That(ints, Does.Contain(3));
            Assert.That(ints, Has.All.GreaterThan(3));
            Assert.That(people, Has.All.Property("Name").Length.GreaterThan(6));
            Assert.That(people, Has.Count.GreaterThan(3));
            Assert.That(people, Is.EquivalentTo(people2));

            CollectionAssert.AllItemsAreUnique(people);
            Assert.That(people, Has.All.Unique);

            //Assert.That(r, Is.Null);
        }
    }
}