(* Provide a surrogate or placeholder for another object to control access to it. *)

module Proxy = 

    [<Literal>]
    let DesignPatternName = "Proxy"

    type ISubject =
        abstract member Operate : unit -> unit

    [<Sealed>]
    type ConcreteSubject() = 
        interface ISubject with
            member __.Operate() =
                printfn "%s: Operate() from ConcreteSubject." DesignPatternName
    
    [<Sealed>]
    type SubjectProxy( subject : ISubject ) =
        let subject : ISubject = subject 
        interface ISubject with
            member __.Operate() =
                printfn "%s: Operate() from SubjectProxy." DesignPatternName
                subject.Operate()

    let main() =
        let subject : ISubject = ConcreteSubject() :> ISubject 
        let subjectProxy       = SubjectProxy( subject )
        ( subjectProxy :> ISubject ).Operate()

        0