// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;
using Foundation;
using UIKit;

namespace Myfitnessapp.iOS.CreaScheda
{
	public partial class CreaSchedaNew : UIViewController
	{
        MyScript lib = new MyScript();
        UIColor bouble = UIColor.SecondarySystemGroupedBackgroundColor;
        UIColor background = UIColor.TertiarySystemGroupedBackgroundColor;
        private float indice=1; //Indice del moltiplicatore dell'estensione
        private float AltezzaContenuto = 980; //Altezza della View compresa dei tre elementi
        private double viewmax = 0;
        //Controlla e verifica che si entri negli if-else una sola volta (max 15)
        private bool flag1 = false;
        private bool flag2 = false;
        private bool flag3 = false;
        private bool flag4 = false;
        private bool flag5 = false;
        private bool flag6 = false;
        private bool flag7 = false;
        private bool flag8 = false;
        private bool flag9 = false;
        private bool flag10 = false;
        private bool flag11 = false;
        private bool flag12 = false;
        private bool flag13 = false;
        private bool flag14 = false;

        public CreaSchedaNew (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidAppear(bool animated)
        {
           
            base.ViewDidAppear(animated);
            //Imposta l'altezza della view giusta
            SetNormalHeightScrollView();

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Imposto i vari colori
            ViewPrincipale.BackgroundColor = background; //Imposto il background di fondo
            ScrollView.BackgroundColor = background; //Imposto il background sotto lo scrolling

            //Bolle per info
            CreateBouble(InfoStack, 25, InfoStack.Layer.Bounds.Height + 20, 2, -35);
            //Bolle per gli esercizi
            CreateBouble(StackEsercizio, 25, StackEsercizio.Layer.Bounds.Height+30, -10, -15);
            CreateBouble(StackES2, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES3, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES4, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES5, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES6, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES7, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES8, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES9, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES10, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES11, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES12, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES13, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES14, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);
            CreateBouble(StackES15, 25, StackES2.Layer.Bounds.Height + 20, -10, -35);


        }


        /*Disegno una bolla di colore "background" definito in precedenza
         elemento: Oggetto UIView passato che contiene l'oggetto al quale aggiungere il layer sotto
         radious: indica il coeficente di rotondita per la bolla disegnata
         h: l'altezza della bolla variabile in base al contesto.
         h0: Il punto y di partenza*/
         private void CreateBouble(UIView elemento, int radious, nfloat h, nfloat h0, nfloat x0)
         {
            var SBouble = new UIView();
            //CGrect funziona attorno all'oggetto
            SBouble.Frame = new CGRect(x0, h0, View.Bounds.Width - 20, h);
            //Coloro i layer
            SBouble.BackgroundColor = bouble;
            //Arrotondo i layer
            SBouble.Layer.CornerRadius = radious;
            //Gli aggiungo agli oggetti di layout
            elemento.InsertSubview(SBouble, 0);
         }

        //Mostro un pop-uop di help
        partial void HelpBTN(NSObject sender)
        {
            lib.ShowPopup("Aiuto (?)", "🏋 = Campo che indica il Carico (Peso) del esercizio. \r\n⏱ = Campo che indica il recupero o pausa.", "OK", this);

        }

        //Invio la modifica al server myfitnessApp
        partial void CheckBTN(NSObject sender)
        {

        }

        /*Funzione che fixa la view a altezza corretta per gli elementi inseriti per un altezza totale di 980*/
        private void SetNormalHeightScrollView()
        {
            ScrollView.ContentSize = new CGSize(View.Bounds.Width, AltezzaContenuto);
        }

        /*Ogni volta che viene richiamato espande la view di 210, in modo da fare spazio al nuovo elemento, quello successivo.*/
        private void EspandiScrollView()
        {
            double val = AltezzaContenuto + ( 210 * indice);
            viewmax = val;
           
            ScrollView.ContentSize = new CGSize(ScrollView.Bounds.Width, val);
            Console.WriteLine("Ingrandisco di:" + val + " Indice: " + indice + " viewmax: " + viewmax + " Altezza SV: " + ScrollView.Bounds.Height);
            indice++;
            
        }
        /*Ogni volta che viene richiamato riduce la view di 210, in modo da fare spazio al nuovo elemento, quello successivo.*/
        private void RiduciScrollView()
        {
            double val = viewmax - 210 ;
            viewmax = val;
            ScrollView.ContentSize = new CGSize(ScrollView.Bounds.Width, val);
            
            indice--;
            Console.WriteLine("Rimpiciolisco di: "+val+" Indice: " + indice+" vmax: "+viewmax + " Altezza SV: " + ScrollView.Bounds.Height);
        }

        /*SERIE DI METODI DI CONTROLLO DELL'INPUT
         Analizzo quando i campi NomeEsercizio vengono compilati e se succede mostro il sucessivo, tutti i metodi sono eventi 'editing changed',
         in seguito allugo la scrolling view per mostrare tutto, in caso di cancellazione l'oggetto sucessivo dovra scomparire ridemensionando la scrolling view.*/
        private void EspandiRiduciSV(UITextField TV, bool f)
        {
            if ((TV.Text.Length > 3) && (f == false))
            {
                EspandiScrollView();
                f = true;
            }

            else if ((TV.Text.Length <= 3) && (f == true))
            {
                RiduciScrollView();
                f = false;
            }
        }
        partial void NomeEsercizio3(NSObject sender)
        {
            if ((Ne3.Text.Length > 3) && (flag3 == false))
            {
                EspandiScrollView();
                flag3 = true;
            }

            else if ((Ne3.Text.Length <= 3) && (flag3 == true))
            {
                RiduciScrollView();
                flag3 = false;
            }
        }
        partial void NomeEsercizio4(NSObject sender)
        {
            if ((Ne4.Text.Length > 3) && (flag4 == false))
            {
                EspandiScrollView();
                flag4 = true;
            }

            else if ((Ne4.Text.Length <= 3) && (flag4 == true))
            {
                RiduciScrollView();
                flag4 = false;
            }
        }
        partial void NomeEsercizio5(NSObject sender)
        {
            if ((Ne5.Text.Length > 3) && (flag5 == false))
            {
                EspandiScrollView();
                flag5 = true;
            }

            else if ((Ne5.Text.Length <= 3) && (flag5 == true))
            {
                RiduciScrollView();
                flag5 = false;
            }
        }
        partial void NomeEsercizio6(NSObject sender)
        {
            if ((Ne6.Text.Length > 3) && (flag6 == false))
            {
                EspandiScrollView();
                flag6 = true;
            }

            else if ((Ne6.Text.Length <= 3) && (flag6 == true))
            {
                RiduciScrollView();
                flag6 = false;
            }
        }
        partial void NomeEsercizio7(NSObject sender)
        {
            if ((Ne7.Text.Length > 3) && (flag7 == false))
            {
                EspandiScrollView();
                flag7 = true;
            }

            else if ((Ne7.Text.Length <= 3) && (flag7 == true))
            {
                RiduciScrollView();
                flag7 = false;
            }
        }
        partial void NomeEsercizio8(NSObject sender)
        {
            if ((Ne8.Text.Length > 3) && (flag8 == false))
            {
                EspandiScrollView();
                flag8 = true;
            }

            else if ((Ne8.Text.Length <= 3) && (flag8 == true))
            {
                RiduciScrollView();
                flag8 = false;
            }
        }
        partial void NomeEsercizio9(NSObject sender)
        {
            if ((Ne9.Text.Length > 3) && (flag9 == false))
            {
                EspandiScrollView();
                flag9 = true;
            }

            else if ((Ne9.Text.Length <= 3) && (flag9 == true))
            {
                RiduciScrollView();
                flag9 = false;
            }
        }
        partial void NomeEsercizio10(NSObject sender)
        {
            if ((Ne10.Text.Length > 3) && (flag10 == false))
            {
                EspandiScrollView();
                flag10 = true;
            }

            else if ((Ne10.Text.Length <= 3) && (flag10 == true))
            {
                RiduciScrollView();
                flag10 = false;
            }
        }
        partial void NomeEsercizio11(NSObject sender)
        {
            if ((Ne11.Text.Length > 3) && (flag11 == false))
            {
                EspandiScrollView();
                flag11 = true;
            }

            else if ((Ne11.Text.Length <= 3) && (flag11 == true))
            {
                RiduciScrollView();
                flag11 = false;
            }
        }
        partial void NomeEsercizio12(NSObject sender)
        {
            if ((Ne12.Text.Length > 3) && (flag12 == false))
            {
                EspandiScrollView();
                flag12 = true;
            }

            else if ((Ne12.Text.Length <= 3) && (flag12 == true))
            {
                RiduciScrollView();
                flag12 = false;
            }
        }
        partial void NomeEsercizio13(NSObject sender)
        {
            if ((Ne13.Text.Length > 3) && (flag13 == false))
            {
                EspandiScrollView();
                flag13 = true;
            }

            else if ((Ne13.Text.Length <= 3) && (flag13 == true))
            {
                RiduciScrollView();
                flag13 = false;
            }
        }
        partial void NomeEsercizio14(NSObject sender)
        {
            if ((Ne14.Text.Length > 3) && (flag14 == false))
            {
                EspandiScrollView();
                flag14 = true;
            }

            else if ((Ne14.Text.Length <= 3) && (flag14 == true))
            {
                RiduciScrollView();
                flag14 = false;
            }

        }
        partial void NomeEsercizio15(NSObject sender)
        {
            Console.WriteLine("Non puoi inserire altri elementi, la view non si espanderà.");
        }
    }
}
