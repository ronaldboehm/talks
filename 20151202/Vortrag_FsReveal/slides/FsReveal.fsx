(**
- title : F# Das Typsystem
- description : Einführung in das Typsystem von F#
- author : Nasser Brake
- theme : night
- transition : none

***
## F# 
## Das Typsystem
 
#### Nasser Brake
#### http://www.nasserbrake.de
#### https://www.github.com/nasserbrake

***
### Tuple

Eine Menge von Elementen 

- Müssen nicht vom gleichen Typ sein
- Die Reihenfolge ist entscheidend
- Ist ein ad-hoc Typ: keine benannter

' Auch wenn ich einen Typ definieren kann, kann ich einzelne Werte diesen Typen nicht zuordnen.  Die einzige Zuordnung ist den Bestandteilen und deren Reihenfolge

---
### Tuple: Construction

- Kann beliebig viele Elemente beinhalten
- Construction erolgt mittels Komma (mit und ohne Klammern)
- Multiplikation von (mindestens) zwei Domains: Kartesiche Summe

*)

type IntTuple = int * int // Jeder int 'mal' jeder int
let intTuple  = 1,1

type IntStringTuple = int * string // Jeder int 'mal' jeder string
let intStringTuple  = 1,"string"

type TripleIntTuple = int * int * int
let tripleIntTuple = 1,2,3

(**

---
### Tuple: Komposition

Aus einfachen Typen lassen sich komplexere erstellen

*)

type Complex = float * float
let complex  = 1.0,1.0

type Komposition = IntStringTuple * Complex
let komposition  = intStringTuple,complex

(**

---
### Tuple: Deconstruction
Ein Tuple kann in seinen Bestandteilen zerlegt werden

*)
let complex' = 1.0,2.0
let c',c'' = complex'
let komposition' = intStringTuple,complex'
let k',k'' = komposition'

(** <div style="display: none" > *)
(*** define-output:complex ***)
printf "c' = %A | c'' = %A" c' c''
(*** define-output:komposition ***)
printf "k' = %A | k'' = %A" k' k''
(** </div> *)

(*** include-output: complex ***)
(*** include-output: komposition ***)

(**
' In F# ist es üblich Varianten von einem Wert mittels '' zu kennzeichnen
' Klammern sind nicht notwendig, dienen der Abgrenzung und Klarheit

---
### Tuple: Strukturelle Gleichheit

- Typ und Reihnfolge: sind Zwei Werte vergelichbar?
- Werte: sind zwei Werte gleich?

*)
let equal = complex' = complex

(** <div style="display: none" > *)
(*** define-output:Type-Equality ***)
printf "Gleich = %b" equal
(** </div> *)

(*** include-output: Type-Equality ***)

(**
' Typ Definition ist hilfreich bei Signaturen

---
### Tuple: Pattern Matching

*)
let matchComplex c = 
    match c with
    | 0.0,0.0 -> "0.0,0.0"
    | 1.0,1.0 -> "0.0,0.0"
    | 1.0,2.0 -> "1.0,1.0"
    | _,_ -> "sonst"

let result   = matchComplex complex
let result'  = matchComplex complex'
let result'' = matchComplex (2.0,2.0) // Brauche jetzt Klammern!

(** <div style="display: none" > *)
(*** define-output:Tuple-PatternMatching ***)
printf "result = %A | " result 
printf "result' = %A | " result'
printf "result'' = %A " result''
(** </div> *)
(*** include-output: Tuple-PatternMatching ***)

(**
' Fehlnde Fälle werden vom Compiler festgestellt und angezeigt

---
### Tuple: Nutzung in der .net API

TryParse Methoden die zwei Werte zurückgeben
- bool: War das Parsen erfolgreich?
- Wert: falls das Parsen erfolgreich war

*)

open System
let showParseResult result = 
    match result with
    | true,value -> sprintf "Value parsed is %s" (value.ToString())
    | false,_ -> "Value couldn't be parsed" 

let tryParseResult = Int32.TryParse "Keine Zahl" |> showParseResult
let tryParseResult' = Int32.TryParse "1" |> showParseResult


(** <div style="display: none" > *)
(*** define-output:Tuple-TryParsePatternMatching ***)
printf "result = %A | " tryParseResult 
printf "result' = %A" tryParseResult'
(** </div> *)

(*** include-output: Tuple-TryParsePatternMatching ***)



