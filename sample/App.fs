module App

open Fable.ReactNative.Navigation

// creates a navigation container with two screens, named counter and test
let render = 
    navigationContainer [] [
        Stack.navigator [
            Stack.NavigatorProps.InitialRouteName "counter"
        ] [
            Stack.screen "counter" Counter.counter [] []
            Stack.screen "test" TestPage.view [] []
        ]
    ]

// register our app with the AppRegistry. Alternative to withReactNative when we dont want to include Elmish
// use the name used for react-native init
Helpers.registerApp "app name here" render 