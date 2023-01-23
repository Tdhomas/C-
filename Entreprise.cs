using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TransConnect
{
    class Entreprise
    {
        #region Attributs
        private List<Client> clients;
        private List<Salarie> salaries;
        private List<Vehicule> vehicules;
        private List<EDT> edts;
        private List<Commande> commandes;
        private List<Ville> villes;
        private Graphe graphe;
        #endregion

        #region Constructeurs
        public Entreprise(List<Client> clients = null, List<Salarie> salaries = null, List<Vehicule> vehicules = null, List<EDT> edts=null, List<Commande> commandes = null)
        {
            this.clients = clients;
            this.salaries = salaries;
            this.vehicules = vehicules;
            this.edts = edts;
            this.commandes = commandes;
            this.villes = null;
            this.graphe = null;
        }
        #endregion

        #region Propriétés
        public List<Client> Clients
        {
            get { return clients; }
        }
        public List<Salarie> Salaries
        {
            get { return salaries; }
        }
        public List<Vehicule> Vehicules
        {
            get { return vehicules; }
        }
        public List<EDT> Edts
        {
            get { return edts; }
        }
        public List<Commande> Commandes
        {
            get { return commandes; }
        }
        public List<Ville> Villes
        {
            get { return villes; }
        }
        public Graphe Graphes
        {
            get { return graphe; }
            set { graphe = value; }
        }
        #endregion

        #region Méthodes

        #region Clients
        public Client TrouverClient(string nom, string prenom)
        {
            Client clientRecherche = null;
            if (clients != null && clients.Count != 0)
            {
                foreach (Client c in clients)
                {
                    if (c.Nom.ToLower() == nom.ToLower() && c.Prenom.ToLower() == prenom.ToLower())
                    {
                        clientRecherche = c;
                        break;
                    }
                }
            }
            return clientRecherche;
        }
        public Client TrouverClientId(int num)
        {
            Client clientRecherche = null;
            if (clients != null && clients.Count != 0)
            {
                foreach (Client c in clients)
                {
                    if (c.Id == num)
                    {
                        clientRecherche = c;
                        break;
                    }
                }
            }
            return clientRecherche;
        }
        public bool AjouterClient(Client c)
        {
            bool ajouter = false;
            if (clients == null)
            {
                clients = new List<Client>();
            }
            if (c != null)
            {
                clients.Add(c);
                ajouter = true;
            }
            return ajouter;
        }
        public bool EnleverClient(Client c)
        {
            bool supprimer = false;
            if (clients != null && c != null && clients.Contains(c))
            {
                clients.Remove(c);
                supprimer = true;
            }
            return supprimer;
        }
        #endregion

        #region Salariés
        public Salarie TrouverSalarie(string nom, string prenom)
        {
            Salarie salarieRecherche = null;
            if (salaries != null && salaries.Count != 0)
            {
                foreach (Salarie s in salaries)
                {
                    if (s.Nom.ToLower() == nom.ToLower() && s.Prenom.ToLower() == prenom.ToLower())
                    {
                        salarieRecherche = s;
                        break;
                    }
                }
            }
            return salarieRecherche;
        }
        public Salarie TrouverSalarieId(int num)
        {
            Salarie salarieRecherche = null;
            if (salaries != null && salaries.Count != 0)
            {
                salarieRecherche = salaries.Find(c => c.Id == num);
            }
            return salarieRecherche;
        }
        public bool Recrutter(Salarie s)
        {
            bool ajouter = false;
            if (salaries == null)
            {
                salaries = new List<Salarie>();
            }
            if (s != null)
            {
                salaries.Add(s);
                ajouter = true;
            }
            return ajouter;
        }

        public bool Grader_Retrograder(Salarie s, Tposte nouveauPoste)
        {
            bool changer = false;
            if (salaries.Contains(s))
            {
                s.Poste = nouveauPoste;
                changer = true;
            }
            return changer;
        }

        public bool Licencier(Salarie s)
        {
            bool virer = false;
            if (salaries!=null && s!=null && salaries.Contains(s))
            {
                salaries.Remove(s);
                virer = true;
            }
            return virer;
        }

        #endregion

        #region Véhicules
        public Vehicule TrouverVehiculeId(int num)
        {
            Vehicule vehiculeRecherche = null;
            if (vehicules != null && vehicules.Count != 0)
            {
                foreach (Vehicule v in vehicules)
                {
                    if (v.Id == num)
                    {
                        vehiculeRecherche = v;
                        break;
                    }
                }
            }
            return vehiculeRecherche;
        }
        public bool AjouterVehicule(Vehicule v)
        {
            bool ajouter = false;
            if (vehicules == null)
            {
                vehicules = new List<Vehicule>();
            }
            if (v != null)
            {
                vehicules.Add(v);
                ajouter = true;
            }
            return ajouter;
        }
        public bool EnleverVehicule(Vehicule v)
        {
            bool supprimer = false;
            if (vehicules!=null && v != null && vehicules.Contains(v))
            {
                vehicules.Remove(v);
                supprimer = true;
            }
            return supprimer;
        }
        #endregion

        #region EDT
        public int IdEdtSuivant()
        {
            int id = 0;
            if (edts != null && edts.Count != 0)
            {
                foreach (EDT e in edts)
                {
                    if (e.Id > id)
                    {
                        id = e.Id;
                    }
                }
            }
            return id + 1;
        }
        #endregion

        #region Commande
        public int IdCommandeSuivant()
        {
            int id = 0;
            if(commandes!=null && commandes.Count != 0)
            {
                foreach(Commande c in commandes)
                {
                    if (c.Id > id)
                    {
                        id = c.Id;
                    }
                }
            }
            return id + 1;
        }

        public bool AjouterCommande(Commande c)
        {
            bool ajouter = false;
            if (commandes == null)
            {
                clients = new List<Client>();
            }
            if (c != null)
            {
                commandes.Add(c);
                c.Maj(new Graphe(graphe.Sommets, graphe.Arcs));
                ajouter = true;
            }
            return ajouter;
        }
        public bool SupprimerCommande(Commande c)
        {
            bool supprimer = false;
            if (commandes != null && c != null && commandes.Contains(c) && !c.Termine)
            {
                commandes.Remove(c);
                supprimer = true;
            }
            return supprimer;
        }
        public bool ModifierCommande(Commande c)
        {
            bool ajouter = false;
            if (c != null)
            {
                Console.WriteLine("Tapez 1 : Pour modifier la ville de destination de la commande");
                Console.WriteLine("Tapez 2 : Pour modifier la date de la livraison");
                Console.WriteLine("Tapez 3 : pour abandonner la modification");
                int n = Program.SaisiePositif(3);
                switch (n)
                {
                    case 1:
                        Ville newVille = Program.DemanderVille(this, "Veuillez saisir la nouvelle ville de destination : ");
                        c.Edt.VilleArrive = newVille.Villes;
                        break;
                    case 2:
                        DateTime date = Program.SaisieDateTime("Veuillez saisir la nouvelle date de livraison : ");
                        while (date < DateTime.Now || !c.Chauffeur.EstLibre(date))
                        {
                            if(date < DateTime.Now)
                            {
                                date = Program.SaisieDateTime("Cette date est déjà passé\nVeuillez saisir à nouveau la date de livraison : ");
                            }
                            else
                            {
                                Console.WriteLine("Votre chauffeur est indisponible le " + date.ToString("dd/MM/yyyy"));
                                Console.WriteLine("\tTapez 1 : pour changer de chauffeur");
                                Console.WriteLine("\tTapez 2 : pour choisir une autre date");
                                Console.WriteLine("\tTapez 3 : pour abandonner la modification");
                                int n2 = Program.SaisiePositif(3);
                                switch (n2)
                                {
                                    case 1:
                                        Chauffeur chauf = Program.ChoixChauffeur(this, date);
                                        c.Chauffeur = chauf;
                                        break;
                                    case 2:
                                        date = Program.SaisieDateTime("Veuillez saisir à nouveau la date de livraison : ");
                                        break;
                                    case 3:
                                        return false;
                                }
                                date = Program.SaisieDateTime("Cette date est déjà passé\nVeuillez saisir à nouveau la date de livraison : ");
                            }
                        }
                        break;
                    case 3:
                        return false;
                }
                c.Maj(new Graphe(graphe.Sommets, graphe.Arcs));
                ajouter = true;
            }
            return ajouter;
        }
        #endregion

        #region Organigramme
        public void AjouterOrga(Salarie employe)
        {
            if (employe!=null && salaries.Count!=0 && employe.Poste!=Tposte.DirecteurGeneral)
            {
                Console.WriteLine("Quel est le nom de son supérieur hiérarchique DIRECT?");
                string nomsuperieur = Console.ReadLine();
                Console.WriteLine("Quel est le prénom de son supérieur hiérarchique DIRECT?");
                string prenomsuperieur = Console.ReadLine();
                Salarie superieur = TrouverSalarie(nomsuperieur, prenomsuperieur);
                while (superieur == null)
                {
                    Console.WriteLine("Erreur redonner un nom ET/OU de prenomveuillez les ressaisir");
                    Console.WriteLine("Nom : ");
                    nomsuperieur = Console.ReadLine();
                    Console.WriteLine("Prenom : ");
                    prenomsuperieur = Console.ReadLine();

                    superieur = TrouverSalarie(nomsuperieur, prenomsuperieur);
                }
                if (superieur.Successeur == null)
                {
                    superieur.AssocierSucesseur(employe,superieur);
                }
                else superieur.AjoutEmploye(employe, superieur.Successeur);
                Console.WriteLine("Ajout employé dans l'organigramme fait");
            }
            if(employe!=null && employe.Poste == Tposte.DirecteurGeneral )
            {
                Salarie s = salaries.Find(c => c.Poste == Tposte.DirecteurGeneral);
                while (s.Frere != null)
                {
                    s = s.Frere;
                }
                if(s!=employe)s.Frere = employe;
            }
        }
        public void ExclureOrga(Salarie employe)
        {
            if(employe!=null && employe.Poste == Tposte.DirecteurGeneral)
            {
                Console.WriteLine("Existe-il un autre Directeur Général");
                int val = Program.SaisiePositif(2,1,"1. Oui 2.Non : ");
                if(val == 1)
                {
                    Console.WriteLine("Est-il dans l'organigramme");
                    val = Program.SaisiePositif(2, 1, "1. Oui 2.Non : ");
                    if(val == 1)
                    {
                        Salarie s = salaries.Find(c => c.Poste == Tposte.DirecteurGeneral && c!=employe);
                        s.Successeur = employe.Successeur;
                        employe.Successeur = null;
                        employe.Frere = null;
                    }
                    else
                    {
                        Salarie s = salaries.Find(c => c.Poste == Tposte.DirecteurGeneral && c != employe);
                        AjouterOrga(s);
                        s.Successeur = employe.Successeur;
                        employe.Successeur = null;
                        employe.Frere = null;
                    }
                }
                else
                {
                    Console.WriteLine("Ajoutez un nouveau directeur général pour remplacer l'ancien");
                    Program.Recrutter(this);
                    ExclureOrga(employe);
                }
            }
            if(employe != null && employe.Poste != Tposte.DirecteurGeneral)
            {
                /***
             *plusieur cas possible :
             *on retrouve son frere ou sucesseur 
             *1. c'est un employé alors recherche si c'est une feuille on l'eneleve direct
             *2.si ce n'est pas une feuille on fait le lien entre son frère d'avant et son frère d'après
             *3.si c'est un manager il faut voir si on raccroche ses employe directement à son supérieur ou à un de ses collègues
            ***/
                Salarie frereavant = null;
                Salarie superieur = null;
                Salarie temp = null; 
                //recherche supérieur+ possible frere avant lui
                foreach (Salarie s in salaries)
                {
                    if (s.Successeur == employe) superieur = s;
                    if (s.Frere == employe) frereavant = s;
                }
                //Supression si feuille
                if (employe.EstFeuille())
                {
                    if (frereavant != null) frereavant.Frere = null;
                    if (superieur != null) superieur.Successeur = null;
                }
                if (superieur != null) Console.WriteLine("Supérieur trouvé");
                if (frereavant != null) Console.WriteLine("Frere avant trouvé");
                //Il n'est pas manager mais a des collègues reliés à lui
                if (employe.Frere != null && employe.Successeur == null)
                {
                    if (frereavant != null)
                    {
                        frereavant.Frere = employe.Frere;
                        employe.Frere = null;
                    }
                    if (superieur != null)
                    {
                        superieur.Successeur = employe.Frere;
                        employe.Frere = null;
                    }
                }
                //Il est manager et possède des collègues
                if (employe.Frere != null  && employe.Successeur!=null)
                {
                    //Présentation de ses collègues
                    Console.WriteLine("Cette employé est manager et possède de un/des collègue-s qui sont: ");
                    //Recherche du premier collègue
                    Salarie premierfrere = employe;
                    if (frereavant != null)
                    {
                        temp = frereavant;
                        premierfrere = temp;
                        int i = 0;
                        while (i == 0)
                        {
                            foreach (Salarie s in salaries)
                            {
                                if (s.Frere == premierfrere) temp = s;
                            }
                            if (temp == premierfrere) i += 1;
                            premierfrere = temp;
                        }

                    }
                    //Recherche du supérieur hiérarchique
                    foreach (Salarie s in salaries)
                    {
                        if (s.Successeur == premierfrere) premierfrere = s;
                    }
                    //Affichage collègues
                    temp = premierfrere;
                    while (temp != null)
                    {
                        Console.WriteLine(temp.ToString());
                        temp = temp.Frere; ;
                    }
                    /*Choix entre supérieur et collègues
                    Console.WriteLine("Voulez vous rattacher son équipe à son superieur ou à un de ses collègue?(superieur/collegue)");
                    string reponse = Console.ReadLine().ToLower();
                    while(reponse!="superieur" || reponse != "collegue")
                    {
                        Console.WriteLine("Erreur de saisie veuillez choisir entre superieur ou collegue");
                        reponse = Console.ReadLine().ToLower();
                    }
                    //Choix du supérieur
                    if (reponse == "superieur")
                    //choix du collègue*/
                    Console.WriteLine("Choisissez le collègue dont vous voulez rattacher l'équipe grâce à son nom et son prénom");
                    string nom = Program.SaisieString("Saisissez le nom");
                    string prenom = Program.SaisieString("Saisissez le prénom");
                    Salarie collegue = TrouverSalarie(nom, prenom);
                    while (collegue == null)
                    {
                        Console.WriteLine("Erreur les collegue est inexistant veuillez saisir de nouveau un nom");
                        nom = Program.SaisieString("Saisissez le nom");
                        prenom = Program.SaisieString("Saisissez le prénom");
                        collegue = TrouverSalarie(nom, prenom);
                    }
                    if (collegue.Successeur == null) collegue.Successeur = employe.Successeur;
                    else
                    {
                        temp = collegue.Successeur;
                        while (temp.Frere != null) temp = temp.Frere;
                        temp.Frere = employe.Successeur;
                    }
                    //Supression de l'employe dans la chaine
                    if (frereavant != null && employe.Frere != null)
                    {
                        frereavant.Frere = employe.Frere;
                        employe.Frere = null;
                    }
                    if (frereavant != null && employe.Frere == null) frereavant.Frere = null;
                    if (frereavant == null && employe.Frere != null)
                    {
                        superieur.Successeur = employe.Frere;
                        employe.Frere = null;
                    }
                }
                //Est manager mais ne possède pas de collegue
                if(employe.Successeur!=null && employe.Frere==null)
                {
                    Console.WriteLine("Cette employé est manager mais ne possède pas de collègue de même niveau, les employé seront ratachés au supérieur de ceului-ci");
                    superieur.Successeur = employe.Successeur;
                    employe.Successeur = null;
                }
            }
            Console.WriteLine("Exclusion éffectuée");
        }
        #endregion

        #region Ville
        public bool EstPresent(Ville ville)
        {
            bool present = false;
            if (ville != null && villes!=null && villes.Count!=0)
            {
                if (villes.Find(v => v.Villes == ville.Villes) != null)
                {
                    present = true;
                }
            }
            return present;
        }
        public bool AjouterVille(Ville ville)
        {
            bool ajouter = false;
            if (!EstPresent(ville))
            {
                villes.Add(ville);
                ajouter = true;
            }
            return ajouter;
        }
        public Ville TrouverVille(string s)     // Il faut que s soit au bon format "ville"
        {
            Ville ville = null;
            if (s != "" && villes!=null)
            {
                ville = villes.Find(c => c.Villes == s);
            }
            return ville;
        }
        #endregion

        #region Sauvegarde
        public bool SaveClients(string fichier = "clients.txt")
        {
            bool save = false;
            if (clients != null)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach(Client c in clients)
                {
                    ecriture.WriteLine(c.ToString());
                }
                ecriture.Close();
                save = true;
            }
            return save;
        }
        public bool SaveSalaries(string fichier = "salaries.txt")
        {
            bool save = false;
            if (salaries != null)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach (Salarie s in salaries)
                {
                    ecriture.WriteLine(s.ToString());
                }
                ecriture.Close();
                save = true;
            }
            return save;
        }
        public bool SaveVehicules(string fichier = "vehicules.txt")
        {
            bool save = false;
            if (vehicules != null)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach (Vehicule v in vehicules)
                {
                    ecriture.WriteLine(v.ToString());
                }
                ecriture.Close();
                save = true;
            }
            return save;
        }

        public bool SaveEdts(string fichier = "edts.txt")
        {
            bool save = false;
            if (edts != null)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach (EDT e in edts)
                {
                    ecriture.WriteLine(e.ToString());
                }
                ecriture.Close();
                save = true;
            }
            return save;
        }

        public bool SaveCommande(string fichier = "commandes.txt")
        {
            bool save = false;
            if (commandes != null)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach (Commande c in commandes)
                {
                    ecriture.WriteLine(c.ToString());
                }
                ecriture.Close();
                save = true;
            }
            return save;
        }

        public bool SaveVille(string fichier = "villes.txt")
        {
            bool save = false;
            if (villes != null)
            {
                StreamWriter ecriture = new StreamWriter(fichier);
                foreach (Ville v in villes)
                {
                    ecriture.WriteLine(v.Villes);
                }
                ecriture.Close();
                save = true;
            }
            return save;
        }
        #endregion

        #region Lecture
        public bool LectureClients(string fichier = "clients.txt")
        {
            this.clients = new List<Client>();
            if (villes != null)
            {
                try
                {
                    StreamReader lecture = new StreamReader(fichier);
                    while (lecture.Peek() > 0)
                    {
                        string[] tem = lecture.ReadLine().Split(";");
                        clients.Add(new Client(Convert.ToInt32(tem[0]), tem[1], tem[2], Convert.ToDateTime(tem[3]), villes.Find(c => c.Villes == tem[4]), tem[5], tem[6], Convert.ToDouble(tem[7]), Convert.ToBoolean(tem[8])));
                    }
                    lecture.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        
        public bool MajSalaries(string fichier = "salaries.txt")
        {
            if (salaries != null && salaries.Count != 0)
            {
                try
                {
                    StreamReader lecture = new StreamReader(fichier);
                    while (lecture.Peek() > 0)
                    {
                        string[] tem = lecture.ReadLine().Split(";");
                        salaries.Find(c => c.Id == int.Parse(tem[0])).Successeur = salaries.Find(c => c.Id == int.Parse(tem[10]));
                        salaries.Find(c => c.Id == int.Parse(tem[0])).Frere = salaries.Find(c => c.Id == int.Parse(tem[11]));
                    }
                    lecture.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool LectureSalaries(string fichier = "salaries.txt")
        {
            this.salaries = new List<Salarie>();
            if (villes != null)
            {
                try
                {
                    StreamReader lecture = new StreamReader(fichier);
                    while (lecture.Peek() > 0)
                    {
                        string[] tem = lecture.ReadLine().Split(";");
                        if (tem.Length > 13)
                        {
                            List<EDT> edt = new List<EDT>();
                            if (tem.Length > 14)
                            {
                                try
                                {
                                    foreach (string e in tem[14].Split("|"))
                                    {
                                        if (edts!=null && e != "" && int.Parse(e) != -1)
                                        {
                                            edt.Add(edts.Find(c => c.Id == int.Parse(e)));
                                        }

                                    }
                                }
                                catch
                                {

                                }

                            }
                            salaries.Add(new Chauffeur(Convert.ToInt32(tem[0]), tem[1], tem[2], Convert.ToDateTime(tem[3]), villes.Find(c => c.Villes == tem[4]), tem[5], tem[6], Convert.ToDateTime(tem[7]), (Tposte)Convert.ToInt32(tem[8]), Convert.ToDouble(tem[9]), salaries.Find(c => c.Id == int.Parse(tem[10])), salaries.Find(c => c.Id == int.Parse(tem[11])), int.Parse(tem[12]), Convert.ToDouble(tem[13]), edt));
                        }
                        else
                        {
                            salaries.Add(new Salarie(Convert.ToInt32(tem[0]), tem[1], tem[2], Convert.ToDateTime(tem[3]), villes.Find(c => c.Villes == tem[4]), tem[5], tem[6], Convert.ToDateTime(tem[7]), (Tposte)Convert.ToInt32(tem[8]), Convert.ToDouble(tem[9]), salaries.Find(c => c.Id == int.Parse(tem[10])), salaries.Find(c => c.Id == int.Parse(tem[11])), int.Parse(tem[12])));
                        }
                    }
                    lecture.Close();
                    MajSalaries();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool LectureVehicules(string fichier = "vehicules.txt")
        {
                this.vehicules = new List<Vehicule>();
                if (edts != null)
                {
                    StreamReader lecture = new StreamReader(fichier);
                try
                {
                    while (lecture.Peek() > 0)
                    {
                        string[] tem = lecture.ReadLine().Split(";");
                        List<EDT> edt = new List<EDT>();
                        foreach (string e in tem[4].Split("|"))
                        {
                            if (e != "" && e != "-1")
                            {
                                edt.Add(edts.Find(c => c.Id == int.Parse(e)));
                            }
                        }
                        switch (tem[0])
                        {
                            case ("Voiture"):
                                vehicules.Add(new Voiture(Convert.ToInt32(tem[1]), Convert.ToDouble(tem[2]), Convert.ToDouble(tem[3]), edt, Convert.ToInt32(tem[5])));
                                break;
                            case ("Camionnette"):
                                vehicules.Add(new Camionnette(Convert.ToInt32(tem[1]), Convert.ToDouble(tem[2]), Convert.ToDouble(tem[3]), edt, tem[5]));
                                break;
                            case ("Camion_benne"):
                                vehicules.Add(new Camion_benne(Convert.ToInt32(tem[1]), Convert.ToDouble(tem[2]), Convert.ToDouble(tem[3]), edt, tem[5], Convert.ToDouble(tem[6]), int.Parse(tem[7]), Convert.ToBoolean(tem[8])));
                                break;
                            case ("Camion_citerne"):
                                vehicules.Add(new Camion_citerne(Convert.ToInt32(tem[1]), Convert.ToDouble(tem[2]), Convert.ToDouble(tem[3]), edt, tem[5], Convert.ToDouble(tem[6]), tem[7]));
                                break;
                            case ("Camion_frigorifique"):
                                vehicules.Add(new Camion_frigorifique(Convert.ToInt32(tem[1]), Convert.ToDouble(tem[2]), Convert.ToDouble(tem[3]), edt, tem[5], Convert.ToDouble(tem[6]), int.Parse(tem[7])));
                                break;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                    lecture.Close();
                    return true;
                }
                else
                {
                    return true;
                }
            
        }

        public bool LectureEdts(string fichier = "edts.txt")
        {
            this.edts = new List<EDT>();
            
                StreamReader lecture = new StreamReader(fichier);
                while (lecture.Peek() > 0)
                {
                    try
                    {
                        string[] tem = lecture.ReadLine().Split("|");
                        edts.Add(new EDT(int.Parse(tem[0]), tem[1], tem[2], Convert.ToDateTime(tem[3]), Convert.ToDouble(tem[4]), Convert.ToDouble(tem[5])));
                    }
                    catch
                    {
                        return false;
                    }
                }
                lecture.Close();

                return true;
            
        }

        public bool LectureCommandes(string fichier = "commandes.txt")
        {
            this.commandes = new List<Commande>();
            
                if (edts != null && clients != null && salaries != null && vehicules != null)
                {
                    StreamReader lecture = new StreamReader(fichier);
                    while (lecture.Peek() > 0)
                    {
                        try
                        {
                            string[] tem = lecture.ReadLine().Split(";");
                            commandes.Add(new Commande(int.Parse(tem[0]), (Tlivraison)(Convert.ToInt32(tem[1])), Convert.ToDouble(tem[2]), Convert.ToBoolean(tem[3]), TrouverClientId(int.Parse(tem[4])), TrouverSalarieId(int.Parse(tem[5])) as Chauffeur, TrouverVehiculeId(int.Parse(tem[6])), edts.Find(c => c.Id == int.Parse(tem[7]))));

                        }
                        catch
                        {
                            return false;
                        }
                    }
                    lecture.Close();

                    return true;
                }
                else
                {
                    return true;
                }
        }

        public bool LectureVille(string fichier = "villes.txt")
        {
            this.villes = new List<Ville>();
            try
            {
                StreamReader lecture = new StreamReader(fichier);
                while (lecture.Peek() > 0)
                {
                    villes.Add(new Ville(lecture.ReadLine()));
                }
                lecture.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Tout (lecture/sauvegarde)
        public bool Sauvegarder()
        {
            return SaveVille() && SaveClients() && SaveSalaries() && SaveVehicules() && SaveEdts() && SaveCommande();
        }

        public bool Initialiser()
        {
            return LectureVille() && LectureClients() && LectureSalaries() && LectureEdts() && LectureVehicules() && LectureCommandes();
        }
        #endregion

        
        #endregion
    }
}
