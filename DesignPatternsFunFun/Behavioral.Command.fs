(* Encapsulate a request as an object, thereby letting you parameterize clients with different requests, 
   queue or log requests, and support undoable operations.*)

module Command = 
 
    [<Literal>]
    let DesignPatternName = "Command"

    type IReceiver = 
        abstract member Action : unit -> unit

    [<Sealed>]
    type ConcreteReceiver() =
        interface IReceiver with
            member __.Action() =
                printfn "%s: Calling Action from the Concrete Receiver." DesignPatternName

    type ICommand = 
        abstract member Receiver : IReceiver with get
        abstract member Execute  : unit -> unit 

    [<Sealed>]
    type ConcreteCommand( receiver : IReceiver ) =
        interface ICommand with
            member __.Receiver = receiver 
            member __.Execute() = 
                receiver.Action()
            
    [<Sealed>]
    type Invoker( command : ICommand ) = 
        let command = command
        member __.Execute() = 
            printfn "%s: Calling Execute from the Invoker." DesignPatternName
            command.Execute()
                
    let main() =
        let receiver : IReceiver = ConcreteReceiver() :> IReceiver 
        let command  : ICommand  = ConcreteCommand( receiver ) :> ICommand

        printfn "%s: Created the Concrete Receiver and established the command." DesignPatternName 

        let invoker = Invoker( command )
        printfn "%s: Created the Invoker." DesignPatternName 




        0
