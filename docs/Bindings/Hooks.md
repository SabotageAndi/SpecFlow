# Hooks

Hooks (event bindings) can be used to perform additional automation logic at specific times, such as any setup required prior to executing a scenario. In order to use hooks, you need to add the `Binding` attribute to your class:

``` csharp
[Binding]
public class MyClass
{
    ...
}
```

Hooks are global, but can be restricted to run only for features or scenarios by defining a [scoped binding](Scoped-Step-Definitions.md), which can be filtered with [tags](https://github.com/techtalk/SpecFlow/wiki/Scoped-bindings#different-steps-for-different-tags). The execution order of hooks for the same type is undefined, unless specified explicitly.

## SpecFlow+ Runner Restrictions

When running tests in multiple threads with SpecFlow+ Runner, Before and After hooks such as `BeforeTestRun` and `AfterTestRun` are executed once for each thread. This is a limitation of the current architecture.

If you need to execute specific steps once per test run, rather than once per thread, you can do this using [deployment transformations](http://specflow.org/plus/documentation/SpecFlowPlus-Runner-Profiles/#DeploymentTransformation). An example can be found [here](https://github.com/techtalk/SpecFlow.Plus.Examples/tree/master/CustomDeploymentSteps).

## Supported Hook Attributes

| Attribute | Tag filtering* | Description |
|-----------|----------------|-------------|
| [BeforeTestRun]/[AfterTestRun] | not possible | Automation logic that has to run before/after the entire test run. <br/> __Note: As most of the unit test runners do not provide a hook for executing logic once the tests have been executed, the [AfterTestRun] event is triggered by the test assembly unload event. <br/> The exact timing and  thread of this execution may therefore differ for each test runner.__ <br/> The method it is applied to must be static. |
| [BeforeFeature]/[AfterFeature] | possible | Automation logic that has to run before/after executing each feature<br/> The method it is applied to must be static. |
| [BeforeScenario] or [Before]<br/>[AfterScenario] or [After] | possible | Automation logic that has to run before/after executing each scenario or scenario outline example |
| [BeforeScenarioBlock]<br/>[AfterScenarioBlock] | possible | Automation logic that has to run before/after executing each scenario block (e.g. between the "givens" and the "whens") |
| [BeforeStep]<br/>[AfterStep] | possible | Automation logic that has to run before/after executing each scenario step |

You can annotate a single method with multiple attributes.

## Hook Execution Order

By default the hooks of the same type (e.g. two `[BeforeScenario]` hook) are executed in an unpredictable order. If you need to ensure a specific execution order, you can specify the `Order` property in the hook's attributes.

```c#
[BeforeScenario(Order = 0)]
public void CleanDatabase()
{
    // we need to run this first...
}

[BeforeScenario(Order = 100)]
public void LoginUser()
{
    // ...so we can log in to a clean database
}
```

The number indicates the order, not the priority, i.e. the hook with the lowest number is always executed first.

If no order is specified, the default value is 10000. However, we do not recommend on relying on the value to order your tests and recommend specifying the order explicitly for each hook.

**Note:** If a hook throws an unhandled exception, subsequent hooks of the same type are not executed. If you want to ensure that all hooks of the same types are executed, you need to handle your exceptions manually.

## Tag Scoping

Most hooks support tag scoping. Use tag scoping to restrict hooks to only those features or scenarios that have *at least one* of the tags in the tag filter (tags are combined with OR). You can specify the tag in the attribute or using [scoped bindings](Scoped-Step-Definitions.md).
