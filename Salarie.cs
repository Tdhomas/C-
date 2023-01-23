using System;
using System.Collections.Generic;
using System.Text;

namespace TransConnect
{
    enum Tposte
    {
        DirecteurGeneral, DirecteurCommercial, Commercial, DirecteurDesOperations, ChefEquipe, Chauffeur, DirecteurRH, Formateur, Contrat, DirecteurFinancier, DirecteurComptable, Comptable, ControleurDeGestion
    }
    class Salarie : Personne
    {
        #region Attributs
        protected DateTime entree;
        protected Tposte poste;
        protected double salaire;

        //Organigramme
        protected Salarie successeur;
        protected Salarie frere;
        protected int hauteur;
        #endregion

        #region Constructeurs
        public Salarie(int numSS, string nom = "", string prenom = "", DateTime naissance = new DateTime(), Ville ville = null, string email = "", string tel = "",DateTime entree=new DateTime(),Tposte poste=new Tposte(),double salaire = 0, Salarie successeur = null, Salarie frere = null, int hauteur = 0) : base(numSS, nom, prenom, naissance, ville, email, tel)
        {
            if (entree == new DateTime())
            {
                entree = DateTime.Now;
            }
            this.entree = entree;
            this.poste = poste;
            this.salaire = salaire;
            this.successeur = successeur;
            this.frere = frere;
            this.hauteur = hauteur;
        }
        public Salarie(string nom, Tposte poste, int num=0) : base(num, nom)
        {
            this.poste = poste;

        }
        #endregion

        #region Propriétés
        public DateTime Entree
        {
            get { return entree; }
        }
        public Tposte Poste
        {
            get { return poste; }
            set { poste = value; }
        }
        public double Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public Salarie Successeur
        {
            get { return successeur; }
            set { successeur = value; }
        }
        public Salarie Frere
        {
            get { return frere; }
            set { frere = value; }
        }
        public int Hauteur
        {
            get { return hauteur; }
            set { hauteur = value; }
        }
        #endregion


        #region Méthodes

        #region Affichage
        public override string ToString()
        {
            string s = base.ToString() + ";" + entree.ToString() + ";" + ((int)poste) + ";" + salaire + ";";
            if (successeur != null)
            {
                s += successeur.Id + ";";
            }
            else
            {
                s += -1 + ";";
            }
            if (frere != null)
            {
                s += frere.Id + ";" + hauteur;
            }
            else
            {
                s += -1 + ";" + hauteur;
            }
            return s;
        }
        public string tostring()
        {
            return base.ToString() + "\nDate d'embauche : " + this.entree + "\nPoste : " + this.poste + "\nSalaire : " + this.salaire.ToString(); ;
        }
        public string AffichageNomPoste()
        {
            return this.nom + " " + this.poste;
        }
        #endregion

        #region Organigramme
        public bool AssocierSucesseur(Salarie nouveau, Salarie actuel)
        {
            bool ok = false;
            if (nouveau != null && actuel.Successeur == null)
            {
                actuel.Successeur = nouveau;
                nouveau.Hauteur = actuel.Hauteur + 1;
                ok = true;
            }
            return ok;
        }
        public bool AssocierFrere(Salarie nouveau, Salarie actuel)
        {
            bool ok = false;
            if (nouveau != null && actuel.Frere == null)
            {
                actuel.Frere = nouveau;
                ok = true;
                nouveau.Hauteur = actuel.Hauteur;
            }
            return ok;
        }
        public bool EstFeuille()
        {
            bool ok = false;
            if (this.successeur == null && this.frere == null) ok = true;
            return ok;
        }
        public void AjoutEmploye(Salarie employe, Salarie actuel)
        {
            while (actuel.Frere != null)
            {
                actuel = actuel.Frere;
            }
            actuel.AssocierFrere(employe, actuel);
        }
        public void AffichageOrganigramme(Salarie s)
        {
            if (s.Hauteur != 0)
            {
                Console.Write("     |     ");
                for (int i = 0; i < s.Hauteur - 1; i++)
                {
                    Console.Write("|     ");
                }
                Console.Write("|");
                Console.Write("-" + s.AffichageNomPoste());
                Console.WriteLine("");
            }
            if (s.Hauteur == 0)
            {
                if (s.Poste != Tposte.DirecteurGeneral)
                {
                    Console.WriteLine("     |-" + s.AffichageNomPoste());
                }
                else
                {
                    Console.WriteLine(s.AffichageNomPoste());
                }
            }
            if (s.Successeur != null)
            {
                AffichageOrganigramme(s.Successeur);
            }
            if (s.Frere != null)
            {
                AffichageOrganigramme(s.Frere);
            }
        }
        #endregion



        #endregion





     





    }
}
