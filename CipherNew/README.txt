

        		--README.txt--


-------------------------------------------------------------------------------------------------------------------------

			--Setovanje baze za komunikaciju--

- Kao komunikacioni medijum se koristi postgres baza podataka.
- Naredbe za setovanje trigera koji će obaveštavati korisnike se nalaze u fajlu SQL.txt 
- Kao ORM je korišćen Entity Framework

- Baza je kreirana u okviru Docker kontejnera. Naredbe za kreiranje postgres kontejnera kao i za pristup kontejneru u okviru WSL-a:

docker pull postgres

docker run -d --name postgres2 -p 5432:5432 -e POSTGRES_PASSWORD=password postgres

psql -h localhost -U postgres -p 5432 -W

-------------------------------------------------------------------------------------------------------------------------

				--Upotreba--

- Data aplikacija je dizajnirana za peer to peer čet između 2 korisnika. 
- Pri pokretanju aplikacije korisnik unosi svoj username i klikom na dugme Connect sprema se za ulazak u čet. Pri 
kliku na dugne Connect, korisniku ne mora odmah da se otvori čet soba, već ako drugi korisnik nije konektovan, korisnik koji
se ulogovao će čekati drugog korisnika za deadline period (zadat unutar config fajla) da se uloguje. Tek kad i drugi korisnik upiše svoj Username, i prvi korisnik će dobiti prikaz čet sobe, kao i drugi korisnik (koji će odmah pri konektovanju dobiti prikaz čet sobe, jer nije morao da čeka nikog da se konektuje)
- U čet sobi, dole se unosi tekst, klikom Send se poruka šalje, a stikliranjem Encrypted polja bira se da li će prikaz poruka
biti enkriptovan izabranim algoritmom ili ne.


2. Faza

-Odabirom datog algoritma za enkriptovanje bira se algoritam enkripcije. Ovim algoritmom će dati program kriptovati poruke koje pošalje i dekriptovati nadolazeće poruke.

3. Faza

- Dodaje se opcija slanja fajlova i provere ispravnosti slanja pomoću MD5 heša. Klikom na dugme "Send file" korisnik bira fajl koji će poslati. 
- Dodata je opcija za case sensitive RailFence cipher, zato što je u prvoj fazi (kako je inače rađen RailFence algoritam) rađen RailFence koji je case insensitive.
- u Received Files nalaze se primljeni fajlovi i klikom opcije Download može da se doda fajl u neku od lokalnih datoteka u računaru korisnika.