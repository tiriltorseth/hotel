Bruk av AI i Arbeidskrav 2:

**Prompt:**
Hva er galt med denne? d står at den skal være abstrakt public abstract decimal GetDiscount(decimal basePrice) { return basePrice; }

**Svar:**
En abstract metode kan ikke ha implementasjon.

Du har skrevet:

        public abstract decimal GetDiscount(decimal basePrice)
        {
        return basePrice;
        }

Her gjør du to ting samtidig som ikke går:

abstract → betyr ingen kode

        { return basePrice; } → er kode

C# tillater ikke begge.

Riktig måte

En abstrakt metode skal bare deklareres, ikke implementeres.

Så i Guest skal det være:

public abstract decimal GetDiscount(decimal basePrice);

Legg merke til:

ingen {}

ingen return

Hvorfor?

Fordi subklassene må implementere metoden selv.

Du får da:

RegularGuest

        public override decimal GetDiscount(decimal basePrice)
        {
        return basePrice;
        }

Ingen rabatt.

**Prompt:** Hva er m i 0.85?

**Svar:** m betyr at tallet er en decimal literal i C#.

I C# har tall uten noe suffix som standard typen double.

Så:

0.85

er egentlig:

    double

Men metoden din bruker:

    decimal basePrice

Da må også tallet du ganger med være decimal, ellers får du type-konflikt.

Derfor skriver man
    
    0.85m

m betyr:

decimal
Eksempel

Dette gir feil:

    decimal price = basePrice * 0.85;

Feil fordi:

    decimal * double

ikke er lov.

Dette fungerer:

    decimal price = basePrice * 0.85m;

fordi det blir:

    decimal * decimal

**Prompt:** Feilsøk:         
    
    var showCardNumber = cardNumber.Substring(Length - 4, 4);

**Svar:**
Problemet er denne delen:

Length - 4

Length alene finnes ikke her. Length må tilhøre en string. Du må derfor bruke cardNumber.Length.

Riktig kode

    var showCardNumber = cardNumber.Substring(cardNumber.Length - 4, 4);
**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**







