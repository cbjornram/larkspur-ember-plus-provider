![.NET Core Multi Build](https://github.com/IrisBroadcast/LarkspurEmberPlusProvider/workflows/.NET%20Core%20Multi%20Build/badge.svg?branch=0.1)

Larkspur EmBER+ Plus Provider
=========================================================

* Web Site: https://www.irisbroadcast.org
* Github: https://github.com/IrisBroadcast/LarkspurEmberWebProvider

Creates an EmBER+ tree with custom leafs and nodes. The information/nodes
is also available to access with SignalR and a REST application interface (API).

It's a standalone .NET Core Worker Service.

Could be used to bridge EmBER+ information and control to and from the web.

EmBER+ is a control protocol developed and maintained by [Lawo](https://github.com/Lawo).
Control and status is represented as nodes in a tree (XML-structure). You can
have your device or application to be a provider or consumer. This application
is a provider with a web-API backbone. Make your custom nodes writable or just readable.


![Larkspur Ember Provider Web Endpoint Screenshot](readme/webpage-larkspurweb.png)

![Larkspur Ember Provider View Screenshot](readme/larkspur-emberview.png)

## Roadmap
- Posibility to add functions
- Adding the complete template idea, for structuring your custom EmBER+ tree without changing the "code".

License
=======
Larkspur Ember Web Provider is using a library 'EmberLib.net' from Lawo GmbH.
```
EmberLib.net -- .NET implementation of the Ember+ Protocol
Copyright (c) 2012-2019 Lawo GmbH (http://www.lawo.com).
Distributed under the Boost Software License, Version 1.0.
```
There has been some modifications to the source code for .NET Standard adaption.
And we are not running on the latest commit. There is something not working with the
101 communication implementation. The files in the library that are using an
older version is marked in the header with 'XXX'.

```
Larkspur Ember Web Provider is Copyright (c) 2020 Roger Sandholm & Fredrik Bergholtz, Stockholm, Sweden
The code is licensed under the BSD 3-clause license.
```

The license for Larkspur Ember Web Provider is in the LICENSE.txt file

## How to get started

### Run and develop as a Microsoft Service

The application contains a package:

> Microsoft.Extensions.Hosting.WindowsServices

that enables the application to be runned as a windows service.

First build the application:

```
dotnet publish -r win-x64 -c Release
```

Then to get the service up and running use the windows service 'sc.exe' commands.
Open an elevated powershell:

```
sc.exe create LarkspurService BinPath=C:\[path-to-repository]\larkspur-ember-plus-provider\src\LarkspurEmberWebProvider\bin\Release\netcoreapp3.1\win-x64\publish\LarkspurEmberWebProvider.exe
```

Then you should start the service:

```
sc.exe start LarkspurService
```

Now you should be able to connect from EmberPlus Viewer (https://github.com/Lawo/ember-plus/releases) on localhost port 9003, or from a browser on localhost:5001

#### To stop service and/or rebuild

Stop service:

```
sc.exe stop LarkspurService
```

Remove service:

```
sc.exe delete LarkspurService
```

Then build the application and run again as described above.

## Responsible Maintainers
- [Roger Sandholm](https://github.com/Roog)
- [Fredrik Bergholtz](https://github.com/fredrikbergholtz-sr)
