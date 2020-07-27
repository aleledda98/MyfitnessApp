using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace Myfitnessapp.iOS
{
    public class Animazioni : UIViewController
    {
        /*Riproduco l'animazione di classica di iOS dove se si scorre nel bordo sinistro, in tempo reale la View si anima 
         *e si sposta indirizzando verso un altra pagina che in questo caso e la pagina precedente!
         View: La view da animare
         Da: ViewController di partenza creato nella classe di richiamo
         A: stringa contente il nome del viewcontroller di destinazione*/
        public void SlideAnimation(UIView View, string Da, string A)
        {
            var storyboard = UIStoryboard.FromName("Main", null);

            var interactiveTransitionRecognizer = new UIScreenEdgePanGestureRecognizer();
            var vc2 = storyboard.InstantiateViewController(Da);
            var vc1 = storyboard.InstantiateViewController(A);

            interactiveTransitionRecognizer.AddTarget(() => IWI(interactiveTransitionRecognizer, View, vc1, vc2));
            interactiveTransitionRecognizer.Edges = UIRectEdge.Left;

            View.AddGestureRecognizer(interactiveTransitionRecognizer);
        }


        /*Riproduco l'animazione di classica di iOS dove se si scorre nel bordo sinistro, in tempo reale la View si anima 
         *e si sposta indirizzando verso un altra pagina che in questo caso e la pagina precedente!
         View: La view da animare
         Da: ViewController di partenza creato nella classe di richiamo
         A: stringa contente il nome del viewcontroller di destinazione*/
        public void SlideAnimationIndex(UIView View, string Da, string A, int index)
        {
            var storyboard = UIStoryboard.FromName("Main", null);
            var vc1 = storyboard.InstantiateViewController(Da);

            var vc2 = (UITabBarController)storyboard.InstantiateViewController(A);
            vc2.SelectedIndex = index;

            var interactiveTransitionRecognizer = new UIScreenEdgePanGestureRecognizer();
            interactiveTransitionRecognizer.AddTarget(() => IWI(interactiveTransitionRecognizer, View, vc1, vc2));
            interactiveTransitionRecognizer.Edges = UIRectEdge.Left;

            View.AddGestureRecognizer(interactiveTransitionRecognizer);
        }

        //Metodo di appoggio
        private void IWI(UIScreenEdgePanGestureRecognizer sender, UIView View, UIViewController viewController, UIViewController dest)
        {
            //Contiene un valore numerico che varia in base allo stato della gesture
            var percento = sender.TranslationInView(View).X * 100 / sender.View.Bounds.Size.Width;

            //Gradino di destinazione
            CALayer gradient2 = new CALayer();
            gradient2.Frame = dest.View.Bounds;
            dest.View.Layer.InsertSublayer(gradient2, 0);
            View.Add(dest.View);

            //gradino di partenza
            CALayer gradient = new CALayer();
            gradient.Frame = viewController.View.Bounds;
            viewController.View.Layer.InsertSublayer(gradient, 1);
            View.Add(viewController.View);

            var TIV = sender.TranslationInView(View).X;
            var Width = sender.View.Bounds.Size.Width;

            //Quando la gesture termina, si alza il dito o limite schermo
            if (sender.State == UIGestureRecognizerState.Ended)
            {
                if (percento <= 40)
                {
                    //Annullo l'animazion e torna in posizione
                    var minTransform = CGAffineTransform.MakeTranslation(TIV, 0);
                    var maxTransform = CGAffineTransform.MakeIdentity();

                    viewController.View.Transform = true ? minTransform : maxTransform;
                    UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                    {
                        viewController.View.Transform = true ? maxTransform : minTransform;
                    }, null);
                }

                else
                {
                    //La gesture è conclusa quindi eseguo il seguente codice
                    //Feedback haptico
                    var impact = new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Soft);
                    impact.Prepare();
                    impact.ImpactOccurred();
                    //Spostamento view a casino
                    var A = CGAffineTransform.MakeTranslation(TIV, 0);
                    var B = CGAffineTransform.MakeTranslation(TIV + Width, 0);

                    //Centro quella giusta
                    var C = CGAffineTransform.MakeTranslation(TIV - Width, 0);
                    var D = CGAffineTransform.MakeTranslation(0, 0);

                    viewController.View.Transform = true ? A : B;

                    dest.View.Transform = true ? C : D;

                    UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                    {
                        dest.View.Transform = true ? D : C;
                    }, null);
                    UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                    {
                        //lo sposto dal cazzo
                        viewController.View.Transform = true ? B : A;
                    }, null);
                }
                //Devo rimuovere il rilevatore se no rimane attivo e tutte le schermate si glitchano
                View.RemoveGestureRecognizer(sender);
            }

            //Quando la gesture rileva una variazione
            if (sender.State == UIGestureRecognizerState.Changed)
            {
                var T = CGAffineTransform.MakeTranslation(TIV, 0);
                var C = CGAffineTransform.MakeTranslation(TIV - Width, 0);

                viewController.View.Transform = true ? T : T;
                dest.View.Transform = true ? C : C;
                UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseIn, () =>
                {
                    viewController.View.Transform = true ? T : T;
                    //rimbalzo view2
                    dest.View.Transform = true ? C : C;
                }, null);
             }

        } //fine animazione slide

        /*Animazione di Slide quando apri un nuovo viewController, slide uno sopra l'altro.
         View: La view di partenza
         A: il Viewcontroller di destinazione*/
        public void SlideTo(UIView View, string A)
        {
            var CC = CGAffineTransform.MakeTranslation(View.Bounds.Size.Width, 0);
            var DD = CGAffineTransform.MakeIdentity();

            //Istanzio il vc
            var storyboard = UIStoryboard.FromName("Main", null);
            var vc = storyboard.InstantiateViewController(A);           

            CALayer gradient = new CALayer();
            gradient.Frame = vc.View.Bounds;
            vc.View.Layer.InsertSublayer(gradient, 1);
            //AggiustoLayout(vc, View);
            View.Add(vc.View);

            //Centro quella giusta
            vc.View.Transform = true ? CC : DD;

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveLinear, () =>
            {
                vc.View.Transform = true ? DD : CC;
            }, null);

        }
        /*Animazione di Slide quando apri un nuovo viewController, slide uno sopra l'altro.
        View: La view di partenza
        A: il Viewcontroller di destinazione*/
        public void SlideToWithAdjust(UIView View, string A)
        {
            var CC = CGAffineTransform.MakeTranslation(View.Bounds.Size.Width, 0);
            var DD = CGAffineTransform.MakeIdentity();

            //Istanzio il vc
            var storyboard = UIStoryboard.FromName("Main", null);
            var vc = storyboard.InstantiateViewController(A);

            CALayer gradient = new CALayer();
            gradient.Frame = vc.View.Bounds;
            vc.View.Layer.InsertSublayer(gradient, 1);
            //AggiustoLayout(vc, View);

            //Centro quella giusta
            vc.View.Transform = true ? CC : DD;

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveLinear, () =>
            {
                vc.View.Transform = true ? DD : CC;
            }, null);

        }

        /*public void AggiustoLayout(UIViewController dest, UIView view)
        {
            //Devi fare sta cosa se no si compenetra tutto by stackoverflow!!
            //Qyuinbdi utilizzo safearea come area libera
            View.AddSubview(dest.View);
            dest.View.TranslatesAutoresizingMaskIntoConstraints = false;

            var safeGuide = dest.View.SafeAreaLayoutGuide;
            view.LeadingAnchor.ConstraintEqualTo(safeGuide.LeadingAnchor).Active = true;
            view.TrailingAnchor.ConstraintEqualTo(safeGuide.TrailingAnchor).Active = true;
            view.TopAnchor.ConstraintEqualTo(safeGuide.TopAnchor).Active = true;
            view.BottomAnchor.ConstraintEqualTo(safeGuide.BottomAnchor).Active= true;

            view.LayoutIfNeeded();
        }*/
    }
}
