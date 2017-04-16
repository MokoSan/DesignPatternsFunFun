(* Provide a unified interface to a set of interfaces in a subsystem. 
   Façade defines a higher-level interface that makes the subsystem easier to use. *)

module Facade = 

    [<Literal>]
    let DesignPatternName = "Facade"

    [<Sealed>]
    type ComponentA() = 
        member __.MethodA() =
            printfn "%s: MethodA() called." DesignPatternName

    [<Sealed>]
    type ComponentB() = 
        member __.MethodB() =
            printfn "%s: MethodB() called." DesignPatternName


    [<Sealed>]
    type ComponentC() = 
        member __.MethodC() =
            printfn "%s: MethodC() called." DesignPatternName

    [<Sealed>]
    type Facade() =
        let componentA = ComponentA()
        let componentB = ComponentB()
        let componentC = ComponentC()

        member __.MethodA() =
            componentA.MethodA()

        member __.MethodB() =
            componentB.MethodB()

        member __.MethodC() = 
            componentC.MethodC()

    let main() = 
        let facade = Facade()
        printfn "%s: Created Facade." DesignPatternName

        facade.MethodA()
        facade.MethodB()
        facade.MethodC()

        0