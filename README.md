# n Dependents

Adds a Dependency Injection menu to the Project Settings window where you can assign a service container.
This framework does not use reflection. It is a concrete implementation of Abstract Service Locator:
https://github.com/moonymachine/abstract-service-locator

### Installation

n Dependents requires Unity version 2018 or later.
This package can be installed via Unity's Package Manager.

This package depends on the following packages, please install these first:
- https://github.com/moonymachine/abstract-service-locator
- https://github.com/moonymachine/scrutable-objects
- Then, open the Add (+) menu in the Package Manager’s toolbar.
- Select the "Install package from git URL" button.
- Enter the URL: https://github.com/moonymachine/n-dependents.git
- Select Install.
- Open the Dependency Injection menu in the Project Settings window to create the required CompositionRootAsset.
(The Dependency Injection menu is only available on 2018.3 or later. Otherwise, manually create a CompositionRoot asset in Resources.)

Or, add the following to your Packages/manifest.json and reload packages.

```json
{
  "dependencies": {
    "fun.moonymachine.abstract-service-locator": "https://github.com/moonymachine/abstract-service-locator.git",
    "fun.moonymachine.scrutable-objects": "https://github.com/moonymachine/scrutable-objects.git",
    "fun.moonymachine.n-dependents": "https://github.com/moonymachine/n-dependents.git"
  }
}
```

### Example Service Container

This package uses samples, which require Unity version 2019 or later.
If using an earlier version, copy the contents from the "Samples~" folder to your Assets directory.

- Select this package in the Package Manager window.
- Press the Samples button.
- Import the Example Service Container sample.
- Copy the Scripts folder from the "Example Service Container" folder to the "Dependency Injection" folder created earlier.
- Delete the empty Samples folders that were created.
- Right-click the "Dependency Injection\Assets" folder, then select Create > Service Container.
- Drag the ServiceContainer asset to the Service Container field in the Dependency Injection menu.
- Press play to see the service container initialize, update, and shut down, regardless of the current scene.
- Explore the example scripts for ServiceContainerAsset and the ServiceContainer that it creates.
- The service container can provide generic service types to MonoBehaviours through the Abstract Service Locator.
