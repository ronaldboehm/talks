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

' In F# ist es üblich Varianten von einem Wert mittels '' zu kennzeichnen
' Klammern sind nicht notwendig, dienen der Abgrenzung und Klarheit

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
let matchTuple c = 
    match c with
    | 0.0,0.0 -> "0.0,0.0"
    | 1.0,1.0 -> "0.0,0.0"
    | 1.0,2.0 -> "1.0,1.0"
    | _,_ -> "sonst"

let result   = matchTuple complex
let result'  = matchTuple complex'
let result'' = matchTuple (2.0,2.0) // Brauche jetzt Klammern!

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

' _ ist ein Platzhalter für egal welches Wert erscheint, bitte nicht evaluieren

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

(**



***
###  Record

Eine bennante Menge von benannten Elementen

- Müssen nicht vom gleichen Typ sein
- Die Reihenfolge der Deklaration ist nicht relevant
- Ist kein ad-hoc Typ

' Wichitg: es ist auch nur ein Tuple, ein multiplication type

---
### Record: Deklaration

*)

type ComplexNumber = { Real: float; Imaginary: float; }
type GeoCoord = { Lat: float; Long: float; }

(**
' Semikolon ist hier der Trenner

---
### Record: Construction
*)

let complexNumber = { Real = 1.0; Imaginary = 1.0; }
let hamburg       = { Lat = 53.553260805869805; Long = 9.993009567260742; }

(**
' Construction ist ähnlich wie die Deklaration, nur werden : durch = ersetzt
' F# ist in der Lage anhand der Namen der Member den Typ zu erkennen -> Typinferenz

---
### Record: Construction
Typ kann bei der Construction qualifiziert werden
*)

let complexNumber' = { ComplexNumber.Real = 2.0; Imaginary = 2.0; }
let hamburg'       = { GeoCoord.Lat = 53.553260805869805; Long = 9.993009567260742; }

(**
' Dies ist dann besonders hilfreich wenn Typen die gleichen Bezeichner verwenden

---
### Record: Construction
Keine halben Sachen: 

- Alle Werte müssen angegeben werden
- Kein Wert kann verändert werden nach der Construction

*)
// let complexNumber' = { ComplexNumber.Real = 1.0; } // Imaginary gebe ich später an
(**

' Funktionales Vorgehen: Wenn ich feststelle dass ich den Fall konkret habe dass ein Wert nicht immer angegeben werden kann, dann erstelle ich hierfür einen Typ

---
### Record: Deconstruction
Record in seinen Bestandteilen zerlegen

*)

