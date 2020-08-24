# Fable.ReactNative.Navigation

Provides Fable bindings for the stack navigator of [React Navigation](https://reactnavigation.org). 

View the requirements and read the docs on getting started with React Navigation [here](https://reactnavigation.org/docs/getting-started). 

When building Elmish-applications in React Native larger than the infamous "Counter app", navigating in the native way of iOS and Android quickly becomes wonky. This package lets you make use of the stack navigator of React Navigation, in order to get a native feel and look when navigating in your Fable React Native apps.

## Install

Assuming a similar setup to the one described in [this](https://github.com/martinmoec/fable-react-native-how-to) how-to.
- Add the ```Fable.ReactNative.Navigation``` Nuget package to your F# project

- Install the required npm-modules
    * ```@react-navigation/native```, ```@react-navigation/stack```, ```react-native-gesture-handler```, ```react-native-reanimated```, ```react-native-safe-area-context```, ```react-native-screens```,  ```@react-native-community/masked-view``` (See the React Navigation docs)

## Register your application

When using React Navigation you, simplified, provide the app with a set of screens and a function for rendering these, and React Navigation will handle the rest. The classic Elmish setup does not work too well with this approach. As an alternative to registering your application with Elmish at the core through ```withReactNative```, this package provides a helper function for registering your application. ```Fable.ReactNative.Navigation.Helpers.registerApp``` takes the name of your application (from ```react-native init```) and your base ```navigationContainer``` as an input. For example, if i followed the how-to linked at the top, with ```react-native init demo```:

```fsharp
open Fable.ReactNative.Navigation

let homePage () : Fable.React.ReactElement = 
    ...

let loginPage () : Fable.React.ReactElement =
    ...

let render = 
    navigationContainer [] [
        navigator [
            Props.NavigatorProps.InitialRouteName "home"
        ] [
            screen "home" homePage [] []
            screen "login" loginPage [] []
        ]
    ]

Helpers.registerApp "demo" render
```

Here an app is registered with two screens, ```home``` and ```login```, which will be handled with the ```homePage``` and ```loginPage``` functions respectively. The navigation handler of React Navigate will call the respected function for each screen added to the stack. From here you can build the functionality for each screen as you wish, for example using an MVU-architecture with ```useReducer``` (see sample). 

## Navigating and passing data between screens

The navigation handler of React Navigate provides every screen-handler function with a navigation-object. You can access this, with its bindings, as follows.

```fsharp
open Fable.ReactNative.Navigation
open Fable.ReactNative.Navigation.Types

...

let loginPage ( navigation : INavigation<int>) = 
    ...

let render = 
    navigationContainer [] [
        navigator [
            Props.NavigatorProps.InitialRouteName "home"
        ] [
            screen "home" home [] []
            screen "login" loginPage [] []
        ]
    ]

Helpers.registerApp "demo" render
```

Here the ```loginPage``` expects to receive a navigation object, where the data payload is an integer. The payload can be accessed through ```navigation.route.params```. 

The navigation object provides functions for navigating further. For example, you can navigate to the ```home``` page using 
```fsharp
navigation.navigation.navigate "home"
```
where ```"home"``` is the name of the screen registered in the ```navigationContainer```. Other examples are ```push("home")```, ```goBack()```, ```pop()``` and ```popToTop()```. See the React Navigation docs for more.

The package provides you with helper functions for navigating and pushing screens with data, ```navigateWithData``` and ```pushWithData```, which takes the navigation object, the screen name and your data.

```fsharp
open Fable.ReactNative.Navigation
open Fable.ReactNative.Navigation.Types

let HomeScreenName = "home"
let LoginScreenName = "login"

let homePage ( nav : INavigation<_> ) = 
    view [] [

        text [] "Navigate"
        |> touchableHighlightWithChild [
            OnPress ( fun _ -> navigateWithData nav LoginScreenName "This text is passed" ) 
        ]
    ]

let loginPage ( nav : INavigation<string> ) =
    view [] [
        text [] ( sprintf "Received: %s" nav.route.params )
    ]

let render = 
    navigationContainer [] [
        navigator [
            Props.NavigatorProps.InitialRouteName HomeScreenName
        ] [
            screen HomeScreenName homePage [] []
            screen LoginScreenName loginPage [] []
        ]
    ]

Helpers.registerApp "demo" render
```

## Notice

As you might have noticed, the type-safety of F# is not preserved between the data passed with the navigation function and the screen-function receiving the data, as it essentially is the navigation handler of React Navigation which will call your screen functions. 
