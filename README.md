# SPARK
**Traženje, rezervacija i plaćanje parking mjesta**

1. Lejla Brgulja
2. Kerim Jamaković
3. Hamza Zubača

## OPIS TEME
Spark je programsko rješenje koje će olakšati i ubrzati proces traženja, rezervisanja i plaćanja parkinga za korisnika, ali i olakšati vlasniku parkinga poslovanje istim. Neki od problema koje će naš sistem riješiti jesu pronalazak najbližeg slobodnog parkinga, uvid u to u koje zone spadaju parkinzi u blizini, da li postoji slobodno mjesto i koliko košta parking, rezervisanje mjesta kako bi korisnik sebi osigurao sigurno mjesto za parking. Razlog zbog kojeg bi neko kupio naš sistem jeste user-friendly pristup, odnosno kada bi postojala mreža ovakvih parkinga koji nude brzu i jednostavnu uslugu korisnicima, lakše bi bilo preusmjeriti veći broj korisnika upravo na ovakvu vrstu parkinga, gdje bi jednostavno, putem aplikacije, pronašli slobodan parking u blizini i izbjegli naplatne kućice jednostavnim sistemom za elektronsko plaćanje.

## PROCESI

#### REGISTRACIJA KORISNIKA
Korisnik upisuje svoje lične podatke, korisničko ime i lozinku, podatke potrebne za elektronsko plaćanje, uplaćuje početni iznos kredita na karticu i dobija informaciju o lokaciji i vremenu preuzimanja kartice.

#### REGISTRACIJA VLASNIKA
Vlasnik upisuje svoje lične podatke, korisničko ime, te lozinku, podatke o bankovnom računu kako bi se omogućio direktan transfer novca sa korisničkog na vlasnički račun, prilikom plaćanja parkinga.

#### REGISTRACIJA PARKINGA
Dodjeljuje se vlasnik, lokacija, broj raspoloživih mjesta, zona parkinga, cijena i radno vrijeme.  

#### DOPUNA KREDITA
Korisnik bira način elektronskog plaćanja (npr. Paypal, pikpay,...), vrši odabir koliko kredita želi kupiti, a zatim se sa njegovog računa skida odabrani iznos. Sa dopunjeim kreditima može koristiti sve parkinge unutar sistema. 

#### TRAŽENJE PARKINGA
Korisnik u aplikaciji otvara mapu na kojoj mu se prikazuju svi parkinzi u blizini. Klikom na neki od parkinga prikazuje mu se status parkinga (broj slobodnih mjesta), zona kojoj pripada te cijena. Odabirom parkinga korisniku se prikazuje put od lokacije na kojoj se nalazi do parkinga kako bi lakše došao do istog.

#### REZERVACIJA PARKINGA
Korisnik vrši odabir parkinga na kojem želi rezervisati mjesto, te odabirom od kada do kada želi rezervirati mjesto vrši rezervaciju istog. Samom potvrdom rezervacije automatski mu se skida obračunati broj kredita za taj određeni period kako bi se izbjegla zloupotreba ove opcije.

#### PLAĆANJE PARKINGA
Korisnik ulaskom na parking skenira svoju karticu, te se u bazi spašava vrijeme kada je on ušao na parking. Prilikom izlaska korisnik opet skenira svoju karticu, te sistem na osnovu vremena ulaska i izlaska obračunava količinu kredita koji će biti skinuti sa kartice.

#### UVID U FINANSIJSKO STANJE OD STRANE VLASNIKA
Vlasnik na svom računu, u obliku dnevnog izvještaja, raspolaže detaljnim informacijama o finansijskom stanju svakog od parkinga kojim posluje.

## FUNKCIONALNOSTI
- Mogućnost pravljenja korisničkog računa
- Mogućnost elektronske uplate na parking karticu
- Pronalazak parkinga na mapi
- Uvid u cijene 
- Provjera statusa parkinga (popunjen/slobodan)
- Mogućnost rezervacije mjesta na parkingu
- Mogućnost dolaska na parking i bez predviđene aplikacije / korisničkog računa uz upotrebu jednokratnih parking kartica/listića--
- Mogućnost uvida u poslovanje parkinga
- Uvid u ostvareni profit 
- Izbor jezika (en/bs)


## AKTERI
- Administrator, verifikuje korisničke račune i ima uvid u njih, ažurira aplikaciju.
- Vlasnik, osoba koja može kreirati parking profil i ima uvid u poslovanje istim.
- Korisnik, osoba koja ima mogućnost traženja, rezervacije i plaćanja parkinga preko korisničkog računa.