let { Real = real;  Imaginary = imaginary; } = complexNumber // Alle Member
let { Real = real'; } = complexNumber' // Einzelne

(** <div style="display: none" > *)
(*** define-output:Record-Deconstruction ***)
printf "real = %f | imaginary = %f | real' = %f" real imaginary real'
(** </div> *)

(*** include-output: Record-Deconstruction ***)

(**

---
### Record: Clone With
Da modifizieren nicht geht, bietet F# verinfachtes Klonen an

*)

let complexNumber'' = { complexNumber' with Imaginary = 3.0 }

(** <div style="display: none" > *)
(*** define-output:Record-Clone ***)
let { Real = real''; Imaginary = imaginary''; } = complexNumber''
printf "real = %f | imaginary = %f" real'' imaginary'' 
(** </div> *)

(*** include-output: Record-Clone ***)

(**

'  Dies ist notwendig für den üblichen Fall dass sich nicht alle Werte geändert haben sondern nur bestimmte.  Mit der With Syntax kann ich diesen Fall gut abdecken

---
### Record: Strukturelle Gleichheit

Zwei Record Werte sind gleich wenn beide 

- Vom Gleichen Typ sind
- Alle korrespondierende Bezeichner haben die gleichen Werte

' ACHTUNG: Wenn zwei unterschiedliche Record Typen (unterschiedlilch bennante) die gleichen Bezeichner haben, sind beide trotzdem nicht über strukturelle Gleichheit vergleichbar.  In anderen funktionalen Sprachen wird die Gleicheit anders gehandhabt

---
### Record: Pattern Matching

*)

let matchRecord c = 
    match c with 
    | { Real = 1.0; Imaginary = 1.0; } -> "Real = 1.0 & Imaginary = 1.0"
    | { Real = 1.0 } -> "Real = 1.0"
    | { Imaginary = 2.0 } -> "Imaginary = 2.0"
    | _ -> "sonst"

let r    = matchRecord { Real = 1.0; Imaginary = 1.0; }
let r'   = matchRecord { Real = 1.0; Imaginary = 2.0; }
let r''  = matchRecord { Real = 2.0; Imaginary = 2.0; }
let r''' = matchRecord { Real = 3.0; Imaginary = 3.0; }

(** <div style="display: none" > *)
(*** define-output:Record-PatternMatching ***)
printf "r = %s | " r
printf "r' = %s" r'
(*** define-output:Record-PatternMatching0 ***)
printf "r'' = %s |" r''
printf "r''' = %s " r'''
(** </div> *)
(*** include-output: Record-PatternMatching ***)
(*** include-output: Record-PatternMatching0 ***)

(**

***
### Discriminated Union

- Unterscheidungs-Union auf deutsch
- Besteht aus einer Anzahl von benannten Fällen
- Ein benannter Fall kann aus einer Anzahl von Werten bestehen
- Nur eines der bennanten Fällt ist gültig für einen Ausdruck
- Mit Bezeichnern oder ohne

*)

type Shape =
| Viereck of width : float * length : float
| Kreis of radius : float

(**
' Ausdrucke sind immutable, ändert sich der Wert nicht.  Bitte nicht mit einem Variant (vb.net) verwechseln
' DU ist das ganze.  Die einzelnen möglichen Werte heißen Union Case
' Single Case gibt es auch und sind sehr schön im DDD


---
### DU: Deklaration

- Empty Case, ist nur ein Bezeichner, keine Daten. 
- Komposition: Record definieren und als union case verwenden

' Empty case kann sehr hilfreich sein, z.B. bei DDD

*)

type DuBeispiel =
| Leer
| Complex of ComplexNumber
| Coordinate of GeoCoord

(**

---
#### DU: Construction

Für jeden Union Case gibt es eine Constructor Funktion

*)

let viereck = Viereck(length = 1.3, width = 10.0) // ACHTUNG, Welchen Typ hat rect?
let kreis = Kreis (1.0)

(**

---
### DU: Deconstruction & Pattern Matching

- rect im Beispiel hatte den Type Shape, nicht Shape.Rectangle!
- Ich kann von außen nicht wissen welche union case ein DU-Wert darstellt
- Nur Pattern Matching erlaubt es mir dies zu erfahren
- Deconstruction *muss* für *alle* Fälle erfolgen (Exhaustivness)

' Wenn ich einen DU anspreche dann entweder um eine Transforation zu erhalten (Fläche Ermitteln) oder um 

---
### DU: Deconstruction & Pattern Matching

*)

let flaeche s = 
    match s with 
    | Viereck (w,l) -> w*l
    | Kreis(r) -> Math.PI*(r ** 2.0) 
    // einen union case auszulassen verursacht einen Compiler Fehler 

let kreisFlaeche   = flaeche (Kreis (5.0))
let viereckFlaeche = Viereck(length = 5.0, width = 5.0) |> flaeche


(** <div style="display: none" > *)
(*** define-output:DU-PatternMatching ***)
printf "Kreisfläche = %f | " kreisFlaeche
printf "Viereckfläche = %f" viereckFlaeche
(** </div> *)
(*** include-output: DU-PatternMatching ***)

(**

--- 
### DU: Nutzung

- DU für die Darstellung von Zuständen/Übergängen
- Alle Fälle abgedecken führt zu wengier Fehlerfälle
- Schnell erstellt!

---
### DU: Single Case

- Primitives haben oft besondere Bedeutung
- Höhengrad und Breitengrad sind beide floats
- Beide werte jedoch stellen einen anderen Domain dar

' In DDD (Domain Driven Design) spielen diese oft eine wichtige Rolle.  
' Z.B. kann ich dadurch Primitives so definieren dass diese untereinander nicht „kompatibel“ sind, auch wenn diese vom gleichen Typ sind.

*)

// Erste Longitude ist der Name des Typs
// Zweite ist der Name des Constructors
// Müssen nicht identisch sein!
type Longitude = Longitude of float 
type Latitude = Latitude of float

let longitude = Longitude(9.993009567260742)
let latitude  = Latitude(53.553260805869805)

// Der Compiler mag das nicht, es handelt sich um zwei Typen
// let gleich = longitude = latitude 






