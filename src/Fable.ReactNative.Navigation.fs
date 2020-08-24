module Fable.ReactNative.Navigation

open Fable.Core
open Fable.Import
open Fable.Core.JsInterop
open Fable.React


module Types = 
    type INavigatorStatic =
        interface end
    and INavigator = INavigatorStatic
    and IScreenStatic = interface end
    and IScreen = IScreenStatic

    type INavigationContainerStatic = interface end
    and INavigationContainer = INavigationContainerStatic

    type Globals = 
        [<Import("NavigationContainer", "@react-navigation/native")>] 
        static member NavigationContainer with get(): INavigationContainerStatic = jsNative and set(v: INavigationContainerStatic): unit = jsNative

    type ICreateStackNavigator =
        abstract member registerComponent : string -> ( unit -> obj ) -> unit
        abstract member setRoot : obj -> JS.Promise<obj>

        abstract member Navigator : ReactElement

        abstract member Screen : ReactElement

    type INavigationObject =
        {
            navigate    : string    -> unit
            goBack      : unit      -> unit
            push        : string    -> unit
            pop         : unit      -> unit
            popToTop    : unit      -> unit
            setOptions  : obj       -> unit
            reset       : obj       -> unit
            setParams   : obj       -> unit
        }

    // 'a = the type of data expected to be passed with the navigation
    type IRouteObject<'a> = 
        {
            key         : string
            name        : string
            ``params``  : 'a
        }
    
    type INavigation<'b> =
        abstract member navigation : INavigationObject
        abstract member route : IRouteObject<'b>
    
    [<Import("createStackNavigator", from="@react-navigation/stack")>]    
    let CreateStackNavigator () : ICreateStackNavigator = jsNative


module Props =
    [<StringEnum>]
    type AnimationType =
        | Push
        | Pop

    [<StringEnum>]
    type GestureDirection =
        | Horizontal
        | [<CompiledName("horizontal-inverted")>] HorizontalInverted
        | Vertical
        | [<CompiledName("vertical-inverted")>] VerticalInverted
    
    type Inset = 
        | Bottom of float 
        | Left of float 
        | Right of float
        | Top of float

    type ResponseDistance =
        | Horizontal of float
        | Vertical of float
    
    [<StringEnum>]
    type TitleAlign = 
        | Left
        | Center

    type TransitionConfig = 
        | Damping                   of int
        | Duration                  of float
        | Easing                    of float
        | Mass                      of int
        | OvershootClamping         of bool
        | RestDisplacementThreshold of float
        | RestSpeedThreshold        of float
        | Stiffness                 of int
    
    type TransitionProp = 
        | Animation of string
        static member Config ( x : seq<TransitionConfig> ) = !!("config", keyValueList CaseRules.LowerFirst x )

    type Transition = 
        static member Open ( x : seq<TransitionProp> ) = !!("open", keyValueList CaseRules.LowerFirst x )
        static member Close ( x : seq<TransitionProp> ) = !!("close", keyValueList CaseRules.LowerFirst x )

    type ScreenOptions =
        | AnimationEnabled              of bool
        | AnimationTypeForReplace       of AnimationType
        | CardOverlay                   of ( obj -> ReactElement )
        | CardOverlayEnabled            of bool
        | CardShadowEnabled             of bool
        | GestureDirection              of GestureDirection
        | GestureEnabled                of bool
        | GestureVelocityImpact         of float
        | Header                        of ( obj -> ReactElement )
        | HeaderBackAllowFontScaling    of bool
        | HeaderBackground              of ( obj -> ReactElement )
        | HeaderBackImage               of ( obj -> ReactElement )
        | HeaderBackTitle               of string
        | HeaderBackTitleVisible        of bool
        | HeaderLeft                    of ( obj -> ReactElement )
        | HeaderPressColorAndroid       of string
        | HeaderRight                   of ( obj -> ReactElement )
        | HeaderShown                   of bool
        | HeaderStatusBarHeight         of float
        | HeaderTintColor               of string
        | HeaderTitle                   of ReactElement
        | HeaderTitleAlign              of TitleAlign
        | HeaderTitleAllowFontScaling   of bool
        | HeaderTransparent             of bool
        | HeaderTruncatedBackTitle      of string
        | Title                         of string

        static member CardStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("cardStyle", keyValueList CaseRules.LowerFirst x )
        static member GestureResponseDistance ( x : seq<ResponseDistance> ) = !!("gestureResponseDistance", keyValueList CaseRules.LowerFirst x )
        static member HeaderBackTitleStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("headerBackTitleStyle", keyValueList CaseRules.LowerFirst x )
        static member HeaderLeftContainerStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("headerLeftContainerStyle", keyValueList CaseRules.LowerFirst x )
        static member HeaderRightContainerStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("headerRightContainerStyle", keyValueList CaseRules.LowerFirst x )
        static member HeaderTitleContainerStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("headerTitleContainerStyle", keyValueList CaseRules.LowerFirst x )
        static member HeaderTitleStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("headerTitleStyle", keyValueList CaseRules.LowerFirst x )
        static member HeaderStyle ( x : seq<Fable.ReactNative.Props.IStyle> ) = !!("headerStyle", keyValueList CaseRules.LowerFirst x )    
        static member SafeAreaInsets ( x : seq<Inset> ) = !!("safeAreaInsets", keyValueList CaseRules.LowerFirst x )
        static member TransitionSpec ( x : seq<Transition> ) = !!("transitionSpec", keyValueList CaseRules.LowerFirst x )
    
    type ScreenProps =
        static member Options ( x : seq<ScreenOptions> ) = !!("options", keyValueList CaseRules.LowerFirst x )

    [<StringEnum>]
    type NavigatorMode = 
        | Card
        | Modal

    [<StringEnum>]
    type NavigatorHeaderMode = 
        | Float
        | Screen
        | None

    type NavigatorProps = 
        | InitialRouteName          of string      
        | KeyboardHandlingEnabled   of bool
        | Mode                      of NavigatorMode
        | HeaderMode                of NavigatorHeaderMode

        static member ScreenOptions ( x : seq<ScreenOptions> ) = !!("screenOptions", keyValueList CaseRules.LowerFirst x )

