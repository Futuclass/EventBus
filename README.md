# EventBus [![openupm](https://img.shields.io/npm/v/com.futuclass.event-bus?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.futuclass.event-bus/)


EventBus is a system, which can dispatch published events to any registered handler accepting particular event argument. This means, that your event sources should no longer contain references to other components, only to event bus. 

This creates a clean separation between event sources and handlers, and allows to build handler pipelines dynamically in runtime, rather than compile time. So in theory you can even load third-party libraries adopted for this Event Bus and insert their handlers into the pipeline. For example, this behavior can be used to implement plugin systems.

## Installation

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.futuclass.event-bus
```

### Install via Git URL

Open *Packages/manifest.json* with your favorite text editor. Add the following line to the dependencies block.

    {
        "dependencies": {
            "com.futuclass.event-bus": "https://github.com/Futuclass/EventBus.git"
        }
    }

Notice: Unity Package Manager records the current commit to a lock entry of the *manifest.json*. To update to the latest version, change the hash value manually or remove the lock entry to resolve the package.

    "lock": {
      "com.futuclass.event-bus": {
        "revision": "master",
        "hash": "..."
      }
    }


## Features
- Custom event arguments.
- Automatic event data mapping to handlers by type.
- Ability to decouple code.
- Prioritized handlers.
- Cancelable events.
- Event pipeline building in runtime.

## Credits
Based on [Salday/EventBus](https://github.com/SaldayOpen/EventBus)
