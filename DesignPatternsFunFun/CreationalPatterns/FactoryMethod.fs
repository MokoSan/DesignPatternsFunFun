(* Define an interface for creating an object, but let subclasses decide which class to instantiate. 
   Factory Method lets a class defer instantiation to subclasses.*)

module FactoryMethod = 

    [<Literal>]
    let DesignPatternName = "Factory Method"

    [<AbstractClass>]
    type AbstractProduct() = 
        abstract member Name : string with get

    [<Sealed>]
    type ConcreteProductA( name : string ) =
        inherit AbstractProduct()
        override __.Name = name

    [<Sealed>]
    type ConcreteProductB( name : string ) =
        inherit AbstractProduct()
        override __.Name = name 

    [<AbstractClass>]
    type AbstractProductCreator() = 
        abstract member Create : unit -> AbstractProduct

    [<Sealed>]
    type ConcreteProductACreator() = 
        inherit AbstractProductCreator()
        override __.Create() = 
            let concreteProductA = ConcreteProductA( "Product A" )
            concreteProductA :> AbstractProduct

    [<Sealed>]
    type ConcreteProductBCreator() = 
        inherit AbstractProductCreator()
        override __.Create() =
            let concreteProductB = ConcreteProductB( "Product B" )
            concreteProductB :> AbstractProduct

    let main() = 
        let productCreators : AbstractProductCreator[] = [| ConcreteProductACreator() :> AbstractProductCreator; 
                                                            ConcreteProductBCreator() :> AbstractProductCreator; |] 
        printfn "%s: Creating Products A and B." DesignPatternName 
        for productCreator in productCreators do
            let product = productCreator.Create()
            printfn "%s: Created Product - %s" DesignPatternName product.Name 

        0