let inline navigateWithData ( navigation : Types.INavigation<_> ) ( endpoint : string ) ( data : 'a ) =
    navigation.navigation?navigate( endpoint, data )

let inline pushWithData ( navigation : Types.INavigation<_> ) ( endpoint : string ) ( data : 'a ) =
    navigation.navigation?push( endpoint, data )

let popMultipleScreens ( navigation : Types.INavigation<_> ) ( numScreens : int ) = 
    navigation.navigation?pop( numScreens )

let inline setOptions ( navigation : Types.INavigation<_> ) ( options : seq<Props.ScreenOptions> ) =
    let x = keyValueList CaseRules.LowerFirst options
    navigation.navigation.setOptions x

let navigationContainer props children = 
    ReactBindings.React.createElement( Types.Globals.NavigationContainer, keyValueList CaseRules.LowerFirst props, children )

let Stack = Types.CreateStackNavigator ()

let navigator ( props : seq<Props.NavigatorProps> ) children = 
    ReactBindings.React.createElement( Stack.Navigator, keyValueList CaseRules.LowerFirst props, children )

let screen name ( render : 'a ) ( props : seq<Props.ScreenProps> ) children =
    let screenProps = 
        let x = keyValueList CaseRules.LowerFirst props
        x?name          <- name
        x?``component`` <- render
        x

    ReactBindings.React.createElement( Stack.Screen, screenProps, children )






module Helpers = 
    type AppState = {
        render : unit -> Fable.React.ReactElement
        setState : AppState -> unit
    }

    let mutable appState = None

    type App(props) as this =
        inherit Component<obj,AppState>(props)
        do
            match appState with
            | Some state ->
                appState <- Some { state with AppState.setState = this.setInitState }
                this.setInitState state
            | _ -> failwith "App-state is not set. Remember to call registerApp."

        override this.componentDidMount() =
            appState <- Some { appState.Value with setState = fun s -> this.setState(fun _ _ -> s) }

        override this.componentWillUnmount() =
            appState <- Some { appState.Value with setState = ignore; render = this.state.render }

        override this.render () =
            this.state.render()

    [<Import("AppRegistry","react-native")>]
    type AppRegistry =
        static member registerComponent(appKey:string, getComponentFunc:unit->ReactElementType<_>) : unit =
            failwith "JS only"


    let registerApp appKey render = 
        AppRegistry.registerComponent(appKey, fun () -> unbox JsInterop.jsConstructor<App>)
        match appState with
        | Some state ->
            state.setState { state with render = fun () -> render }
        | _ ->
            appState <- Some { render = fun () -> render
                               setState = ignore }
