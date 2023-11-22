using Bunit;
using Moq;
using Xunit;
using Hourglass.Pages.Components;
using Hourglass.Pages;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCore.Interfaces;

namespace Hourglass.UnitTests;

public class CounterTests : TestContext
{
    [Fact]
    public void IncrementCount_IncreasesCurrentCount()
    {
        // Arrange
        var cut = RenderComponent<Counter>();

        // Additional diagnostic output
        if (cut == null)
        {
            var componentNotFoundMessage = "Counter component not found. Ensure correct namespace, class name, and project references.";
            throw new Xunit.Sdk.XunitException(componentNotFoundMessage);
        }

        // Act
        cut.Find("button").Click();

        // Additional diagnostic output
        var countText = cut.Find("p[role='status']").TextContent;
        var diagnosticMessage = $"Current count: {countText}";
        System.Diagnostics.Debug.WriteLine(diagnosticMessage);

        // Assert
        Assert.Contains("Current count: 1", countText);
    }
}



public class ComponentTest : TestContext
{
    [Fact]
    public void Button_Click_StartsTimer()
    {
        // Arrange
        var cut = RenderComponent<StopWatch>(); // Replace with your actual component type

        // Act
        cut.Find(".startbutton").Click();

        // Assert

    }
}
