module TestPage

open Fable.ReactNative.Navigation

module R = Fable.ReactNative.Helpers
module P = Fable.ReactNative.Props
open Fable.ReactNative.Props

let view ( navigation : Types.INavigation<int> ) = 
    R.view [
        P.ViewProperties.Style [
            P.FlexStyle.Flex 1.0 
            P.FlexStyle.JustifyContent JustifyContent.Center
            P.FlexStyle.AlignItems ItemAlignment.Center
        ]
    ] [
        
        R.text [] ( sprintf "The screen was opened with counter: %i" navigation.route.``params`` )

        R.text [] "Open TestPage again with 10"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.BorderWidth 2.
            ]
            OnPress ( fun _ -> pushWithData navigation "test" 10 )
        ]

        R.text [] "Go back"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.BorderWidth 2.
            ]
            OnPress ( fun _ -> navigation.navigation.goBack () )
        ]

        R.text [] "pop"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.BorderWidth 2.
            ]
            OnPress ( fun _ -> navigation.navigation.pop () )
        ]

        R.text [] "pop to top"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.BorderWidth 2.
            ]
            OnPress ( fun _ -> navigation.navigation.popToTop () )
        ]

        R.text [] "Update title"
        |> R.touchableHighlightWithChild [
            P.TouchableHighlightProperties.Style [
                P.FlexStyle.Padding( R.dip 4. )
                P.BorderWidth 2.
            ]
            OnPress ( fun _ -> setOptions navigation [ Props.ScreenOptions.Title "Updated title" ] )
        ]
     
    ]