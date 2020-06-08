# EventBus
EventBus is a system, which can dispatch published events to any registered handler accepting particular event argument. This means, that your event sources should no longer contain references to other components, only to event bus. 

This creates a clean separation between event sources and handlers, and allows to build handler pipelines dynamically in runtime, rather than compile time. So in theory you can even load third-party libraries adopted for this Event Bus and insert their handlers into the pipeline. For example, this behavior can be used to implement plugin systems.

## Installation
#### Using UnityPackageManager (for Unity 2018.3 or later)
Find the manifest.json file in the Packages folder of your project and edit it to look like this:
```js
{
  "dependencies": {
    "com.futuclass.event-bus": "https://github.com/futuclass/EventBus.git",
    ...
  },
}
```

## Features
- Custom event arguments.
- Automatic event data mapping to handlers by type.
- Ability to decouple code.
- Prioritized handlers.
- Cancelable events.
- Event pipeline building in runtime.

## Credits
Based on [Salday/EventBus](https://github.com/SaldayOpen/EventBus)
