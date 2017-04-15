(* Separate the construction of a complex object from its representation so that the same construction process can create different 
representations. *)

module Builder = 

    open System.Collections.Generic // needed for the Dictionary

    [<Literal>]
    let DesignPatternName = "Builder Pattern"

    [<AbstractClass>]
    type AbstractProduct() = 
        abstract member Name  : string                     with get
        abstract member Parts : Dictionary<string, string> with get 

    [<Sealed>]
    type ConcreteProduct(name : string) =
        inherit AbstractProduct()
        let parts : Dictionary<string, string> = Dictionary<string, string>() 
        override __.Name  = name 
        override __.Parts = Dictionary<string, string>() 

    [<AbstractClass>]
    type AbstractBuilder() = 
        abstract member BuildPartA     : unit -> unit
        abstract member BuildPartB     : unit -> unit
        abstract member ReleaseProduct : unit -> AbstractProduct 

    [<Sealed>]
    type ConcreteBuilder() = 
        inherit AbstractBuilder()
        let concreteProduct : AbstractProduct = ConcreteProduct("Concrete Product") :> AbstractProduct 
        override __.BuildPartA()     =
            concreteProduct.Parts.Add("Part A", "Part A")
        override __.BuildPartB()     =
            concreteProduct.Parts.Add("Part B", "Part B")
        override __.ReleaseProduct() =
            concreteProduct 

    type Director() =
        static member Construct(builder : AbstractBuilder) : AbstractProduct = 
            builder.BuildPartA() 
            printfn "%s: Director is Built Part A" DesignPatternName 
            builder.BuildPartB()
            printfn "%s: Director is Built Part B" DesignPatternName
            printfn "%s: Director is Releasing the Product" DesignPatternName
            builder.ReleaseProduct()

    let main() = 
        let abstractBuilder : AbstractBuilder = ConcreteBuilder()  :> AbstractBuilder 
        printfn "%s: Created the Abstract Builder." DesignPatternName
        let concreteProduct = Director.Construct( abstractBuilder )
        printfn "%s: Created the Product using the builder with name %s" DesignPatternName concreteProduct.Name 
        0