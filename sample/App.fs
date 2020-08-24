module App

open Fable.ReactNative.Navigation

// creates a navigation container with two screens, named counter and test
let render = 
    navigationContainer [] [
        navigator [
            Props.NavigatorProps.InitialRouteName "counter"
        ] [
            screen "counter" Counter.counter [] []
            screen "test" TestPage.view [] []
        ]
    ]

// use the name used for react-native init
Helpers.registerApp "app name here" render 