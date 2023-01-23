using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TransConnect
{
    class Program
    {
        #region Fonction de base

        #region Séparateur
        public static void Separateur(int n = 30)
        {
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n");
        }
        public static void SeparateurMessage(string m, int n = 10)
        {
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("-");
            }
            Console.Write(m);
            for (int i = 0; i < n; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n");
        }
        #endregion

        #region Saisie sécurisée
        public static int SaisiePositif(int max, int min = 1, string message = "Veuillez saisir votre choix : ")
        {
            int choix = 0;
            choix = SaisieEntiere(message);
            while (choix < min || choix > max)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }
            return choix;
        }
        static int SaisieEntiere(string message)
        {
            int i = 0;
            Console.Write(message);
            bool erreur = true;
            while (erreur)
            {
                try
                {
                    i = int.Parse(Console.ReadLine());
                    erreur = false;
                }
                catch
                {
                    Console.WriteLine("Erreur : La saisie n'est pas entière");
                    Console.Write("Veuillez réessayer : ");
                }
            }
            return i;
        }
        static double SaisieRationnelle(string message)
        {
            double d = 0;
            Console.Write(message);
            bool erreur = true;
            while (erreur)
            {
                try
                {
                    d = Convert.ToDouble(Console.ReadLine());
                    erreur = false;
                }
                catch
                {
                    Console.WriteLine("Erreur : La saisie n'est pas nombre réel");
                    Console.Write("Veuillez réessayer : ");
                }
            }
            return d;
        }
        public static string SaisieString(string message = "Veuillez saisire une chaine de charactère : ")
        {
            Console.Write(message);
            string s = Console.ReadLine();
            if (s.Length != 0)
            {
                foreach(char c in s)
                {
                    if(c==';' || c == '|')
                    {
                        Console.WriteLine("Erreur : Vous avez entré des symboles non autorisés");
                        return SaisieString(message);
                    }
                }
            }
            return s;
        }
        public static string SaisieNom(string message = "Veuillez saisir le nom : ")
        {
            string s = SaisieString(message);
            if (s.Length > 1)
            {
                s = s[0].ToString().ToUpper() + s.Substring(1).ToLower();
            }
            else
            {
                Console.WriteLine("Erreur : votre saisie n'a pas été compris");
                return SaisieNom(message);
            }
            return s;
        }
        public static DateTime SaisieDateTime(string message)
        {
            DateTime naissance = new DateTime();
            bool erreur = true;
            while (erreur)
            {
                try
                {
                    Console.Write(message);
                    naissance = Convert.ToDateTime(Console.ReadLine());
                    erreur = false;
                }
                catch
                {
                    Console.WriteLine("Erreur : La date n'a pas été saisi au bon format, veuillez réessayer");
                }
            }
            return naissance;
        }
        static string SaisieTel(string message)
        {
            string tel = SaisieString(message);
            while (tel.Length != 10)
            {
                Console.Write("Erreur, la saisie ne correspond pas au format d'un numéro, veuillez réessayer : ");
                tel = Console.ReadLine();
            }
            try
            {
                Convert.ToInt32(tel);
            }
            catch
            {
                Console.WriteLine("Erreur : La saisie ne correspond pas au format d'un numéro");
                return SaisieTel(message);
            }
            return tel;
        }
        static int Id(Entreprise E, string message)
        {
            int i = SaisieEntiere(message);
            if (E.TrouverSalarieId(i) == null)
            {
                return i;
            }
            else
            {
                Console.WriteLine("Cette identifient existe déjà, veuillez réessayer");
                return Id(E, message);
            }
        }
        static bool SaisieBool(string message)
        {
            bool saisie = false;
            Console.WriteLine(message);
            Console.WriteLine("\t0 : Non");
            Console.WriteLine("\t1 : Oui");
            int n = SaisiePositif(1, 0);
            if (n == 1)
            {
                saisie = true;
            }
            return saisie;
        }

        #endregion

        #region Graphe

        #region Initialisation
        public static List<Sommet> LectureSommet(string fichier = "villes.txt")
        {
            List<Sommet> sommets = new List<Sommet>();
            StreamReader lecture = new StreamReader(fichier);
            while (lecture.Peek() > 0)
            {
                string[] tem = lecture.ReadLine().Split(";");
                sommets.Add(new Sommet(new Ville(tem[0])));
            }
            lecture.Close();
            return sommets;
        }
        public static List<Arc> LectureArc(string fichier = "arcs.txt")
        {
            List<Arc> arcs = new List<Arc>();
            StreamReader lecture = new StreamReader(fichier);
            while (lecture.Peek() > 0)
            {
                string[] tem = lecture.ReadLine().Split(";");
                arcs.Add(new Arc(new Sommet(new Ville(tem[0])), new Sommet(new Ville(tem[1])), Convert.ToDouble(tem[2])));
            }
            lecture.Close();
            return arcs;
        }

        #endregion

        #region Ville

        #region Saisie
        static string SaisieStringVille(string message)
        {
            string v = "";
            bool erreur = true;
            while (erreur)
            {
                try
                {
                    string s = SaisieString(message);
                    v = s[0].ToString().ToUpper() + s.Substring(1).ToLower();
                    erreur = false;
                }
                catch
                {
                    Console.WriteLine("Erreur : Veuillez ne pas saisir de caractères spéciaux");
                    return SaisieStringVille(message);
                }
            }
            return v;
        }
        static Ville SaisieNewVille(Entreprise E, string message = "Veuillez saisir le nom d'une ville : ")
        {
            string v = SaisieStringVille(message);
            Ville ville = E.TrouverVille(v);

            if (ville != null)
            {
                Console.WriteLine("Cette ville existe déjà, veillez réessayer");
                return SaisieNewVille(E, message);
            }
            return new Ville(v);
        }
        static Ville SaisieVilleExistant(Entreprise E, string message = "Veuillez saisir le nom d'une ville : ")
        {
            string v = SaisieStringVille(message);
            Ville ville = E.TrouverVille(v);

            if (ville == null)
            {
                Console.WriteLine("Cette ville n'existe pas, veillez réessayer");
                return SaisieVilleExistant(E, message);
            }
            return ville;
        }
        public static Ville DemanderVille(Entreprise E, string message = "Veuillez saisir la ville du salarié : ")
        {
            string s = SaisieStringVille(message);
            Ville ville = E.Villes.Find(v => v.Villes == s);
            if (ville != null)
            {
                return ville;
            }
            Console.WriteLine("Cette ville n'est pas connu de nos service");
            Console.WriteLine("Voulez vous l'ajouter à la base de donnée : ");
            Console.WriteLine("\tTapez 0 : Oui, je veux ajouter " + s + " en tant que ville");
            Console.WriteLine("\tTapez 1 : Non, je veux saisir à nouveau une ville");
            int i = SaisiePositif(1, 0);
            if (i == 0)
            {
                ville = new Ville(s);
                E.AjouterVille(ville);
                Sommet sommet = new Sommet(ville);
                E.Graphes.Sommets.Add(new Sommet(ville));
                RelierVille(E, ville);
            }
            else
            {
                ville = DemanderVille(E, message);
            }
            return ville;
        }
        #endregion

        #region Ajout
        static void CreerVille(Entreprise E)
        {
            Ville ville = SaisieNewVille(E);
            E.AjouterVille(ville);
            E.Graphes.Sommets.Add(new Sommet(ville));
            RelierVille(E, ville);
        }
        #endregion

        #region Afficher distance
        static void AfficherDistance(Entreprise E)
        {
            Ville ville = SaisieVilleExistant(E);
            Graphe itineraire = new Graphe(LectureSommet(), LectureArc());
            Sommet s = itineraire.Sommets.Find(c => c.VIlle.Villes == ville.Villes);
            itineraire.Dijkstra(s);
            itineraire.Afficher();

        }
        #endregion

        #endregion

        #region Arc

        #region Saisie
        static void RelierVille(Entreprise E, Ville ville)
        {
            Console.WriteLine("Voulez vous relier cette ville avec d'autre (0:Non|1:Oui) : ");
            int n = SaisiePositif(1, 0);
            while (n == 1)
            {
                Console.WriteLine("Voulez vous la relier avec une ville de départ ou d'arrivée (0:départ|1:arrivée) : ");
                int n2 = SaisiePositif(1, 0);
                Sommet s1;
                Sommet s2;
                if (n2 == 0)
                {
                    Ville newVille = SaisieVilleExistant(E, "Veuillez saisir la ville de départ : ");
                    while (ville.Villes == newVille.Villes)
                    {
                        Console.WriteLine("Erreur : Vous avez saisie les mêmes villes, veuillez réessayer");
                        newVille = SaisieVilleExistant(E, "Veuillez saisir à nouveau la ville de départ : ");
                    }
                    s1 = E.Graphes.Sommets.Find(c => c.VIlle.Villes == newVille.Villes);
                    s2 = E.Graphes.Sommets.Find(c => c.VIlle.Villes == ville.Villes);
                }
                else
                {
                    Ville newVille = SaisieVilleExistant(E, "Veuillez saisir la ville d'arrivée : ");
                    while (ville.Villes == newVille.Villes)
                    {
                        Console.WriteLine("Erreur : Vous avez saisie les mêmes villes, veuillez réessayer");
                        newVille = SaisieVilleExistant(E, "Veuillez saisir à nouveau la ville d'arrivée : ");
                    }
                    s1 = E.Graphes.Sommets.Find(c => c.VIlle.Villes == ville.Villes);
                    s2 = E.Graphes.Sommets.Find(c => c.VIlle.Villes == newVille.Villes);
                }
                AjouterNewArc(E, s1, s2);

                Console.WriteLine("Voulez vous relier cette ville ("+ville.Villes+") avec d'autre (0:Non|1:Oui) : ");
                n = SaisiePositif(1, 0);
            }
        }

        static void CreerArc(Entreprise E)
        {
            Ville ville = SaisieVilleExistant(E);
            RelierVille(E, ville);
        }
        #endregion

        #region Ajout

        // Lorsqu'on crée une nouvelle ville, on doit pouvoir la relier avec les autres
        static bool AjouterNewArc(Entreprise E, Sommet depart = null, Sommet arrive = null)
        {
            bool ajout = false;

            if (depart == null)
            {
                depart = E.Graphes.Sommets.Find(c => c.VIlle.Villes == SaisieVilleExistant(E,"Veuillez saisir la ville de départ : ").Villes);
            }

            if (arrive == null)
            {
                arrive = E.Graphes.Sommets.Find(c => c.VIlle.Villes == SaisieVilleExistant(E,"Veuillez saisir la ville d'arrivée : ").Villes);
            }


            if (depart.VIlle.Villes == arrive.VIlle.Villes)
            {
                Console.WriteLine("Erreur : Vous avez saisie les mêmes villes");
                return false;
            }


            if (E.Graphes.Sommets!=null)
            {
                if(E.Graphes.Arcs.Find(c=>(c.Depart.Equals(depart) && c.Arrivee.Equals(arrive))) != null)
                {
                    Console.WriteLine("Ce chemin existe déjà, voulez vous la redéfinir ?");
                    Console.WriteLine("\tTapez 0 : Non, je veux abandonner ma saisie");
                    Console.WriteLine("\tTapez 1 : Oui, je veux redéfinir une nouvelle distance entre " + depart.VIlle.Villes + " et " + arrive.VIlle.Villes); ;
                    
                    int n = SaisiePositif(1, 0);
                    if (n == 0)
                    {
                        return false;
                    }
                    else
                    {
                        Arc a = E.Graphes.Arcs.Find(c => (c.Depart.Equals(depart) && c.Arrivee.Equals(arrive)));
                        int poids = SaisieEntiere("Veuillez saisir la nouvelle distance entre " + depart.VIlle.Villes + " et " + arrive.VIlle.Villes + "\n(Sachant que l'ancienne distance était de "+ a.LongueurArc +") : ");
                        a.LongueurArc = poids;
                        ajout = true;
                    }
                }
                else
                {
                    int longueur = SaisieEntiere("Veuillez saisir la distance entre " + depart.VIlle.Villes + " et " + arrive.VIlle.Villes + " : ");
                    E.Graphes.Arcs.Add(new Arc(depart, arrive, longueur));
                    ajout = true;
                }
            }
            else
            {
                E.Graphes.Sommets = new List<Sommet>();
                int longueur = SaisieEntiere("Veuillez saisir la distance entre " + depart.VIlle.Villes + " et " + arrive.VIlle.Villes + " : ");
                E.Graphes.Arcs.Add(new Arc(depart, arrive, longueur));
                ajout = true;
            }
            

            return ajout;
        }

        #endregion

        #endregion

        #region Sauvegarde
        static void SaveArcs(List<Arc> arcs, string fichier = "arcs.txt")
        {
            if (arcs != null && arcs.Count != 0)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach (Arc a in arcs)
                {
                    ecriture.WriteLine(a.ToString());
                }
                ecriture.Close();
            }
        }
        #endregion

        #endregion

        #endregion

        #region Entreprise - Salariés

        static Entreprise Creer()
        {
            Entreprise TransConnect = new Entreprise();
            if (TransConnect.Initialiser())
            {
                Thread.Sleep(1500);
                Console.WriteLine("\nInitialisation effectuée avec succès");
            }
            else
            {
                Thread.Sleep(1500);
                Console.WriteLine("\nEchec de l'initialisation");
            }
            
            return TransConnect;
        }


        #endregion

        #region Partie 1 : Salariés

        #region Recrutter
        public static void Recrutter(Entreprise E)
        {
            Console.WriteLine("\t2. Recrutter un salarié\n");
            string nom = SaisieNom("Veuillez saisir le nom du salarié : ");
            string prenom = SaisieNom("Veuillez saisir le prénom du salarié : ");
            int numSS = Id(E, "Veuillez saisir le numéro de sécurité social du salarié : ");
            DateTime naissance = SaisieDateTime("Veuillez saisir la date de naissance du salarié (au format : jj/mm/aaaa) : ");

            Ville ville = DemanderVille(E, "Veuillez saisir la ville du salarié : ");
            string email = SaisieString("Veuillez saisir l'email du salarié : ");
            string tel = SaisieTel("Veuillez saisir le numéro de téléphone du salarié : ");
            Tposte poste = MenuPoste();
            double salaire = SaisieRationnelle("Veuillez saisir le salaire du salarié : ");

            Salarie s = null;
            if (poste == Tposte.Chauffeur)
            {
                double tarif = SaisieRationnelle("Veuillez saisir le tarif du chauffeur : ");
                s = new Chauffeur(numSS, nom, prenom, naissance, ville, email, tel, new DateTime(), poste, salaire, null, null, 0, tarif, null);
            }
            else
            {
                s = new Salarie(numSS, nom, prenom, naissance, ville, email, tel, new DateTime(), poste, salaire);
            }

            if (E.Recrutter(s))
            {
                Console.WriteLine("\nCe salarié a bien été recrutté");
                Console.Write("Voulez vous le rajouter à l'organigramme (0:Non ; 1:Oui) : ");
                int choix = -1;
                choix = Program.SaisiePositif(1, 0);
                if (choix == 1)
                {
                    E.AjouterOrga(s);
                }

            }
            else
            {
                Console.WriteLine("\nErreur : cette personne n'existe pas");
            }
        }
        #endregion

        #region Modifier Poste
        static void ModifierPoste(Entreprise E)
        {
            string nom = SaisieNom("Veuillez saisir le nom du salarié : ");
            string prenom = SaisieNom("Veuillez saisir le prénom du salarié : ");
            Salarie s = E.TrouverSalarie(nom, prenom);
            if (s != null)
            {
                Console.WriteLine("\nCe salarié est actuellement au poste : " + s.Poste);
                Tposte newPoste = MenuPoste("Veuillez choisir son nouveau poste : ");
                s.Poste = newPoste;
                if (newPoste == Tposte.Chauffeur)
                {
                    double tarif = SaisieRationnelle("Veuillez saisir son tarif à l'heure : ");
                    Chauffeur c = new Chauffeur(s.Id, s.Nom, s.Prenom, s.Naissance, s.VILLE, s.Email, s.Tel, s.Entree, Tposte.Chauffeur, s.Salaire, s.Successeur, s.Frere, s.Hauteur, tarif);
                    E.Salaries.Remove(s);
                    E.Salaries.Add(c);
                    E.ExclureOrga(c);
                    E.AjouterOrga(c);
                }
                E.ExclureOrga(s);
                E.AjouterOrga(s);

                Console.WriteLine("\nLe poste du salarié a bien été modifier");
            }
            else
            {
                Console.WriteLine("\nCe salarié n'existe pas");
            }
        }
        #endregion

        #region Modifier Salaire
        static void ModifierSalaire(Entreprise E)
        {
            string nom = SaisieNom("Veuillez saisir le nom du salarié : ");
            string prenom = SaisieNom("Veuillez saisir le prénom du salarié : ");
            Salarie s = E.TrouverSalarie(nom, prenom);
            if (s != null)
            {
                Console.WriteLine("\nCe salarié a actuellement un salaire de : " + s.Salaire);
                double newSalaire = SaisieRationnelle("Veuillez saisir son nouveau salaire : ");
                s.Salaire = newSalaire;
                Console.WriteLine("\nLe salaire du salarié a bien été modifier");
            }
            else
            {
                Console.WriteLine("\nCe salarié n'existe pas");
            }
        }
        #endregion

        #region Ajouter Salarie Organigramme
        static void AjouterSalarieOrganigramme(Entreprise E)
        {
            string nomsalarie = SaisieNom("Quel est le nom du salarié : ");
            string prenomsalarie = SaisieNom("Quel est le prénom du salarié : ");
            Salarie employe = E.TrouverSalarie(nomsalarie, prenomsalarie);
            if (employe != null)
            {
                E.AjouterOrga(employe);
                Console.WriteLine("Opération effectuée");
            }
            else
            {
                Console.WriteLine("Erreur : ce salarié n'existe pas");
            }
            

        }
        #endregion

        #region Exclure Salarie Organigramme
        static void ExclureSalarieOrganigramme(Entreprise E)
        {
            string nomsalarie = SaisieNom("Quel est le nom du salarié : ");
            string prenomsalarie = SaisieNom("Quel est le prénom du salarié : ");
            Salarie employe = E.TrouverSalarie(nomsalarie, prenomsalarie);
            if (employe != null)
            {
                E.ExclureOrga(employe);
                Console.WriteLine("Opération effectuée");
            }
            else
            {
                Console.WriteLine("Erreur : ce salarié n'existe pas");
            }
        }
        #endregion
        #endregion

        #region Partie 2 : Clients
        static void AjouterClient(Entreprise E, string nom="", string prenom = "")
        {
            if (nom == "")
            {
                nom = SaisieNom("Veuillez saisir le nom du client : ");
            }
            if (prenom == "")
            {
                prenom = SaisieNom("Veuillez saisir le prénom du client : ");
            }
            int numSS = Id(E, "Veuillez saisir le numéro de sécurité social du client : ");
            DateTime naissance = SaisieDateTime("Veuillez saisir sa date de naissance (format jj/mm/aaaa) : ");
            Ville ville = DemanderVille(E, "Veuillez saisir la ville du client : ");
            string email = SaisieString("Veuillez saisir son email : ");
            string tel = SaisieTel("Veuillez saisir son numéro de téléphone : ");

            Client c = new Client(numSS, nom, prenom, naissance, ville, email, tel);
            if (E.AjouterClient(c))
            {
                Console.WriteLine("\nCe client a bien été rajouté à la liste des clients");
            }
            else
            {
                Console.WriteLine("\nErreur : Ce client n'a pas pu être rajouté à la liste des clients");
            }
        }

        static Client SaisieClient(Entreprise E)
        {
            string nom = SaisieNom("Veuillez saisir le nom du client : ");
            string prenom = SaisieNom("Veuillez saisir le prénom du client : ");

            Client c = E.Clients.Find(c => (c.Nom == nom && c.Prenom == prenom));
            return c;
        }
        #endregion

        #region Partie 3 : Véhicules

        #region Saisie
        static int SaisieTypeVehicule(bool choix = false)
        {
            int i = 0;
            Console.WriteLine("Veuillez choisir le type du véhicule");
            if (choix)
            {
                Console.WriteLine("\tTapez 0 : Générale (tous les véhicules)");
            }
            Console.WriteLine("\tTapez 1 : Voiture");
            Console.WriteLine("\tTapez 2 : Camionnette");
            Console.WriteLine("\tTapez 3 : Camion-citerne");
            Console.WriteLine("\tTapez 4 : Camion-benne");
            Console.WriteLine("\tTapez 5 : Camion-frigorifique");
            if (choix)
            {
                i = SaisiePositif(5, 0);
            }
            else
            {
                i = SaisiePositif(5, 1);
            }
            return i;
        }
        static int SaisieImmatriculation(Entreprise E)
        {
            int n = SaisieEntiere("Veuillez saisir l'immatriculation du véhicule : ");
            while (E.Vehicules.Find(c => c.Id == n) != null)
            {
                Console.WriteLine("Erreur : cette immatriculation existe déjà");
                n = SaisieEntiere("Veuillez réessayer : ");
            }
            return n;
        }
        static Camionnette SaisieCamionnette(Entreprise E)
        {
            return new Camionnette(SaisieImmatriculation(E), SaisieRationnelle("Veuillez saisir son tarif de location : "), SaisieRationnelle("Veuillez saisir sa vitesse : "), null, SaisieString("Veuillez saisir l'usage du véhicule : "));
        }
        static Voiture SaisieVoiture(Entreprise E)
        {
            return new Voiture(SaisieImmatriculation(E), SaisieRationnelle("Veuillez saisir son tarif de location : "), SaisieRationnelle("Veuillez saisir sa vitesse : "), null, SaisiePositif(4, 1, "Veuillez saisir le nombre maximum de passagers : "));
        }
        static Camion_citerne SaisieCamionCiterne(Entreprise E)
        {
            return new Camion_citerne(SaisieImmatriculation(E), SaisieRationnelle("Veuillez saisir son tarif de location : "), SaisieRationnelle("Veuillez saisir sa vitesse : "), null, SaisieString("Veuillez saisir les matériaux transportés : "), SaisieRationnelle("Veuillez saisir le volume maximum transportés : "), SaisieString("Veuillez saisir le type de cuve utilisé : "));
        }
        static Camion_benne SaisieCamionBenne(Entreprise E)
        {
            return new Camion_benne(SaisieImmatriculation(E), SaisieRationnelle("Veuillez saisir son tarif de location : "), SaisieRationnelle("Veuillez saisir sa vitesse : "), null, SaisieString("Veuillez saisir les matériaux transportés : "), SaisieRationnelle("Veuillez saisir le volume maximum transportés : "), SaisiePositif(3, 1, "Nombre de bennes : "), SaisieBool("Le camion possède t-il une grue auxilière : "));
        }
        static Camion_frigorifique SaisieCamionFrigorifique(Entreprise E)
        {
            return new Camion_frigorifique(SaisieImmatriculation(E), SaisieRationnelle("Veuillez saisir son tarif de location : "), SaisieRationnelle("Veuillez saisir sa vitesse : "), null, SaisieString("Veuillez saisir les matériaux transportés : "), SaisieRationnelle("Veuillez saisir le volume maximum transportés : "), SaisiePositif(10, 1, "Nombre de groupes électrogènes : "));
        }
        static Vehicule SaisieVehicule(Entreprise E)
        {
            Vehicule v = null;
            int n = SaisieTypeVehicule();
            switch (n)
            {
                case 1:
                    v = SaisieVoiture(E);
                    break;
                case 2:
                    v = SaisieCamionnette(E);
                    break;
                case 3:
                    v = SaisieCamionCiterne(E);
                    break;
                case 4:
                    v = SaisieCamionBenne(E);
                    break;
                case 5:
                    v = SaisieCamionFrigorifique(E);
                    break;
            }
            return v;
        }

        #endregion

        #region Ajout/Suppression
        static void AjouterVehicule(Entreprise E)
        {
            Vehicule v = SaisieVehicule(E);
            if (E.AjouterVehicule(v))
            {
                Console.WriteLine("Ce véhicule a bien été ajouté");
            }
            else
            {
                Console.WriteLine("Ce véhicule n'a pas pu être ajouté");
            }
        }

        static void SupprimerVehicule(Entreprise E)
        {
            int n = SaisieEntiere("Veuillez saisir l'immatriculation du véhicule : ");
            Vehicule v = E.Vehicules.Find(c => c.Id == n);
            if (v != null)
            {
                Console.WriteLine("Voici les détails de ce véhicule");
                v.Affiche();
                if (SaisieBool("Etes vous sûr de vouloir retirer ce véhicule de l'entreprise ?"))
                {
                    if (E.EnleverVehicule(v))
                    {
                        Console.WriteLine("Ce véhicule a bien été retiré de l'entreprise");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ce véhicule n'existe pas");
            }
        }
        #endregion

        #region Chauffeur
        static void AjouterChauffeur(Entreprise E)
        {
            string nom = SaisieNom("Veuillez saisir le nom du chauffeur : ");
            string prenom = SaisieNom("Veuillez saisir son prénom : ");
            int id = Id(E, "Veuillez saisir son numéro de sécurité social : ");
            Chauffeur c = new Chauffeur(id, nom, prenom, SaisieDateTime("Veuillez saisir sa date de naissance : "), DemanderVille(E, "Veuillez saisir sa ville de résidence : "), SaisieString("Veuillez saisir son e-mail : "), SaisieTel("Veuillez saisir son numéro de téléphone : "), DateTime.Now, Tposte.Chauffeur, SaisieRationnelle("Veuillez saisir son salaire : "));
            E.Salaries.Add(c);
        }
        static void SupprimerChauffeur(Entreprise E)
        {
            int id = SaisieEntiere("Veuillez saisir le numéro de sécurité social du chauffeur : ");
            Chauffeur c = E.Salaries.Find(c => c.Id == id && c.Poste == Tposte.Chauffeur) as Chauffeur;
            if (c != null)
            {
                Console.WriteLine("Voici les informations générales de ce chauffeur");
                c.Afficher(E);
                if(SaisieBool("Etes vous sûr de vouloir le licencier ?"))
                {
                    E.Salaries.Remove(c);
                }
            }
            else
            {
                Console.WriteLine("Erreur : Ce chauffeur n'existe pas");
            }
        }

        #endregion

        #region Affichage
        static void AfficherVehicule(Entreprise E)
        {
            Console.WriteLine("Voulez vous un affichage général de tout les véhicule ou un affichage trié en fonction du type de véhicule ?");
            int n = SaisieTypeVehicule(true);
            switch (n)
            {
                case 1:
                    Console.Clear();
                    SeparateurMessage("Affichage des voitures");
                    E.Vehicules.FindAll(v=>v.Type== "Voiture").ForEach(v => v.Afficher());
                    break;
                case 2:
                    Console.Clear();
                    SeparateurMessage("Affichage des camionnettes");
                    E.Vehicules.FindAll(v => v.Type == "Camionnette").ForEach(v => v.Afficher());
                    break;
                case 3:
                    Console.Clear();
                    SeparateurMessage("Affichage des camions citernes");
                    E.Vehicules.FindAll(v => v.Type == "Camion-citerne").ForEach(v => v.Afficher());
                    break;
                case 4:
                    Console.Clear();
                    SeparateurMessage("Affichage des camions bennes");
                    E.Vehicules.FindAll(v => v.Type == "Camion-benne").ForEach(v => v.Afficher());
                    break;
                case 5:
                    Console.Clear();
                    SeparateurMessage("Affichage des camions frigorifiques");
                    E.Vehicules.FindAll(v => v.Type == "Camion-frigorifique").ForEach(v => v.Afficher());
                    break;
                default:
                    Console.WriteLine("Affichage générale");
                    E.Vehicules.ForEach(v => v.Afficher());
                    break;
            }
                
            
        }
        #endregion

        #endregion

        #region Partie 4 : Commande

        #region Choix Livraison => véhicule + chauffeur
        static Vehicule ChoixVehicule(Entreprise E, Tlivraison livraison, DateTime date)
        {
            Vehicule vChoix = null;
            List<Vehicule> v = E.Vehicules.FindAll(c => (c.Livraison == livraison && c.EstLibre(date)));
            if(v!=null && v.Count != 0)
            {
                Console.WriteLine("Voici la liste des véhicule disponible le " + date.ToString("dd/mm")+" : ");
                foreach(Vehicule V in v)
                {
                    V.Affiche();
                }
                int immat = SaisieEntiere("Veuillez saisir l'immatriculation du véhicule (ou tapez \"-1\" pour abandonner) : ");
                if (immat == -1)
                {
                    return null;
                }
                vChoix = v.Find(c => c.Id == immat);
                while (vChoix == null)
                {
                    immat = SaisieEntiere("Cette immatriculation n'existe pas, veuillez réessayer (ou tapez \"-1\" pour abandonner : ");
                    if (immat == -1)
                    {
                        return null;
                    }
                    vChoix = v.Find(c => c.Id == immat);
                }
            }
            return vChoix;
        }

        public static Chauffeur ChoixChauffeur(Entreprise E, DateTime date)
        {
            Chauffeur cChoix = null;
            List<Chauffeur> C = new List<Chauffeur>();
            if(E.Salaries!=null && E.Salaries.Count != 0)
            {
                foreach(Salarie s in E.Salaries)
                {
                    if(s is Chauffeur c)
                    {
                        if (c.EstLibre(date))
                        {
                            C.Add(c);
                        }
                    }
                }
            }
            if (C != null && C.Count != 0)
            {
                Console.WriteLine("Voici la liste des chauffeur disponible le " + date.ToString("dd/mm") + " : ");
                foreach (Chauffeur c in C)
                {
                    c.Afficher(E);
                }
                int id = SaisieEntiere("Veuillez saisir le numéro du chauffeur (ou tapez \"-1\" pour abandonner) : ");
                if (id == -1)
                {
                    return null;
                }
                cChoix = C.Find(c => c.Id == id);
                while (cChoix == null)
                {
                    id = SaisieEntiere("Veuillez saisir le numéro du chauffeur (ou tapez \"-1\" pour abandonner) : ");
                    if (id == -1)
                    {
                        return null;
                    }
                    cChoix = C.Find(c => c.Id == id);
                }
            }
            return cChoix;
        }
        #endregion

        static bool AjouterCommande(Entreprise E)
        {
            bool ajout = false;
            int id = E.IdCommandeSuivant();
            int idEdt = E.IdEdtSuivant();

            string nom = SaisieNom("Veuillez saisir le nom du client : ");
            string prenom = SaisieNom("Veuillez saisir le prénom du client : ");
            Client client = E.Clients.Find(c => (c.Nom == nom && c.Prenom == prenom));
            while (client == null)
            {
                Console.WriteLine("Ce client n'existe pas");
                Console.WriteLine("Tapez 0 : Pour le rajouter à la liste de clients");
                Console.WriteLine("Tapez 1 : Pour en saisir un nouveau");
                int n = SaisiePositif(1, 0);
                if (n == 1)
                {
                    nom = SaisieNom("Veuillez saisir le nom du client : ");
                    prenom = SaisieNom("Veuillez saisir le prénom du client : ");
                    client = E.Clients.Find(c => (c.Nom == nom && c.Prenom == prenom));
                }
                else
                {
                    AjouterClient(E, nom, prenom);
                    client = E.Clients.Find(c => (c.Nom == nom && c.Prenom == prenom));
                }
            }
            Tlivraison livrer = MenuLivraison();
            DateTime date = SaisieDateTime("Veuillez saisir la date de livraison : ");
            while (date <= DateTime.Now)
            {
                date = SaisieDateTime("Cette date est déjà passé\nVeuillez saisir à nouveau la date de livraison : ");
            }

            // Affichage des disponibilité, et choix de la date en fonction des disponibilité des chauffeur et des véhicule
            // Plusieurs option au choix, avec date et chauffeur/prix (libre)
            Vehicule v = ChoixVehicule(E, livrer, date);
            if (v == null)
            {
                return false;
            }
            Chauffeur chauffeur = ChoixChauffeur(E, date);
            if (chauffeur == null)
            {
                return false;
            }

            Graphe test = new Graphe(LectureSommet(), LectureArc());
            if (test.Distance(test.Sommets.Find(c => c.VIlle.Villes == chauffeur.VILLE.Villes), test.Sommets.Find(c => c.VIlle.Villes == client.VILLE.Villes)) > 10000)
            {
                Console.WriteLine("Il n'existe actuellement aucun chemin qui relie la ville du chauffeur à la ville du client");
                return false;
            }

            EDT edt = new EDT(idEdt, chauffeur.VILLE.Villes, client.VILLE.Villes, date);        // Quelle ville de départ
            Commande c = new Commande(id, client, livrer, edt);
            c.AjouterVehicule(v);
            c.AjouterChauffeur(chauffeur);

            if (E.AjouterCommande(c))
            {
                E.Edts.Add(edt);
                chauffeur.AjouterEDT(edt);
                v.AjouterEDT(edt);
                Console.WriteLine("La commande a bien été enregistré");
                ajout = true;
            }
            else
            {
                Console.WriteLine("La commande n'a pas pu être enregistré");
            }

            return ajout;
        }

        static bool ModifierCommande(Entreprise E)
        {
            bool modifier = false;
            Commande c = SaisieCommande(E);
            if (E.ModifierCommande(c))
            {
                Console.WriteLine("La commande a bien été modifiée");
            }
            else
            {
                Console.WriteLine("Erreur : Opération intérompue, la commande n'a pas pu être modifiée");
            }
            return modifier;
        }

        static bool SupprimerCommande(Entreprise E)
        {
            bool sup = false;
            int id = SaisieEntiere("Veuillez entrer l'identifiant de la commande : ");
            Commande c = E.Commandes.Find(c => c.Id == id);
            while (c == null)
            {
                Console.WriteLine("Cette commande n'existe pas, veuillez réessayer (\"entrer\" pour continuer ou \"q\" pour abandonner)");
                string s = Console.ReadLine();
                if (s == "q")
                {
                    return sup;
                }
                id = SaisieEntiere("Veuillez entrer l'identifiant de la commande : ");
                c = E.Commandes.Find(c => c.Id == id);
            }
            Console.WriteLine("Voici les information de la commande " + id + " :");
            c.Afficher(E);
            Console.WriteLine("\n");
            Console.WriteLine("Confirmez la suppression de cette commande");
            Console.WriteLine("\tTapez 0 : Non, je ne souhaite pas supprimer cette commande");
            Console.WriteLine("\tTapez 1 : Oui, je souhaite supprimer cette commande");
            int n = SaisiePositif(1, 0);
            if (n == 1)
            {
                c.Vehicule.SupprimerEDT(c.Edt);
                c.Chauffeur.SupprimerEDT(c.Edt);
                if (E.SupprimerCommande(c))
                {
                    Console.WriteLine("La commande " + id + " a bien été supprimé");
                    sup = true;
                }
                else
                {
                    Console.WriteLine("La commande " + id + " n'a pas pu être supprimé : La commande a déjà été réalisée");
                }
            }
            return sup;
        }

        static Commande SaisieCommande(Entreprise E)
        {
            int n = SaisieEntiere("Veuillez entrer l'identifiant de la commande : ");
            Commande c = E.Commandes.Find(c => c.Id == n);
            while (c == null)
            {
                n = SaisieEntiere("Erreur : Cette commande n'existe pas\nVeuillez entrer un nouvel identifiant (-1 pour abandonner) : ");
                if (n == -1)
                {
                    return null;
                }
                c = E.Commandes.Find(c => c.Id == n);
            }
            return c;
        }

        static void DetailCommande(Entreprise E)
        {
            Commande c = SaisieCommande(E);
            if (c != null)
            {
                c.Afficher(E);
            }
            else
            {
                Console.WriteLine("Abandon");
            }
        }

        static void ListeCommande(Entreprise E)
        {
            Console.WriteLine("\n1. Afficher la liste de toutes les commandes\n");
            if (E.Commandes != null && E.Commandes.Count != 0)
            {
                E.Commandes.ForEach(c => c.Afficher(E));
            }
            else
            {
                Console.WriteLine("Aucune commande n'a été enregistré");
            }
            Console.Write("\nTapez 1 pour n'afficher que les commandes réalisées\nTapez 2 pour n'afficher que les commandes en cours\nTapez Entrer pour continuer : ");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.Key == ConsoleKey.D1)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 4 : Gestion des commandes de l'entreprise");
                Console.WriteLine("\n1. Afficher la liste des commandes réalisées\n");
                if (E.Commandes != null && E.Commandes.Count != 0)
                {
                    E.Commandes.FindAll(c => c.Termine).ForEach(c => c.Afficher(E));
                }
                else
                {
                    Console.WriteLine("Aucune commande n'a été réalisée");
                }
            }
            if (info.Key == ConsoleKey.D2)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 4 : Gestion des commandes de l'entreprise");
                Console.WriteLine("\n1. Afficher la liste des commandes en cours\n");
                if (E.Commandes != null && E.Commandes.Count != 0)
                {
                    E.Commandes.FindAll(c => c.Termine == false).ForEach(c => c.Afficher(E));
                }
                else
                {
                    Console.WriteLine("Aucune commande n'est en cours");
                }
            }
        }

        static void ActualiserCommande(Entreprise E)
        {
            if(E.Commandes!=null && E.Commandes.Count != 0)
            {
                E.Commandes.ForEach(c => c.CommandeValider());
            }
        }
        #endregion

        #region Partie 5 : Statistiques

        #region Afficher le nombre de commande par chauffeur
        static void AffichageCommandeChauffeur(Entreprise E)
        {
            foreach(Salarie s in E.Salaries)
            {
                if (s.Poste == Tposte.Chauffeur)
                {
                    int i = 0;
                    foreach(Commande c in E.Commandes)
                    {
                        if (c.Chauffeur == s) i += 1;
                    }
                    Console.WriteLine(s.Nom + " " + s.Prenom + "\n\tNombre de commandes : " + i);
                }
            }
        }
        #endregion

        #region Afficher le nombre de commande par chauffeur
        static void AffichageCommandePeriodeTemps(Entreprise E)
        {
            DateTime depart = SaisieDateTime("Saisissez une date de départ : ");
            DateTime fin = SaisieDateTime("Saisissez une date de fin : ");
            foreach(Commande c in E.Commandes)
            {
                if(DateTime.Compare(depart,c.Edt.DateJour)<0 && DateTime.Compare(fin, c.Edt.DateJour) > 0)
                {
                    c.Afficher(E);
                }
            }

        }
        #endregion
        
        #region Affichage de la moyenne des prix des commandes
        static void AffichageMoyenneDesCommandes(Entreprise E)
        {
            double moyenne = 0;
            foreach(Commande c in E.Commandes)
            {
                moyenne += c.Prix;
            }
            moyenne /= E.Commandes.Count;
            moyenne = Math.Round(moyenne, 2);
            Console.WriteLine("Le prix moyen des commandes est : " + moyenne);
        }
        #endregion

        #region Affichage de la moyenne des dépenses client
        static void AffichageMoyenneDepenseClient(Entreprise E)
        {
            double moyenne = 0;
            foreach (Client c in E.Clients)
            {
                moyenne += c.Depense;
            }
            moyenne /= (double)E.Clients.Count;
            moyenne = Math.Round(moyenne, 2);
            Console.WriteLine("Le prix moyen de dépense des clients est : " + moyenne);
        }
        #endregion

        #region Affichage commandes par clients
        static void AffichageCommandesParClient(Entreprise E)
        {
            foreach(Client c in E.Clients)
            {
                SeparateurMessage(" " + c.Prenom + " " + c.Nom + " ", 20);
                foreach (Commande commande in E.Commandes)
                {
                    if(c == commande.Client)
                    {
                        commande.Afficher(E);
                    }
                }
            }
        }
        #endregion

        #endregion


        #region Menu
        static int Menu()
        {
            Console.WriteLine("Menu : Interface de gestion de l'entreprise");
            Console.WriteLine("\tTapez 1 : Gestion des salariés de l'entreprise");
            Console.WriteLine("\tTapez 2 : Gestion des clients de l'entreprise");
            Console.WriteLine("\tTapez 3 : Gestion des véhicules et des chauffeurs de l'entreprise");
            Console.WriteLine("\tTapez 4 : Gestion des commandes de l'entreprise");
            Console.WriteLine("\tTapez 5 : Statistiques générales de l'entreprise");
            Console.WriteLine("\tTapez 6 : Expension spatiale de l'entreprise");
            Console.WriteLine("\tTapez 7 : Sauvegarder toutes les modifications");
            Console.WriteLine("\tTapez 8 : Quitter le menu\n");

            int choix = SaisiePositif(8);

            return choix;
        }

        #region Menu Tposte/Tlivraison
        static Tposte MenuPoste(string message= "Veuillez choisir le poste du salarié")
        {
            Console.WriteLine(message);
            Console.WriteLine("\tTapez 0 : Directeur Général");
            Console.WriteLine("\tTapez 1 : Directeur Commercial");
            Console.WriteLine("\tTapez 2 : Commercial");
            Console.WriteLine("\tTapez 3 : Directeur des opérations");
            Console.WriteLine("\tTapez 4 : Chef d'équipe");
            Console.WriteLine("\tTapez 5 : Chauffeur");
            Console.WriteLine("\tTapez 6 : Directeur RH");
            Console.WriteLine("\tTapez 7 : Formateur");
            Console.WriteLine("\tTapez 8 : Contrat");
            Console.WriteLine("\tTapez 9 : Directeur Financier");
            Console.WriteLine("\tTapez 10 : Directeur Comptable");
            Console.WriteLine("\tTapez 11 : Comptable");
            Console.WriteLine("\tTapez 12 : Contrôleur de gestion\n");

            int i = SaisiePositif(12, 0, "Votre choix : ");
            return (Tposte)i;
        }
        static Tlivraison MenuLivraison(string message = "Veuillez choisir le type de livraison")
        {
            Console.WriteLine(message);
            Console.WriteLine("\tTapez 0 : Transport de passagers");
            Console.WriteLine("\tTapez 1 : Livraison de matériaux (en petit volume)");
            Console.WriteLine("\tTapez 2 : Livraison de grand volume de liquide");
            Console.WriteLine("\tTapez 3 : Livraison en vrac (sable, gravier, ...)");
            Console.WriteLine("\tTapez 4 : Livraison de marchandise périssable");

            int i = SaisiePositif(12, 0, "Votre choix : ");
            return (Tlivraison)i;
        }
        #endregion

        #region Sous Menu 1 : Salariés
        static int SM1()
        {
            int choix = 0;
            Console.WriteLine("Sous Menu 1 : Gestion des salariés de l'entreprise");
            Console.WriteLine("\tTapez 1 : Afficher l'organigramme de l'entreprise");
            Console.WriteLine("\tTapez 2 : Ajouter un salarié à l'organigramme de l'entreprise");
            Console.WriteLine("\tTapez 3 : Exclure un salarié à l'organigramme de l'entreprise");
            Console.WriteLine("\tTapez 4 : Recruter un salarié");
            Console.WriteLine("\tTapez 5 : Modifier les informations d'un salarié");
            Console.WriteLine("\tTapez 6 : Licencier un salarié");
            Console.WriteLine("\tTapez 7 : Sauvegarder la base de donnée des salariés");
            Console.WriteLine("\tTapez 8 : Quitter le sous menu\n");

            choix = SaisieEntiere("Veuillez saisir votre choix : ");
            while (choix < 1 || choix > 8)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }

            return choix;
        }
        static void SousMenu1(Entreprise E)
        {
            int choix = SM1();
            while (choix != 8)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 1 : Gestion des salariés de l'entreprise");
                switch (choix)
                {
                    #region Affichage organigramme
                    case 1:
                        Console.WriteLine("\t1. Afficher l'organigramme de l'entreprise\n");
                        Salarie s = null;
                        if (E.Salaries.Count > 1)
                        {
                              s= E.Salaries.Find(c => c.Poste == Tposte.DirecteurGeneral && c.Successeur != null);
                        }
                        else
                        {
                            s = E.Salaries.Find(c => c.Poste == Tposte.DirecteurGeneral);
                        }
                        if(s != null)
                        {
                            s.AffichageOrganigramme(s);
                        }

                        break;
                    #endregion
                    #region Ajouter Organigramme
                    case 2:
                        Console.WriteLine("\t2. Ajouter un salarié à l'organigramme de l'entreprise\n");
                        AjouterSalarieOrganigramme(E);

                        break;
                    #endregion
                    #region Exclure Organigramme
                    case 3:
                        Console.WriteLine("\t3. Exclure un salarié à l'organigramme de l'entreprise\n");
                        ExclureSalarieOrganigramme(E);
                        break;
                    #endregion
                    #region Recrutement
                    case 4:
                        Console.WriteLine("\n4. Recruter un salarié\n");
                        Recrutter(E);
                        break;
                    #endregion
                    #region Modifier Salarié
                    case 5:
                        Console.WriteLine("\t5. Modifier les informations d'un salarié ");
                        Console.WriteLine("\tTapez 1 : Modifier le poste d'un salarié");
                        Console.WriteLine("\tTapez 2 : Modifier le salaire d'un salarié");
                        int choix4 = SaisiePositif(2, 1);
                        if (choix4 == 1)
                        {
                            SeparateurMessage(" Modifier le poste d'un salarié ");
                            ModifierPoste(E);
                        }
                        else
                        {
                            SeparateurMessage(" Modifier le salaire d'un salarié ");
                            ModifierSalaire(E);
                        }
                        
                        break;
                    #endregion
                    #region Licenciement
                    case 6:
                        Console.WriteLine("\t6. Licencier un salarié\n");
                        Console.Write("Veuillez saisir le nom du salarié : ");
                        string nom5 = Console.ReadLine();
                        Console.Write("Veuillez saisir le prénom du salarié : ");
                        string prenom5 = Console.ReadLine();
                        Salarie s5 = E.TrouverSalarie(nom5, prenom5);
                        if (E.Licencier(s5))
                        {
                            Console.WriteLine("\nCe salarié a bien été licencier");
                            Console.WriteLine("Ce poste est déjà occupé, voulez vous le réattribuer à ce salarié (0:Non ; 1:Oui)");
                            int c = -1;
                            c = Program.SaisiePositif(1, 0);
                            if (c == 1)
                            {
                                // E.Orga.Supprimer(s5);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nCe salarié est inconnu de l'entreprise");
                        }
                        break;
                    #endregion
                    #region Sauvegarde
                    case 7:
                        Console.WriteLine("\t7. Sauvegarder l'organigramme et la base de donnée des salariés\n");
                        if (E.SaveSalaries())
                        {
                            Console.WriteLine("La liste des salariés a bien été enregistré");
                        }
                        else
                        {
                            Console.WriteLine("La liste des salariés n'a pas pu être enregistré");
                        }
                        break;
                    #endregion
                }
                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = SM1();

            }
        }
        #endregion

        #region Sous Menu 2 : Clients
        static int SM2()
        {
            int choix = 0;
            Console.WriteLine("Sous menu 2 : Gestion des clients de l'entreprise");
            Console.WriteLine("\tTapez 1 : Afficher toute la liste des clients (trié par ordre alphabétique)");
            Console.WriteLine("\tTapez 2 : Afficher la liste des clients filtré en fonction de la ville");
            Console.WriteLine("\tTapez 3 : Afficher la liste des meilleurs clients");
            Console.WriteLine("\tTapez 4 : Ajouter un client");
            Console.WriteLine("\tTapez 5 : Supprimer un client");
            Console.WriteLine("\tTapez 6 : Sauvegarder la base de donnée des clients");
            Console.WriteLine("\tTapez 7 : Quitter le sous menu\n");

            choix = SaisieEntiere("Veuillez saisir votre choix : ");
            while (choix < 1 || choix > 7)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }

            return choix;
        }
        static void SousMenu2(Entreprise E)
        {
            int choix = SM2();
            while (choix != 7)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 2 : Gestion des clients de l'entreprise");
                switch (choix)
                {
                    #region Liste clients (tout)
                    case 1:
                        Console.WriteLine("\t1. Afficher toute la liste des clients (trié par ordre alphabétique)\n");
                        E.Clients.Sort();
                        Console.WriteLine("Nom\t\tPrénom\t\tVille\t\tMontant des achats cumulés");
                        E.Clients.ForEach(c => c.Afficher());
                        break;
                    #endregion
                    #region Liste clients (par ville)
                    case 2:
                        Console.WriteLine("\n2. Afficher la liste des clients filtré en fonction de la ville\n");
                        Ville ville = SaisieVilleExistant(E);

                        List<Client> listC = E.Clients.FindAll(c => c.VILLE == ville);
                        if(listC!=null && listC.Count != 0)
                        {
                            Console.WriteLine("Nom\t\tPrénom\t\tAdresse\t\tMontant des achats cumulés");
                            listC.ForEach(c => c.Afficher());
                        }
                        else
                        {
                            Console.WriteLine("Aucun client n'habite dans cette ville");
                        }
                        
                        break;
                    #endregion
                    #region Liste clients (les meilleurs)
                    case 3:
                        Console.WriteLine("\t3. Afficher la liste des meilleurs clients\n");
                        E.Clients.Sort(Client.CompareToDepense);
                        Console.WriteLine("Nom\t\tPrénom\t\tAdresse\t\tMontant des achats cumulés");
                        E.Clients.ForEach(c => c.Afficher());
                        break;
                    #endregion
                    #region Ajouter client
                    case 4:
                        Console.WriteLine("\t4. Ajouter un client\n");
                        AjouterClient(E);
                        break;
                    #endregion
                    #region Supprimer client
                    case 5:
                        Console.WriteLine("\t5. Supprimer un client\n");
                        string nom = SaisieString("Veuillez saisir le nom du client : ");
                        string prenom = SaisieString("Veuillez saisir le prénom du client : ");
                        Client c = E.TrouverClient(nom, prenom);
                        if (E.EnleverClient(c))
                        {
                            Console.WriteLine("Le client a bien été enlevé de la base de donnée");
                        }
                        else
                        {
                            Console.WriteLine("Ce client n'existe pas");
                        }
                        break;
                    #endregion
                    #region Sauvegarder
                    case 6:
                        Console.WriteLine("\t6. Sauvegarder la base de donnée des clients\n");
                        if (E.SaveClients())
                        {
                            Console.WriteLine("La liste des clients a bien été enregistré");
                        }
                        else
                        {
                            Console.WriteLine("La liste des clients n'a pas pu être enregistré");
                        }
                        break;
                    #endregion
                }
                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = SM2();

            }
        }
        #endregion

        #region Sous Menu 3 : Véhicule et chauffeur
        static int SM3()
        {
            int choix = 0;
            Console.WriteLine("Sous menu 3 : Gestion des véhicules de l'entreprise");
            Console.WriteLine("\tTapez 1 : Afficher toute la flotte de véhicule de l'entreprise");
            Console.WriteLine("\tTapez 2 : Ajouter un véhicule");
            Console.WriteLine("\tTapez 3 : Supprimer un véhicule");
            Console.WriteLine("\tTapez 4 : Afficher la liste des chauffeurs et de leurs caractéristiques");
            Console.WriteLine("\tTapez 5 : Recruter un chauffeur");
            Console.WriteLine("\tTapez 6 : Licencier un chauffeur");
            Console.WriteLine("\tTapez 7 : Quitter le sous menu\n");

            choix = SaisieEntiere("Veuillez saisir votre choix : ");
            while (choix < 1 || choix > 7)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }

            return choix;
        }
        static void SousMenu3(Entreprise E)
        {
            int choix = SM3();
            while (choix != 7)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 3 : Gestion des véhicules de l'entreprise");
                switch (choix)
                {
                    #region Liste des véhicules
                    case 1:
                        Console.WriteLine("\t1. Afficher toute la flotte de véhicule de l'entreprise\n");
                        AfficherVehicule(E);
                        break;
                    #endregion
                    #region Ajouter un véhicule
                    case 2:
                        Console.WriteLine("\t2. Ajouter un véhicule\n");
                        AjouterVehicule(E);
                        break;
                    #endregion
                    #region Supprimer un véhicule
                    case 3:
                        Console.WriteLine("\n3. Supprimer un véhicule\n");
                        SupprimerVehicule(E);
                        break;
                    #endregion
                    #region Liste des chauffeurs et de leurs caractéristiques
                    case 4:
                        Console.WriteLine("\t4. Afficher la liste des chauffeurs et de leurs caractéristiques\n");
                        if (E.Salaries != null && E.Salaries.Count != 0)
                        {
                            foreach (Salarie s in E.Salaries)
                            {
                                if (s is Chauffeur c)
                                {
                                    c.Afficher(E);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region Ajouter un chauffeur
                    case 5:
                        Console.WriteLine("\t5. Recruter un chauffeur\n");
                        AjouterChauffeur(E);
                        break;
                    #endregion
                    #region Supprimer un chauffeur
                    case 6:
                        Console.WriteLine("\t6. Licencier un chauffeur\n");
                        SupprimerChauffeur(E);
                        break;
                    #endregion
                }
                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = SM3();

            }
        }
        #endregion

        #region Sous Menu 4 : Commandes
        static int SM4()
        {
            int choix = 0;
            Console.WriteLine("Sous menu 4 : Gestion des commandes de l'entreprise");
            Console.WriteLine("\tTapez 1 : Afficher la liste de toutes les commandes");
            Console.WriteLine("\tTapez 2 : Afficher tous les détails d'une commande");
            Console.WriteLine("\tTapez 3 : Ajouter une commande");
            Console.WriteLine("\tTapez 4 : Modifier une commande");
            Console.WriteLine("\tTapez 5 : Supprimer une commande");        
            Console.WriteLine("\tTapez 6 : Quitter le sous menu\n");

            choix = SaisieEntiere("Veuillez saisir votre choix : ");
            while (choix < 1 || choix > 6)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }

            return choix;
        }
        static void SousMenu4(Entreprise E)
        {
            ActualiserCommande(E);
            int choix = SM4();
            while (choix != 6)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 4 : Gestion des commandes de l'entreprise");
                switch (choix)
                {
                    #region Liste des commandes
                    case 1:
                        ListeCommande(E);
                        break;
                    #endregion
                    #region Détails d'une commande
                    case 2:
                        Console.WriteLine("\n2. Afficher tous les détails d'une commande\n");
                        DetailCommande(E);
                        break;
                    #endregion
                    #region Ajouter commande
                    case 3:
                        Console.WriteLine("\t3. Ajouter une commande\n");
                        if (!AjouterCommande(E))
                        {
                            Console.WriteLine("Erreur : Saisie intérompu, ou aucune disponibilité");
                        }
                        break;
                    #endregion
                    #region Modifier commande
                    case 4:
                        Console.WriteLine("\t4. Modifier une commande\n");
                        ModifierCommande(E);
                        break;
                    #endregion
                    #region Supprimer commande
                    case 5:
                        Console.WriteLine("\t5. Supprimer une commande\n");
                        SupprimerCommande(E);
                        break;
                    #endregion
                }
                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = SM4();

            }
        }
        #endregion

        #region Sous Menu 5 : Statistiques
        static int SM5()
        {
            int choix = 0;
            Console.WriteLine("Sous Menu 5 : Statistiques de l'entreprise");
            Console.WriteLine("\tTapez 1 : Afficher par chauffeur le nombre de livraisons effectuées");
            Console.WriteLine("\tTapez 2 : Afficher les commandes selon une période de temps");
            Console.WriteLine("\tTapez 3 : Afficher la moyenne des prix des commandes ");
            Console.WriteLine("\tTapez 4 : Afficher la moyenne des comptes clients");
            Console.WriteLine("\tTapez 5 : Afficher la liste des commandes pour un client ");
            Console.WriteLine("\tTapez 6 : Quitter le sous menu\n");

            choix = SaisieEntiere("Veuillez saisir votre choix : ");
            while (choix < 1 || choix > 6)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }

            return choix;

        }
        static void SousMenu5(Entreprise E)
        {
            int choix = SM5();
            ActualiserCommande(E);
            while (choix != 6)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 5 : Statistiques de l'entreprise");
                switch (choix)
                {
                    #region Nombre Livraison par chauffeur
                    case 1:
                        AffichageCommandeChauffeur(E);
                        break;
                    #endregion

                    #region Commandes par périodes de temps
                    case 2:
                        AffichageCommandePeriodeTemps(E);
                        break;
                    #endregion

                    #region Affichage de la moyenne des commandes clients
                    case 3:
                        AffichageMoyenneDesCommandes(E);
                        break;
                    #endregion

                    #region Affichage de la moyenne des dépenses client
                    case 4:
                        AffichageMoyenneDepenseClient(E);
                        break;
                    #endregion

                    #region Affichage liste des commandes par client
                    case 5:
                        AffichageCommandesParClient(E);
                        break;
                    #endregion
                }
                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = SM5();
            }
        }
        #endregion

        #region Sous Menu 6 : Ville
        static int SM6()
        {
            int choix = 0;
            Console.WriteLine("Sous menu 6 : Expension spatiale de l'entreprise");
            Console.WriteLine("\tTapez 1 : Ajouter une nouvelle ville");
            Console.WriteLine("\tTapez 2 : Ajouter une distance entre deux villes");
            Console.WriteLine("\tTapez 3 : Relier une ville aux autres");
            Console.WriteLine("\tTapez 4 : Afficher les distances d'une ville par rapport aux autres");
            Console.WriteLine("\tTapez 5 : Quitter le sous menu\n");

            choix = SaisieEntiere("Veuillez saisir votre choix : ");
            while (choix < 1 || choix > 5)
            {
                choix = SaisieEntiere("Action inconnue, veuillez en saisir un nouveau : ");
            }

            return choix;
        }
        static void SousMenu6(Entreprise E)
        {
            int choix = SM6();
            while (choix != 5)
            {
                Console.Clear();
                Console.WriteLine("Sous menu 6 : Expension spatiale de l'entreprise");
                switch (choix)
                {
                    #region Créer une ville
                    case 1:
                        Console.WriteLine("\t1. Ajouter une nouvelle ville\n");
                        CreerVille(E);
                        break;
                    #endregion
                    #region Relier deux villes
                    case 2:
                        Console.WriteLine("\t2. Ajouter une distance entre deux villes\n");
                        AjouterNewArc(E);
                        break;
                    #endregion
                    #region Relier une ville aux autres
                    case 3:
                        Console.WriteLine("\t3. Relier une ville aux autres\n");
                        CreerArc(E);
                        break;
                    #endregion
                    #region Afficher distance
                    case 4:
                        Console.WriteLine("\t4. Afficher les distances par rapport à une ville\n");
                        AfficherDistance(E);
                        break;
                    #endregion
                }
                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = SM6();

            }
        }
        #endregion

        #endregion

        #region Thread
        static void simultane()
        {
            string tab ="Bienvenue !";
            string tab2 = "Chargement des données";
            Console.Write("\n\n\t");
            for (int i = 0; i < 100 && i < tab.Length + tab2.Length; i++)
            {
                if (i < tab.Length)
                {
                    Console.Write(tab[i]);
                }
                else
                {
                    int j = i - tab.Length;
                    if (j < tab2.Length)
                    {
                        if (j == 0)
                        {
                            Console.WriteLine("\n");
                        }
                        Console.Write(tab2[j]);
                    }
                }
                Thread.Sleep(30);
            }
            Console.WriteLine("\n");
        }
        #endregion

        static void Main(string[] args)
        {
            #region Chargement
            Thread chargement = new Thread(simultane);
            Thread clock = new Thread(() => Thread.Sleep(2500));
            clock.Start();
            chargement.Start();

            Entreprise TransConnect = Creer();
            List<Sommet> sommets = LectureSommet();
            List<Arc> arcs = LectureArc();
            TransConnect.Graphes = new Graphe(sommets, arcs);

            clock.Join();
            chargement.Join();
            #endregion

            Console.Clear();
            int choix = Menu();
            while (choix != 8)
            {
                Console.Clear();
                switch (choix)
                {
                    case 1:
                        SousMenu1(TransConnect);
                        break;
                    case 2:
                        SousMenu2(TransConnect);
                        break;
                    case 3:
                        SousMenu3(TransConnect);       
                        break;
                    case 4:
                        SousMenu4(TransConnect);
                        break;
                    case 5:
                        SousMenu5(TransConnect);
                        break;
                    case 6:
                        SousMenu6(TransConnect);
                        break;

                    case 7:
                        Console.WriteLine("6. Sauvegarder toute la base de donnée\n");
                        if (TransConnect.Sauvegarder())
                        {
                            SaveArcs(TransConnect.Graphes.Arcs);
                            Console.WriteLine("Toutes les modifications ont bien été enregistrées");
                        }
                        break;
                }

                Console.Write("\n\nTapez Entrer pour continuer : ");
                Console.ReadKey();
                Console.Clear();
                choix = Menu();

            }
        }
    }
}
