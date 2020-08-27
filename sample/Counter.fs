module Counter

open Fable.ReactNative.Navigation

type private Model = {
    Counter : int
}
and private Message = 
    | Increment
    | Decrement

let private init () = { Counter = 1 }

let private update model msg = 
    match msg with
    | Increment -> { model with Counter = model.Counter + 1 }
    | Decrement -> { model with Counter = model.Counter - 1 }

module R = Fable.ReactNative.Helpers
module P = Fable.ReactNative.Props
open Fable.ReactNative.Props


// we dont expect the render function to be called from the navigation handler with any properties, so we ignore it
let private view model dispatch ( navigation : Types.INavigation<_> ) = 
    R.view [
        P.ViewProperties.Style [
            P.FlexStyle.Flex 1.0 
            P.FlexStyle.JustifyContent JustifyContent.Center
            P.FlexStyle.AlignItems ItemAlignment.Center
        ]
    ] [
        
        R.text [] ( sprintf "Counter: %i" model.Counter )

        R.text [] "+"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.FlexStyle.MarginLeft ( R.pct 2. )
            ]
            OnPress ( fun _ -> dispatch Increment ) 
        ]

        R.text [] "-"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.FlexStyle.MarginLeft ( R.pct 2. )
            ]
            OnPress ( fun _ -> dispatch Decrement ) 
        ]

        R.text [] "Open TestPage with current counter"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.FlexStyle.MarginLeft ( R.pct 2. )
                P.BorderWidth 2.
            ]

            // use helper function to navigate to the "test" screen with the current counter of our model
            OnPress ( fun _ -> navigateWithData navigation "test" model.Counter ) 
        ]
    ]



// create a simple model-update for handling our state and messages
let counter ( navigation : Types.INavigation<_> ) = 
    let initialModel = init ()
    Fable.React.FunctionComponent.Of ( fun ( mdl : Model ) -> 
    
        let x = Fable.React.HookBindings.Hooks.useReducer( update, mdl )

        view x.current x.update navigation
    ) initialModel
