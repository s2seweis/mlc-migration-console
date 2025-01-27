using Xunit;

namespace XmlToSqlApp.Tests
{
    public class UnitTestExample
    {
        [Fact]
        public void UnitTestExampleMethod()
        {
            // Arrange
            var expected = "Hello, World!"; // Erwarteter Wert
            var instance = new MyClass();  // Beispielklasse aus deinem Hauptprojekt

            // Act
            var result = instance.GetGreeting(); // Methode, die getestet wird

            // Assert
            Assert.Equal(expected, result); // Überprüfen, ob das Ergebnis korrekt ist
        }
    }

    // Beispielklasse im Hauptprojekt
    public class MyClass
    {
        public string GetGreeting()
        {
            return "Hello, World!";
        }
    }
}
